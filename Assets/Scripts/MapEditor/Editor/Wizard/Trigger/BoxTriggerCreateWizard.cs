using UnityEngine;
using UnityEditor;
using System.Collections;

public class BoxSceneTriggerCreateWizard : ScriptableWizard { 
	public SceneTriggerShape triggerShape = SceneTriggerShape.Box;
	public SceneTriggerType triggerType = SceneTriggerType.Event;
	public string triggerData;
	public Vector3 triggerSize = new Vector3(3,3,3);

	void OnWizardUpdate(){
		isValid = true;
		errorString = ""; 
	}

	void OnWizardCreate(){
		MapEventManager.CreateTrigger (Selection.activeGameObject,triggerType,triggerData,triggerShape,triggerSize);
	}
}
