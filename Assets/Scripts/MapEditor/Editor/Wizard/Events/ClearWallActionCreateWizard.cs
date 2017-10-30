using UnityEngine;
using UnityEditor;
using System.Collections;

public class ClearWallActionCreateWizard : ScriptableWizard {   
	
	public MapWall wall; 

	public int key = 1; 

	void OnWizardUpdate(){
		isValid = true;
		errorString = "";  
	}

	void OnWizardCreate(){
		if (wall == null &&Selection.activeGameObject!=null) {
			wall = Selection.activeGameObject.GetComponent<MapWall> ();
		}
		MapEventActionManager.CreateClearWallAction (Selection.activeGameObject,wall,key);
	}
}
