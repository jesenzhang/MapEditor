using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BrakeObjectAction  : GameObjectAction { 

	public string npcID;

	public override ActionArgValue[] Argments{
		get { 
			args = new ActionArgValue[2];
			args [0].strValue = objPath;  
			args [1].strValue = npcID;  
			return args;
		}
	}

	public override string displayName{
		get { 
			return "破碎-" +objPath;
		}
	} 
}
