using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SubmitDungenonAcion : MapEventAction {
	public int mapID = -1;

	public override ActionArgValue[] Argments{
		get { 
			args = new ActionArgValue[1];
			args [0].intValue = mapID;  
			return args;
		}
	}

	public override string displayName{
		get { 
			return "提交" + (mapID>0?"副本-"+mapID:"当前副本");
		}
	} 
}
