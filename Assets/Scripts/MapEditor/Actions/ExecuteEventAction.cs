using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ExecuteEventAction : MapEventAction {
	public MapEvent mapEvent; 

	public override ActionArgValue[] Argments{
		get { 
			args = new ActionArgValue[1];
			args [0].intValue = mapEvent.eventId;  
			return args;
		}
	}

	public override string displayName{
		get { 
			return "执行事件-" + (mapEvent!=null?mapEvent.name:"");
		}
	} 
}
