using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class NPCHPDropEvent : NPCHPChangeEvent {  

	public override string displayName{
		get { 
			if (npcId < 0) {
				return string.Format("玩家掉血量<={0}%",percent);
			}
			return string.Format("NPC-{0}掉血量<={1}%",npcId,percent);
		}
	}   
}
