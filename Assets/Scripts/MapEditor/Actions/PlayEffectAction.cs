using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectType{
	COMMON = 0,
	SCENE = 1,
	UI = 2,
}

[ExecuteInEditMode]
public class PlayEffectAction : LifeEventAction {
	public EffectType effectType = EffectType.COMMON;
	public string effectName;
	public int effectTime = 1000;

	public override ActionArgValue[] Argments{
		get { 
			args = new ActionArgValue[5];
			args [0].intValue = (int)lifeType; 
			args [1].strValue = lifeId; 
			args [2].strValue = effectName;  
			args [3].intValue = (int)effectType;  
			args [4].intValue = effectTime; 
			return args;
		}
	}

	public override string displayName{
		get { 
			return "让"+lifeId+"播放特效"+effectName;
		}
	} 
}
