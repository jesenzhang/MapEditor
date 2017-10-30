using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class StateNodeCreateWizard : ScriptableWizard {
	public string name = ""; 

	void OnWizardUpdate(){
		isValid = true;
		errorString = "";
		if (string.IsNullOrEmpty(name)) {
			isValid = false;
			errorString = "请输入状态名称";
		}
	}

	void OnWizardCreate(){  
		var go = Selection.activeGameObject;
		var st = go.GetComponent<StateMachine> ();
		StateMachineManager.CreateStateNode (st,name); 
	}
}
