using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.Linq;
 
public enum SceneTriggerType{
	Unknown,  
	Event,
	WayPoint,
}

public enum SceneTriggerShape{
	Unknown,
	Sphere,
	Box,  
}
 
[ExecuteInEditMode]
public class SceneTrigger : MonoBehaviour {
	public int objectId;
	public SceneTriggerType triggerType;
	public string triggerData; 
	public SceneTriggerShape shape;  

	public string effectName;
	public bool autoShow = true;

	#if UNITY_EDITOR
	void Update(){ 
		 
		/*
		switch (triggerType) { 
		case SceneTriggerType.Event:
			gameObject.name = "事件触发器-" + triggerData;
			break;
		default:
			gameObject.name = "触发器-" + triggerType + "-" + triggerData;
			break;
		} */
	}
	#endif
}
