using UnityEngine;
using UnityEditor;
using System.Collections;

public class ResetTriggerActionCreateWizard  : ScriptableWizard {   

	public SceneTrigger trigger;  

	void OnWizardUpdate(){
		isValid = true;
		errorString = "";  
	}

	void OnWizardCreate(){ 
		MapEventActionManager.CreateResetTriggerAction (Selection.activeGameObject,trigger);
	}
}

