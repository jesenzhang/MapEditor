using UnityEngine;
using UnityEditor;
using System.Collections;

public class ShowUIEventActionCreateWizard    : ScriptableWizard   {   
	public string UIName  = null;

	void OnWizardUpdate(){
		isValid = true;
		errorString = "";  
	}

	void OnWizardCreate(){ 
		var action = MapEventActionManager.CreateAction<ShowUIAction> (Selection.activeGameObject, Example.MapEventAction.Type.SHOW_UI, UIName);
		action.uiName = UIName;
		Selection.activeGameObject = action.gameObject;
	}
}
