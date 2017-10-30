using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class HideTransferAction : MapEventAction {
	public int transferId = -1; 

	public override ActionArgValue[] Argments{
		get { 
			args = new ActionArgValue[2]; 
			args [0].intValue = transferId;  
			return args;
		}
	}

	public override string displayName{
		get { 
			return "隐藏传送点 "+transferId;
		}
	} 

}
