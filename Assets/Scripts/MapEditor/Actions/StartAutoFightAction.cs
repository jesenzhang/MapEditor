using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class StartAutoFightAction : MapEventAction { 

	public override ActionArgValue[] Argments{
		get { 
			args = new ActionArgValue[0]; 
			return args;
		}
	}

	public override string displayName{
		get { 
			return "开始自动战斗";
		}
	} 
}
