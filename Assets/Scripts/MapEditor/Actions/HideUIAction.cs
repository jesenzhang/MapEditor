using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class HideUIAction : MapEventAction {
	public string uiName;

	public override ActionArgValue[] Argments{
		get { 
			args = new ActionArgValue[1];
			args [0].strValue = uiName;  
			return args;
		}
	}

	public override string displayName{
		get { 
			return "隐藏界面-" + uiName;
		}
	} 
}
