using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
[ExecuteInEditMode]
public class ShowPropUI : MapEventAction {
	public string tipIcon = "Default";

	public string tipWord; 

	public CallbackEvent successCallbackEvent; 

	public CallbackEvent cancelCallbackEvent; 

	public override ActionArgValue[] Argments{
		get { 
			args = new ActionArgValue[4]; 
			args [0].strValue = tipIcon;  
			args [1].strValue = tipWord; 
			args [2].intValue = successCallbackEvent!=null?successCallbackEvent.callbackID:-1;  
			args [3].intValue = cancelCallbackEvent!=null?cancelCallbackEvent.callbackID:-1;  
			return args;
		}
	}

	public override string displayName{
		get { 
			return "提示功能-"+tipWord;
		}
	} 
}
