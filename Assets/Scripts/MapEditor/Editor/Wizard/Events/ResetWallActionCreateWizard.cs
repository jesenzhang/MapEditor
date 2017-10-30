using UnityEngine;
using UnityEditor;
using System.Collections;

public class ResetWallActionCreateWizard : ScriptableWizard {   
	
	public MapWall wall; 

	public bool show = true; 

	void OnWizardUpdate(){
		isValid = true;
		errorString = "";  
	}

	void OnWizardCreate(){
		if (wall == null &&Selection.activeGameObject!=null) {
			wall = Selection.activeGameObject.GetComponent<MapWall> ();
		}
		MapEventActionManager.CreateResetWallAction (Selection.activeGameObject,wall,show);
	}
}
