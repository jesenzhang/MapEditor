using UnityEngine;
using UnityEditor;
using System.Collections;

public class SceneCreateWizard : ScriptableWizard { 

	public int sceneId = 0;
	public string resourceId = ""; 
	 
	void OnWizardUpdate(){
		isValid = true;
		errorString = "";

		if (sceneId <= 0) {
			isValid = false;
			errorString = "请输入合法副本id";
		} else if (string.IsNullOrEmpty(resourceId)) {
			isValid = false;
			errorString = "请输入合法的资源id";
		}
		 
	}

	void OnWizardCreate(){ 
		MapManager.CreateSceneMap (sceneId,resourceId);
	}
}
