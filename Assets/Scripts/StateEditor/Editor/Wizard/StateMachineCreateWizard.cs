using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class StateMachineCreateWizard : ScriptableWizard {
	public string name = ""; 

	void OnWizardUpdate(){
		isValid = true;
		errorString = "";
		if (string.IsNullOrEmpty(name)) {
			isValid = false;
			errorString = "请输入状态机名称";
		}
	}

	void OnWizardCreate(){  
		var st = StateMachineManager.CreateStateMachine (name);
		//Selection.activeGameObject = st.gameObject;
	}
}
