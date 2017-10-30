using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CallbackEvent : MapEvent {
	public int callbackID = 0;

	public override ActionArgValue[] Conditions {
		get { 
			conditions = new ActionArgValue[1];  
			conditions [0].intValue = callbackID;
			return conditions;
		}
	}

	public override string displayName{
		get { 
			return string.Format("回调事件-{0}",callbackID);
		}
	}  
}
