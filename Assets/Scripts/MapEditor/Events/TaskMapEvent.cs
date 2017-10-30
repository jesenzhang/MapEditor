using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TaskEventStatus{
	ENABLED,
	ACCEPTED,
	FINISED,
	SUBMITED,
	REWARD,
}

[ExecuteInEditMode]
public class TaskMapEvent : MapEvent {
	public TaskEventStatus taskStatus; 
	public string taskID;
	public int goalIndex = -1;

	public override ActionArgValue[] Conditions {
		get { 
			conditions = new ActionArgValue[3];
			conditions [0].strValue = taskID;  
			conditions [1].intValue = (int)taskStatus;  
			conditions [2].intValue = goalIndex;  
			return conditions;
		}
	}

	public override string displayName{
		get { 
			return "任务状态："+taskStatus;
		}
	} 
}
