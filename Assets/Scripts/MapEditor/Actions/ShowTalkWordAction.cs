using UnityEngine;
using System.Collections;

public enum DialogCatetory{
	POPUP = 1,
	DIALOG = 3,
}

[ExecuteInEditMode]
public class ShowTalkWordAction : LifeEventAction {  

	[HideInInspector]
	public DialogCatetory category = DialogCatetory.DIALOG;  

	public string dialogID;

	public int callbackEventID = -1;

	public override ActionArgValue[] Argments{
		get { 
			args = new ActionArgValue[5];
			args [0].intValue = (int)lifeType; 
			args [1].strValue = lifeId; 
			args [2].intValue = (int)category;  
			args [3].strValue = dialogID;  
			args [4].intValue = callbackEventID;  
			return args;
		}
	}

	public override string displayName{
		get { 

			if (category == DialogCatetory.POPUP) {

				switch (lifeType) {
				case LifeType.NPC:
					return "NPC-" + lifeId + "-头顶弹出气泡:" + dialogID;
					break;
				case LifeType.HELPER:
					return "助战"+lifeId+"头顶弹出气泡:" + dialogID;
					break;
				}
				return "玩家头顶弹出气泡:" + dialogID;
			} else if (category == DialogCatetory.DIALOG) {

				switch (lifeType) {
				case LifeType.NPC:
					return "NPC-" + lifeId + "-弹出带头像的对话:" + dialogID;
					break;
				case LifeType.HELPER:
					return "助战"+lifeId+"弹出带头像的对话:" + dialogID;
					break;
				}
				return "玩家弹出带头像的对话:" + dialogID;

			} else {
				return "" + category;
			}

		}
	} 
}
