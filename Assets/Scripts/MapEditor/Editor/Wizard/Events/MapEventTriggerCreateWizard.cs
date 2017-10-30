using UnityEngine;
using UnityEditor;
using System.Collections;

public class MapEventTriggerCreateWizard : ScriptableWizard {
	public MapEventTriggerType eventType = MapEventTriggerType.TriggerIn; 
	public string target; 

	void OnWizardUpdate(){
		isValid = true;
		errorString = "";  
	}

	void OnWizardCreate(){ 
	}
}
