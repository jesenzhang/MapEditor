using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ExecuteTaskAction: TaskMapAction { 
	public override ActionArgValue[] Argments{
		get { 
			args = new ActionArgValue[3];
			args [0].strValue = taskId; 
			args [1].intValue = goldIndex;  
			return args;
		}
	}
	public override string displayName{
		get { 
			return "执行任务-" +taskId;
		}
	} 
}
