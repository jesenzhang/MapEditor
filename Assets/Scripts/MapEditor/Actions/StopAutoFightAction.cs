using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class StopAutoFightAction : MapEventAction { 

	public override ActionArgValue[] Argments{
		get { 
			args = new ActionArgValue[0]; 
			return args;
		}
	}

	public override string displayName{
		get { 
			return "停止自动战斗";
		}
	} 
}
