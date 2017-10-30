using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class PickupItemAction : ItemEventAction {  

	public MapEvent successCallbackEvent;

	public MapEvent cancelCallbackEvent;

	public override ActionArgValue[] Argments{
		get { 
			args = new ActionArgValue[3];
			args [0].strValue = itemObj.itemId;  
			args [1].intValue = successCallbackEvent!=null?cancelCallbackEvent.eventId:-1; 
			args [2].intValue = cancelCallbackEvent!=null?cancelCallbackEvent.eventId:-1; 
			return args;
		}
	}

	public override string displayName{
		get { 
			return "提示拾取道具-" +(itemObj!=null?itemObj.name:"");
		}
	} 
}
