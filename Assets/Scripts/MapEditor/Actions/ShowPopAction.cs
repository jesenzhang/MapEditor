using UnityEngine;
using System.Collections; 

[ExecuteInEditMode]
public class ShowPopAction : ShowTalkWordAction {
    public int dimissTime = 4000;

	public void Awake(){
		category = DialogCatetory.POPUP; 
	}

	public override ActionArgValue[] Argments{
		get { 
			args = new ActionArgValue[7];
			args [0].intValue = (int)lifeType;
			args [1].strValue = lifeId; 
			args [2].intValue = (int)category;  
			args [3].strValue = dialogID;  
			args [4].intValue = callbackEventID;

            return args;
		}
	}


}
