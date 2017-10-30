using UnityEngine;
using UnityEditor;
using System.Collections;

public class HideUIEventActionCreateWizard    : ScriptableWizard   {   
	public string UIName  = null;

	void OnWizardUpdate(){
		isValid = true;
		errorString = "";  
	}

	void OnWizardCreate(){ 
		var action = MapEventActionManager.CreateAction<HideUIAction> (Selection.activeGameObject, Example.MapEventAction.Type.HIDE_UI, UIName);
		action.uiName = UIName;
		Selection.activeGameObject = action.gameObject;
	}
}
