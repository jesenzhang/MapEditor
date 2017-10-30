using UnityEngine;
using UnityEditor;
using System.Collections;

public class DropItemActionCreateWizard : ScriptableWizard {   
	 
	public SceneItem item = null;

	public int dropCount = 1;  

	void OnWizardUpdate(){
		isValid = true;
		errorString = ""; 

		if (item == null) {
			isValid = false;
			errorString = "请选择道具";
		}
	}

	void OnWizardCreate(){
		MapEventActionManager.CreateDropItemAction (Selection.activeGameObject, item, dropCount);
	}
}
