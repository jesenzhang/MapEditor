using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraSettingAction : MapEventAction {

	public string settingFile = ""; 

	public override ActionArgValue[] Argments{
		get { 
			args = new ActionArgValue[1]; 
			args [0].strValue = settingFile;  
			return args;
		}
	}

	public override string displayName{
		get { 
			return "设置镜头文件-" + settingFile;
		}
	}
}
