using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FlyingByWayPointAction : MapEventAction {

	public int mapID = -1;
	public int pathID = -1;

	public override ActionArgValue[] Argments{
		get { 
			args = new ActionArgValue[2];
			args [0].intValue = mapID; 
			args [1].intValue = pathID;  
			return args;
		}
	}
	public override string displayName{
		get { 
			return "按路径飞行-" +pathID;
		}
	} 
}
