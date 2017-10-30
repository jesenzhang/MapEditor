using UnityEngine;
using System.Collections;

public enum MapEventType{
	Unknown,
	TriggerEnter = 1,
	TriggerOut = 2,
	NPCGroupDieCount = 3,
	ItemPick = 4,
	ItemUsed = 5,
	SceneIn = 6,
	SceneOut = 7,
	DiceResult = 8,
	StoryOver = 9,
	DiceWinner = 10,
	NPCHPDropTo = 11,
	SceneInit = 12,
	SceneDestroy = 13,
	TaskEvent=14,
	CallbackEvent= 1000,
}



[ExecuteInEditMode]
public class MapEvent : MonoBehaviour { 

	public MapEventType eventType;

	public int eventId; 

	public int executeCount = 1;

	public bool interception = false;

	[HideInInspector]
	public ActionArgValue[] conditions;  

	#if UNITY_EDITOR
	void Update(){  
		OnUpdate ();
		gameObject.name = displayName;   
	}

	#endif

	protected virtual void OnUpdate(){
	} 

	public virtual ActionArgValue[] Conditions {
		get { 
			return conditions;
		}
	}

	public virtual string displayName{
		get { 

			switch (eventType) {
			case MapEventType.SceneIn:
				return "进入副本";
			case MapEventType.SceneOut:
				return "离开副本";
			default:
				break;
			}
			return "机制-" + eventType;

		}
	}
}
