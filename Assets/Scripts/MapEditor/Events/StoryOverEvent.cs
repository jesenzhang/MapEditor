using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class StoryOverEvent : MapEvent {  
	public string sequeueName;

	public override ActionArgValue[] Conditions {
		get { 
			conditions = new ActionArgValue[1];  
			conditions [0].strValue = sequeueName;
			return conditions;
		}
	}

	public override string displayName{
		get { 
			return string.Format("剧情播放完成-{0}",sequeueName);
		}
	}  
}
