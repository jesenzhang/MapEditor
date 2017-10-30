using UnityEngine;
using UnityEditor;
using System.Collections;

public class MiniMapCreateWizard : ScriptableWizard { 
	public string mapId = null;

	void OnWizardUpdate(){
		isValid = true;
		errorString = "";

		if (string.IsNullOrEmpty("mapId")) {
			errorString = "Please Input Map ID";
			isValid = false;
		} 
	}

	void OnWizardCreate(){
		MiniMapHelper.GenerateMiniMap (mapId);
	}
}
