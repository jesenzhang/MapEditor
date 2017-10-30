using UnityEngine;
using UnityEditor;
using System.Collections;

public class MapWallCreateWizard : ScriptableWizard {	
	public string wallName = null;
	public int requireKey = 1; 

	void OnWizardUpdate(){
		isValid = true;
		errorString = "";  

		if (string.IsNullOrEmpty(wallName)) {
			isValid = false;
			errorString = "请输入墙体名称";
		}
	}

	void OnWizardCreate(){ 
		var wall = MapEventManager.CreateWall (  wallName,requireKey);
	}
}
