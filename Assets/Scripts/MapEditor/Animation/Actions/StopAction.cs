using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class StopAction : MapAnimationAction {
	public string action;

	void Update(){
	}
	public override ActionArgValue[] Arguments {
		get{
			args = new ActionArgValue[1];
			args [0].strValue = action; 
			return args;
		}
	}
}
