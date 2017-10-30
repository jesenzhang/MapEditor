using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TransferAction : MapEventAction {
	public int mapId = 0;
	public int nodeId = 0;

	public override ActionArgValue[] Argments{
		get { 
			args = new ActionArgValue[3];
			args [0].intValue =  mapId;  
			args [1].intValue = nodeId;  
			return args;
		}
	}

	public override string displayName{
		get { 
			return "跳转到"+mapId+"("+nodeId+")";
		}
	} 
 
}
