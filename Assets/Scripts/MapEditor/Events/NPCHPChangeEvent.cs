using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class NPCHPChangeEvent : MapEvent { 
	public int npcId = -1;
	[Range(0,100)]
	public int percent = 0;   

	public override ActionArgValue[] Conditions {
		get { 
			conditions = new ActionArgValue[2];
			conditions [0].intValue = npcId;  
			conditions [1].intValue = percent;  
			return conditions;
		}
	}

	public override string displayName{
		get { 
			if (npcId < 0) {
				return string.Format("玩家血量达到{0}%",percent);
			}
			return string.Format("NPC-{0}血量达到{1}%",npcId,percent);
		}
	}   
}
