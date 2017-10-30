using UnityEngine;
using System.Collections;
 
[ExecuteInEditMode]

public class FreezeAIAction : LifeEventAction {  
	public bool freeze;

	public override ActionArgValue[] Argments{
		get { 
			args = new ActionArgValue[3];
			args [0].intValue = (int)lifeType; 
			args [1].strValue = lifeId; 
			args [1].intValue = freeze?1:0;  
			return args;
		}
	}

	public override string displayName{
		get { 
			string text = "";
			if (freeze) {
				text = "冻结AI-"; 
			} else {
				text = "开启AI-"; 
			}
			if (lifeType == LifeType.HELPER) {
				text += "助战-"+lifeId;
			}else if (lifeType == LifeType.PLAYER) {
				text += "玩家";
			}else {
				text += "NPC-"+lifeId;
			}
			return text;
		}
	} 
}
