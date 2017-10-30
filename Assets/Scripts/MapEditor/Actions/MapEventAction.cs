using UnityEngine;
using System.Collections;
using System;
 

[Serializable]
public struct ActionArgValue{ 
	public string strValue;
	public int intValue;
	public float floatValue;
	public Vector3 posValue;
}

[ExecuteInEditMode]
public class MapEventAction : MonoBehaviour {
	public Example.MapEventAction.Type actionType; 

	public int delayTime = 0;

	[HideInInspector]
	public string actionTarget;
	[HideInInspector]
	public ActionArgValue[] args;  

	#if UNITY_EDITOR
	void Update(){  
		OnUpdate ();
		gameObject.name = displayName;   
	}

	#endif

	protected virtual void OnUpdate(){
	} 

	public virtual ActionArgValue[] Argments {
		get { 
			return args;
		}
	}

	public virtual string displayName{
		get { 

			switch (actionType) {
			case Example.MapEventAction.Type.DROP_ITEM:
				return "掉落道具-"+actionTarget; 
			case Example.MapEventAction.Type.HIDE_PICKUP:
				return "隐藏道具拾取提示"; 
			case Example.MapEventAction.Type.HIDE_USEITEM:
				return "隐藏使用道具提示"; 
			case Example.MapEventAction.Type.SHOW_WALL:
				return "重置墙体-"+actionTarget; 
			case Example.MapEventAction.Type.SHOW_UI:
				return "打开界面-"+actionTarget; 
			case Example.MapEventAction.Type.HIDE_UI:
				return "关闭界面-"+actionTarget; 
			case Example.MapEventAction.Type.SHOW_WAYPOINT:
				return "显示路径点-"+actionTarget; 
			case Example.MapEventAction.Type.PLAY_SEQUENCE:
				return "播放剧情-"+actionTarget; 
			}

			if (!string.IsNullOrEmpty(actionTarget)) {
				return "执行-" + actionType + "-" + actionTarget;
			} else {
				return "执行-" + actionType;
			}

		}
	}
}
