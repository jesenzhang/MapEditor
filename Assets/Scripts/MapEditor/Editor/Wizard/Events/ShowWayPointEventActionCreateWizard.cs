using UnityEngine;
using UnityEditor;
using System.Collections;

public class ShowWayPointEventActionCreateWizard    : ScriptableWizard   {   
	public string wayPointId  = null;

	void OnWizardUpdate(){
		isValid = true;
		errorString = "";  
	}

	void OnWizardCreate(){ 
		var action = MapEventActionManager.CreateAction<ShowWayPointAction> (Selection.activeGameObject, Example.MapEventAction.Type.SHOW_WAYPOINT, wayPointId);
		action.wayPointId = wayPointId;
		Selection.activeGameObject = action.gameObject;
	}
}
