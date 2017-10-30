using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MapEditorSetting : ScriptableWizard {
	public string exportDir = ""; 

	void OnWizardUpdate(){
		isValid = true;
		errorString = "";
		if (string.IsNullOrEmpty(exportDir)) {
			isValid = false;
			errorString = "请输入合法的路径";
		}
	}

	void OnWizardCreate(){ 
		MapUtil.UPDATE_DIR = exportDir;
	}
	 
}
