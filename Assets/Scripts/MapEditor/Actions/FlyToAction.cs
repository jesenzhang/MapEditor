using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FlyToAction : LifeEventAction {
	public Transform targetPos;
	public override ActionArgValue[] Argments{
		get { 
			args = new ActionArgValue[3];
			args [0].intValue = (int)lifeType; 
			args [1].strValue = lifeId; 
			args [2].posValue = targetPos.position;   
			return args;
		}
	}

	public override string displayName{
		get { 
			if (targetPos != null) {
				return "飞到" + targetPos.position;
			} else {
				return "飞行";
			}

		}
	} 
}
