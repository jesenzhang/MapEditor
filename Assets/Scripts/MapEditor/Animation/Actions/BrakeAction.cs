using UnityEngine;
using System.Collections;
[ExecuteInEditMode]

public class BrakeAction : MapAnimationAction {

	public int duration;	 

	public override ActionArgValue[] Arguments {
		get{
			args = new ActionArgValue[1];
			args [0].intValue = duration; 
			return args;
		}
	}
}
