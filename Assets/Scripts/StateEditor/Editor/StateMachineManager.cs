using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using System.IO;

public class StateMachineManager  {

	private const string STATE_MACHINE_PREFAB_DIR = "Assets/StateEditor/Prefabs/";
	
	public static StateMachine CreateStateMachine(string name){
		string folder = STATE_MACHINE_PREFAB_DIR + name;

		Directory.CreateDirectory (folder);
	
		var controller = AnimatorController.CreateAnimatorControllerAtPath (folder +"/" + name + ".controller"); 
		 
		var go = new GameObject (name);
		var st = go.AddComponent<StateMachine> ();
		st.controller = controller;

		var prefab = PrefabUtility.CreatePrefab (folder + "/" + name + ".prefab", go);
		PrefabUtility.ConnectGameObjectToPrefab (go,prefab);

		Selection.activeObject = controller;

		return st;
	}


	public static void CreateStateNode(StateMachine stateMachine, string name){
		var controller = stateMachine.controller as AnimatorController;
		var st = controller.layers [0].stateMachine;
		st.AddState (name);
	}

	public static void ExportStateMachine(StateMachine stateMachine,string path){
		var controller = stateMachine.controller as AnimatorController;
		var st = controller.layers [0].stateMachine;

		Example.StateMachineData stateMacs = new Example.StateMachineData();

		List<Example.StateMachine> macList = new List<Example.StateMachine> (); 

		var rootMachine = ExportStateMachine (st.anyStateTransitions,macList,controller,st,stateMachine.name);

		stateMacs.StateMachines = macList; 
		stateMacs.RootStateMachine = rootMachine.Id;

		Debug.LogFormat ("RootMachine id={0} defaultState={1}",rootMachine.Id,rootMachine.DefaultState);

		var data = Example.StateMachineData.SerializeToBytes (stateMacs);
		File.WriteAllBytes ( path,data);

		Debug.LogFormat ("Export StateMachine {0} To {1}", stateMachine.name, path);
	}




	private static Example.StateMachine ExportStateMachine(AnimatorStateTransition[] topAnyState, List<Example.StateMachine> macList,AnimatorController controller,AnimatorStateMachine st,string name){
		
		List<Example.StateMachineNode> nodeList = new List<Example.StateMachineNode> (); 
		List<Example.StateMachineTransaction> transList = new List<Example.StateMachineTransaction> (); 
		List<Example.StateMachineParam> paramList = new List<Example.StateMachineParam> (); 

		foreach (var param in controller.parameters) {
			var stParam = new Example.StateMachineParam();
			stParam.Name = param.name;
			switch (param.type) {
			case AnimatorControllerParameterType.Bool:
				stParam.valueType = Example.StateMachineParam.ValueType.BOOLEAN;
				stParam.BoolValue = param.defaultBool;
				break;
			case AnimatorControllerParameterType.Float:
				stParam.valueType = Example.StateMachineParam.ValueType.FLOAT;
				stParam.FloatValue = param.defaultFloat;
				break;
			case AnimatorControllerParameterType.Int:
				stParam.valueType = Example.StateMachineParam.ValueType.INTEGER;
				stParam.IntValue = param.defaultInt;
				break;
			case AnimatorControllerParameterType.Trigger:
				stParam.valueType = Example.StateMachineParam.ValueType.TRIGGER; 
				break;
			default:
				Debug.LogErrorFormat ("param type {0} not found", param.type);
				break;
			}
			paramList.Add (stParam);
		}
		 
		Dictionary<Object,Example.StateMachineNode> nodes = new Dictionary<Object, Example.StateMachineNode> (); 

		int stateID = 0;
		foreach (var state in st.states) {
			Debug.LogFormat (name+" child state {0}", state.state.name); 
			var stState = new Example.StateMachineNode();
			stState.Id = nodeList.Count;
			stState.StateName = state.state.name;
			stState.ChildStateMachine = -1;
			stState.Duration = state.state.motion!=null?Mathf.RoundToInt(state.state.motion.averageDuration*1000):-1;

			List<string> enterActions = new List<string> ();
			List<string> exitActions = new List<string> ();
			foreach (var behavior in state.state.behaviours) {
				var action1 = behavior as StateMachineEnterAction;
				var action2 = behavior as StateMachineExitAction;
				var action3 = behavior as LuaStateMachineAction;
				if (action1 != null) {
					enterActions.Add (action1.action);
				}else if (action2 != null) {
					exitActions.Add (action2.action);
				}else if (action3 != null) {
					stState.ScriptName = action3.scriptName;
				}
			} 
			stState.EnterActions = enterActions;
			stState.ExitActions = exitActions;
			nodes [state.state] = stState;
			nodeList.Add (stState);
			 
		}

		foreach (var childST in st.stateMachines) {
			Debug.LogFormat (name+" child st {0}", childST.stateMachine.name); 
			var childStateMachine = ExportStateMachine (topAnyState,macList,controller,childST.stateMachine,childST.stateMachine.name);
			var stState = new Example.StateMachineNode();
			stState.Id = nodeList.Count;
			stState.StateName = childST.stateMachine.name;
			stState.ChildStateMachine = childStateMachine.Id; 
			  

			List<string> enterActions = new List<string> ();
			List<string> exitActions = new List<string> ();
			foreach (var behavior in childST.stateMachine.behaviours) {
				var action1 = behavior as StateMachineEnterAction;
				var action2 = behavior as StateMachineExitAction;
				var action3 = behavior as LuaStateMachineAction;
				if (action1 != null) {
					enterActions.Add (action1.action);
				}else if (action2 != null) {
					exitActions.Add (action2.action);
				}else if (action3 != null) {
					stState.ScriptName = action3.scriptName;
				}
			} 
			stState.EnterActions = enterActions;
			stState.ExitActions = exitActions;

			nodes [childST.stateMachine] = stState;
			nodeList.Add (stState);
		}
		 

		/*foreach (var transaction in st.anyStateTransitions) { 
			var stTransaction = ExportTransaction (controller,transList,transaction,-1,nodes);
			stTransaction.ExitTime = transaction.hasExitTime ? Mathf.RoundToInt(transaction.exitTime * 1000): -1; 
		}*/
		 
		foreach (var transaction in topAnyState) {
			if (transaction.destinationState != null && nodes.ContainsKey (transaction.destinationState)) {
				var stTransaction = ExportTransaction (controller,transList,transaction,-1,nodes[transaction.destinationState].Id);
				stTransaction.ExitTime = transaction.hasExitTime ? Mathf.RoundToInt(transaction.exitTime * 1000): -1;
			}else if (transaction.destinationStateMachine != null && nodes.ContainsKey (transaction.destinationStateMachine)) {
				var stTransaction = ExportTransaction (controller,transList,transaction,-1,nodes[transaction.destinationStateMachine].Id);
				stTransaction.ExitTime = transaction.hasExitTime ? Mathf.RoundToInt(transaction.exitTime * 1000): -1;
			}
		}

		foreach (var state in st.states) {
			var stState = nodes [state.state];
			List<int> trans = new List<int> (); 
			foreach (var transaction in state.state.transitions) { 
				var stTransaction = ExportTransaction (controller,transList,transaction,stState.Id,nodes);  
				stTransaction.ExitTime = transaction.hasExitTime ? Mathf.RoundToInt(transaction.exitTime * 1000): -1; 
			}
			stState.Transactions = trans;
		}


		foreach (var state in st.stateMachines) { 
			var stState = nodes [state.stateMachine];
			List<int> trans = new List<int> (); 
			foreach (var transaction in st.GetStateMachineTransitions(state.stateMachine)) {				
				var stTransaction = ExportTransaction (controller,transList,transaction,stState.Id,nodes);
				if (transaction.name.StartsWith ("exittime=")) {
					var exitTimeStr = transaction.name.Substring ("exittime=".Length);
					if (exitTimeStr.Length > 0) {
						stTransaction.ExitTime = Mathf.RoundToInt(float.Parse (exitTimeStr)*1000);
					}
				}
			}
			stState.Transactions = trans;
		}

		Example.StateMachine stateMac = new Example.StateMachine();
		stateMac.States = nodeList;
		stateMac.Transactions = transList;
		stateMac.Params = paramList;

		Example.StateMachineNode defaultST = null;  

		if (st.defaultState!=null) {
			Object defaultObject = st.defaultState;
			if (!nodes.ContainsKey (st.defaultState)) { 
				var defaultMachine = FindParentStateMachine (st, st.defaultState);
				defaultObject = defaultMachine;
				if (defaultObject == null) { 
					Debug.LogErrorFormat ("st {0} default {1} not found", st.name, st.defaultState.name);
				} else {
					Debug.LogWarningFormat ("st {0} default machine {1} ", st.name, defaultMachine.name);
				}
			}
			defaultST = nodes[defaultObject];
		} else {
			Debug.LogErrorFormat ("st {0} defaultState is null", st.name);
		}


		stateMac.Id = macList.Count;
		stateMac.Name = name;
		stateMac.DefaultState = defaultST!=null?defaultST.Id:-1;
		macList.Add (stateMac);

		return stateMac;

	} 

	private static AnimatorStateMachine FindParentStateMachine(AnimatorStateMachine root,AnimatorState targetState){
		LinkedList<AnimatorStateMachine> stack = new LinkedList<AnimatorStateMachine> ();
		if (FindParentStateMachine (root, targetState, stack)) {
			return stack.First.Value;
		}
		return null;
	}

	private static bool FindParentStateMachine(AnimatorStateMachine stateMachine,AnimatorState targetState,LinkedList<AnimatorStateMachine> stack){ 
		foreach (var childState in stateMachine.states) {
			if (childState.state == targetState) {
				stack.AddLast (stateMachine);
				return true; 
			}
		} 

		foreach (var childST in stateMachine.stateMachines) {
			if (FindParentStateMachine (childST.stateMachine, targetState, stack)) {
				return true;
			}
		} 
		return false;
	}

	private static Example.StateMachineTransaction ExportTransaction(AnimatorController controller, List<Example.StateMachineTransaction> transList,AnimatorTransitionBase transition,int fromState,Dictionary<Object,Example.StateMachineNode> nodes){
		Example.StateMachineNode toState = null;
		if (transition.destinationState != null) { 
			toState = nodes[transition.destinationState];
		} else if (transition.destinationStateMachine != null) {
			toState = nodes[transition.destinationStateMachine];
		}
		return ExportTransaction (controller,transList,transition,fromState,toState!=null?toState.Id:-1); 

	}

	private static Example.StateMachineTransaction ExportTransaction(AnimatorController controller, List<Example.StateMachineTransaction> transList,AnimatorTransitionBase transition,int fromState,int toState){
		 

		Example.StateMachineTransaction trans = new Example.StateMachineTransaction ();
		trans.FromState = fromState;
		trans.ToState = toState;
		trans.Id = transList.Count; 
		trans.IsExit = transition.isExit; 
		trans.IsMute = transition.mute;
		trans.IsSolo = transition.solo;

		List<Example.StateMachineTransactionCondition> conditionList = new List<Example.StateMachineTransactionCondition> ();
		foreach (var condition in transition.conditions) {
			var stCondition = new Example.StateMachineTransactionCondition ();
			stCondition.What = condition.parameter;
			stCondition.Value = condition.threshold;

			//Debug.LogFormat ("transition {0} {1} {2} {3} ",transition.name,condition.parameter,condition.mode,condition.threshold);

			switch (condition.mode) {
			case AnimatorConditionMode.Equals:
				stCondition.Type = Example.StateMachineTransactionCondition.ConditionType.EQUAL;
				break;
			case AnimatorConditionMode.NotEqual:
				stCondition.Type = Example.StateMachineTransactionCondition.ConditionType.NOT_EQUAL;
				break;
			case AnimatorConditionMode.Greater:
				stCondition.Type = Example.StateMachineTransactionCondition.ConditionType.GREATER;
				break;
			case AnimatorConditionMode.Less:
				stCondition.Type = Example.StateMachineTransactionCondition.ConditionType.LESS;
				break; 
			case AnimatorConditionMode.If:
				stCondition.Type = Example.StateMachineTransactionCondition.ConditionType.IF;
				break; 
			case AnimatorConditionMode.IfNot:
				stCondition.Type = Example.StateMachineTransactionCondition.ConditionType.IF_NOT;
				break; 
			default: 
				foreach (var param in controller.parameters) {
					if (param.name == condition.parameter) {
						if (param.type == AnimatorControllerParameterType.Trigger) {
							stCondition.Type = Example.StateMachineTransactionCondition.ConditionType.TRIGGER;
						}
						break;
					}
				}
				break;
			}
			conditionList.Add (stCondition);
		}
		trans.Conditions = conditionList;

		transList.Add (trans);
		return trans;
	}


	 
}
