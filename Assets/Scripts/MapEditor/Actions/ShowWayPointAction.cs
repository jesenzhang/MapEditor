using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ShowWayPointAction : MapEventAction {
	public string wayPointId;

	public override ActionArgValue[] Argments{
		get { 
			args = new ActionArgValue[1];
			args [0].strValue = wayPointId;  
			return args;
		}
	}

	public override string displayName{
		get { 
			return "显示路径点-" + wayPointId;
		}
	} 
}
