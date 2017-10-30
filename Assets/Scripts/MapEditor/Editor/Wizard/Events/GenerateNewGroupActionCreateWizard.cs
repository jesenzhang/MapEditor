using UnityEngine;
using UnityEditor;
using System.Collections;

public class GenerateNewGroupActionCreateWizard : ScriptableWizard {   
	
	public SceneNPCGroup group;  

	void OnWizardUpdate(){
		isValid = true;
		errorString = "";  
	}

	void OnWizardCreate(){
		if (group == null &&Selection.activeGameObject!=null) {
			group = Selection.activeGameObject.GetComponent<SceneNPCGroup> ();
		}
		MapEventActionManager.CreateNewGroupAction (Selection.activeGameObject,group);
	}
}
