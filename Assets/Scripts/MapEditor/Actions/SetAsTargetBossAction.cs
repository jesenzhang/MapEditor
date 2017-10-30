using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class SetAsTargetBossAction : MapEventAction {  

	public string npcId;

	public bool lockCamera = true;

	public override ActionArgValue[] Argments{
		get { 
			args = new ActionArgValue[1];
			args [0].strValue = npcId;  
			args [0].intValue = lockCamera?1:0;  
			return args;
		}
	}

	public override string displayName{
		get { 
			return "设置为当前目标BOSS-" +npcId;
		}
	} 
}
