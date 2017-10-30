using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class PlayModelAction : LifeEventAction { 

	public string action; 

	public int fadeTime = 3000;

	public override ActionArgValue[] Argments{
		get { 
			args = new ActionArgValue[4];
			args [0].intValue = (int)lifeType; 
			args [1].strValue = lifeId; 
			args [2].strValue = action;  
			args [3].intValue = fadeTime;  
			return args;
		}
	}

	public override string displayName{
		get { 
			return "让"+lifeId+"执行动作"+action;
		}
	} 
}
