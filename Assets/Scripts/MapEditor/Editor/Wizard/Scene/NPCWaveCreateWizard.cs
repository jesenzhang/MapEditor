using UnityEngine;
using UnityEditor;
using System.Collections;

public class NPCWaveCreateWizard : ScriptableWizard { 
	public int delayTime = 0;

	void OnWizardUpdate(){
		isValid = true;
		errorString = "";

		if (MapManager.selectionGroup == null) {
			errorString = "Please Select Group";
			isValid = false;
		} 
	}

	void OnWizardCreate(){
		MapManager.CreateNPCWave (MapManager.selectionGroup,delayTime);
	}
}
