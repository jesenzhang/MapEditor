using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class UserGuideAction : MapEventAction {
	public string guideID = "";

	public bool begin = true;

	public override ActionArgValue[] Argments{
		get { 
			args = new ActionArgValue[2];
			args [0].strValue = guideID; 
			args [1].intValue = begin?1:0;  
			return args;
		}
	}
	public override string displayName{
		get { 
			if (begin) {
				return "开始引导-" +guideID;
			}
			return "结束引导-" +guideID;
		}
	} 
}
