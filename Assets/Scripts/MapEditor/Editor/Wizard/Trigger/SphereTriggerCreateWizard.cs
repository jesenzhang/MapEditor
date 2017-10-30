using UnityEngine;
using UnityEditor;
using System.Collections;

public class SphereSceneTriggerCreateWizard : ScriptableWizard { 
	public SceneTriggerShape triggerShape = SceneTriggerShape.Sphere;
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
