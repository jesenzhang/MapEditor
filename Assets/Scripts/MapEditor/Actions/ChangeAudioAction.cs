using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChangeAudioType{
	MOVEMENT = 1,
	BACKGROUND = 2,
}

[ExecuteInEditMode]
public class ChangeAudioAction : MapEventAction {
	public ChangeAudioType audioType;
	public string audioName;
	public int loop = 0;
	public float volume = 1;

	public override ActionArgValue[] Argments {
		get {
			args = new ActionArgValue[4]; 
			args [0].intValue = (int)audioType;  
			args [1].strValue = audioName;  
			args [2].intValue = loop;  
			args [3].floatValue = volume;  
			return args;
		}
	}

	public override string displayName {
		get {
			return "修改音效-" + audioType +" 为:"+audioName;
		}
	}
}
