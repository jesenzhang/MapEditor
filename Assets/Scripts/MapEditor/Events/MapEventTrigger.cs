using UnityEngine;
using System.Collections;

public enum MapEventTriggerType{
	UNKNOWN,
	TriggerIn,
	TriggerOut,
	Born,
	Die, 
	Pickup,
	UseItem,
	Attack,
	Behit,
	HasBuff,
	HPChanged,
	MonaChanged,
}

[ExecuteInEditMode]
public class MapEventTrigger : MonoBehaviour {

	public MapEventTriggerType eventType;

	[HideInInspector]
	public string targetId;   

	public MapEvent triggerEvent;

	[HideInInspector]
	protected ActionArgValue condition;

	
	// Update is called once per frame
	void Update () {
		gameObject.name = displayName;
	}

	public virtual ActionArgValue Condition {
		get { 
			return condition;
		}
	}

	public string displayName{
		get { 
			string label = eventType.ToString();

			switch (eventType) {
			case MapEventTriggerType.Born:
				label = "出生";
				break;
			case MapEventTriggerType.Die:
				label = "死亡";
				break;
			case MapEventTriggerType.TriggerIn:
				label = "进入";
				break;
			case MapEventTriggerType.TriggerOut:
				label = "离开";
				break;
			case MapEventTriggerType.Pickup:
				label = "道具被拾取";
				break;
			case MapEventTriggerType.Attack:
				label = "释放技能";
				break;
			case MapEventTriggerType.Behit:
				label = "被攻击";
				break;
			case MapEventTriggerType.HPChanged:
				label = "掉血";
				break;
			default: 
				break;
			}

			return label;
		}
	}
}
