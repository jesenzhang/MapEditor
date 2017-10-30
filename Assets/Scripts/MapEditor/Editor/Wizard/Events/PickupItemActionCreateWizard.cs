using UnityEngine;
using UnityEditor;
using System.Collections;

public class PickupItemActionCreateWizard : ScriptableWizard {  

	public SceneItem item = null; 

	void OnWizardUpdate(){
		isValid = true;
		errorString = ""; 

		if (item == null) {
			isValid = false;
			errorString = "请选择道具";
		}
	}

	void OnWizardCreate(){
		MapEventActionManager.CreatePickupItemAction (Selection.activeGameObject,item);
	}
}
