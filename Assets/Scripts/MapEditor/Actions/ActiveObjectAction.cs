using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ActiveObjectAction : GameObjectAction {
	public bool show ;

	public override ActionArgValue[] Argments{
		get { 
			args = new ActionArgValue[2];
			args [0].strValue = objPath; 
			args [1].intValue = show?1:0;  
			return args;
		}
	}

	public override string displayName{
		get { 
			if (show) {
				return "显示-" +objPath;
			}
			return "隐藏-" +objPath;
		}
	} 
	 
}
