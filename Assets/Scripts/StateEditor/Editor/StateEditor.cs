using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class StateEditor  {

	[MenuItem("StateEditor/Create State Machine",false,100)]
	static void CreateStateMachine(){
		ScriptableWizard.DisplayWizard<StateMachineCreateWizard> ("Create State Machine","Create");
	}

	[MenuItem("StateEditor/Create State",false,101)]
	static void CreateStateNode(){
		var go = Selection.activeGameObject;
		var st = go.GetComponent<StateMachine> ();
		if ( st== null) {
			Debug.LogWarningFormat ("State Machine not selected");
			return;
		}
		ScriptableWizard.DisplayWizard<StateNodeCreateWizard> ("Create State Node","Create");
	}

	[MenuItem("StateEditor/Export State Machine",false,200)]
	static void ExportStateMachine(){ 
		var st = Selection.activeGameObject!=null?Selection.activeGameObject.GetComponent<StateMachine> ():null; 
		if ( st== null) {
			Debug.LogWarningFormat ("State Machine not selected");
			return;
		}
		StateMachineManager.ExportStateMachine (st, "Export/StateMachine/StateMachine_"+st.name+".bytes");
	}

	[MenuItem("StateEditor/Export All State Machine",false,201)]
	static void ExportAllStateMachine(){ 
		foreach (var st in GameObject.FindObjectsOfType<StateMachine> ()) {
			StateMachineManager.ExportStateMachine (st, "Export/StateMachine/StateMachine_"+st.name+".bytes");
		}
	}

}
