using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ClearEffect : MapAnimationAction {

	public string effect;
	 
	public override ActionArgValue[] Arguments {
		get{
			args = new ActionArgValue[1];
			args [0].strValue = effect;
			return args;
		}
	}
}
