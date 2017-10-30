using UnityEngine;
using System.Collections;

public enum EffectPos{
	FEET = 0,
	BODY = 1,
	HEAD = 2
}

[ExecuteInEditMode]
public class PlayEffect : MapAnimationAction {

	public string effect; 

	public int duration = -1;

	public EffectPos mountPoint = EffectPos.FEET; 

	public override ActionArgValue[] Arguments {
		get{
			args = new ActionArgValue[2];
			args [0].strValue = effect;
			args [0].intValue = duration;

			args [1].intValue = (int)mountPoint; 

			return args;
		}
	}
}
