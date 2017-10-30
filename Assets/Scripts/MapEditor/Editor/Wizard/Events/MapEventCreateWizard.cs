using UnityEngine;
using UnityEditor;
using System.Collections;

public class MapEventCreateWizard : ScriptableWizard {
	public MapEventType eventType  ; 
	public string targetId;
	public ActionArgValue[] conditions; 

	void OnWizardUpdate(){
		isValid = true;
		errorString = "";  
	}

	void OnWizardCreate(){ 
		var mapEvent = MapEventManager.CreateEvent<MapEvent>  (eventType);
		mapEvent.conditions = conditions;
	}
}
