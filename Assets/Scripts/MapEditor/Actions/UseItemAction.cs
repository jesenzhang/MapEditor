using UnityEngine;
using System.Collections;

public class UseItemAction : ItemEventAction { 

	public string npcID;

	public CallbackEvent successCallbackEvent; 
	public CallbackEvent cancelCallbackEvent; 

	public override ActionArgValue[] Argments{
		get { 
			args = new ActionArgValue[6]; 
			args [0].strValue = itemObj.itemId;   
			args [1].strValue = itemObj.itemIcon;   
			args [2].strValue = itemObj.itemName;   
			args [3].strValue = npcID;   
			args [4].intValue = successCallbackEvent!=null?successCallbackEvent.callbackID:-1;  
			args [5].intValue = cancelCallbackEvent!=null?cancelCallbackEvent.callbackID:-1;  
			return args;
		}
	}

	public override string displayName{
		get { 
			return "提示使用道具-"+ (itemObj!=null?itemObj.itemName:"");
		}
	} 
}
