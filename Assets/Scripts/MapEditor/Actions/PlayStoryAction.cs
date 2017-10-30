using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class PlayStoryAction : MapEventAction {
	public string sequeueName;

	public override ActionArgValue[] Argments{
		get { 
			args = new ActionArgValue[1];
			args [0].strValue = sequeueName;  
			return args;
		}
	}

	public override string displayName{
		get { 
			return "播放剧情-" + sequeueName;
		}
	} 
}
