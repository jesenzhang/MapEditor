using UnityEngine;
using UnityEditor;
using System.Collections;

public class PlaySequeueEventActionCreateWizard    : ScriptableWizard   {   
	public string sequeueName  = null;

	void OnWizardUpdate(){
		isValid = true;
		errorString = "";  
	}

	void OnWizardCreate(){ 
		var action = MapEventActionManager.CreateAction<PlayStoryAction> (Selection.activeGameObject, Example.MapEventAction.Type.PLAY_SEQUENCE, sequeueName);
		action.sequeueName = sequeueName;
		Selection.activeGameObject = action.gameObject;
	}
}
