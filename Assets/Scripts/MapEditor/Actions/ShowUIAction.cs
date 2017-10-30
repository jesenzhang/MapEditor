using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ShowUIAction : MapEventAction {
	public string uiName;

	public string[] uiParams;

	public override ActionArgValue[] Argments{
		get { 
			args = new ActionArgValue[uiParams.Length+1];
			args [0].strValue = uiName;  
			for (int i = 0; i < uiParams.Length; ++i) {
				args [i+1].strValue = uiParams[i];  
			}
			return args;
		}
	}

	public override string displayName{
		get { 
			return "显示界面-" + uiName;
		}
	} 
}
