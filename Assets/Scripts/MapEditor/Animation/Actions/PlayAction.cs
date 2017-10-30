using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class PlayAction : MapAnimationAction {

	public string action;	 

	public override ActionArgValue[] Arguments {
		get{
			args = new ActionArgValue[1];
			args [0].strValue = action; 
			return args;
		}
	}
	 
}
