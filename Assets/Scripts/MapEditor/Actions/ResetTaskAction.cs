using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class ResetTaskAction: TaskMapAction { 
	public override ActionArgValue[] Argments{
		get { 
			args = new ActionArgValue[2];
			args [0].strValue = taskId; 
			args [1].intValue = goldIndex;  
			return args;
		}
	}
	public override string displayName{
		get { 
			return "重置任务-" +taskId;
		}
	} 
} 