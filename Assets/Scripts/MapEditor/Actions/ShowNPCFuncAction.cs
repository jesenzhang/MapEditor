using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ShowNPCFuncAction : LifeEventAction { 

	public string tipIcon = "Default";

	public string tipWord;

	public CallbackEvent callbackEvent;
	
	public override ActionArgValue[] Argments{
		get { 
			args = new ActionArgValue[5];
			args [0].intValue = (int)lifeType; 
			args [1].strValue = lifeId;  
			args [2].strValue = tipIcon;  
			args [3].strValue = tipWord;  
			args [4].intValue = callbackEvent!=null?callbackEvent.callbackID:-1;  
			return args;
		}
	}

	public override string displayName{
		get { 
			return "显示NPC功能-"+lifeId;
		}
	} 
}
