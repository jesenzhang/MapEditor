using UnityEngine;
using UnityEditor;
using System.Collections;

public class UseItemActionCreateWizard : ScriptableWizard {   
	  
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
		MapEventActionManager.CreateUseItemAction (Selection.activeGameObject,item);
	}
}
