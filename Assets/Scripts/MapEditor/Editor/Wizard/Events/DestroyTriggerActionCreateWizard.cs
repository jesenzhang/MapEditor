using UnityEngine;
using UnityEditor;
using System.Collections;

public class DestroyTriggerActionCreateWizard : ScriptableWizard {   
	public SceneTrigger trigger;  

	void OnWizardUpdate(){
		isValid = true;
		errorString = "";  
	}

	void OnWizardCreate(){ 
		MapEventActionManager.CreateDestroyTriggerAction (Selection.activeGameObject,trigger);
	}
}
