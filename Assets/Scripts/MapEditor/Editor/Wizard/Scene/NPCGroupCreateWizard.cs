using UnityEngine;
using UnityEditor;
using System.Collections;

public class NPCGroupCreateWizard : ScriptableWizard { 
	
	public int delayTime = 0;
	 
	void OnWizardUpdate(){
		isValid = true;
		errorString = "";
		 
	}

	void OnWizardCreate(){ 
		MapManager.CreateNPCGroup (MapManager.current,delayTime);
	}
}
