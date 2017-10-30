using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class StopMusicAction : MapEventAction {
	public MapAudioSource audioSource;

	public override ActionArgValue[] Argments {
		get {
			args = new ActionArgValue[1]; 
			args [0].intValue = audioSource.audioID;  
			return args;
		}
	}

	public override string displayName {
		get {
			return "关闭音频源-" + audioSource;
		}
	}
}
