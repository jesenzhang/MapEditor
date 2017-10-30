using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class TriggerEventAction : MapEventAction { 

	public int triggerId;  

	public SceneTrigger triggerObj;

	[HideInInspector]
	public string objPath; 

	protected override void OnUpdate(){ 
		if (triggerObj != null) {
			objPath = MapEventHelper.GetGameObjectPath (triggerObj.transform);
			triggerId = triggerObj.objectId;
		} else if (!string.IsNullOrEmpty (objPath) && triggerObj == null) {
			triggerObj = GameObject.Find (objPath).GetComponent<SceneTrigger> ();
		}else {
			triggerObj = FindTrigger (triggerId);
		}
	}

	public override ActionArgValue[] Argments{
		get { 
			args = new ActionArgValue[1];
			args [0].intValue = triggerId;  
			return args;
		}
	}

	SceneTrigger FindTrigger(int triggerId){
		var triggers = GameObject.Find ("GameTriggers").GetComponentsInChildren<SceneTrigger> ();
		foreach (var trigger in triggers) {
			if (trigger.objectId == triggerId) {
				return trigger;
			}
		}
		return null;
	}
	 
	public override string displayName{
		get { 
			if (actionType == Example.MapEventAction.Type.RESET_TRIGGER) {
				return  "激活-"+(triggerObj!=null?triggerObj.name:"");
			} else if(actionType == Example.MapEventAction.Type.DESTROY_TRIGGER){
				return  "销毁-"+(triggerObj!=null?triggerObj.name:"");
			}else{
				return base.displayName;
			} 
		}
	}  
}
