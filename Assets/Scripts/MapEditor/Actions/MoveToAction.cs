using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class MoveToAction : LifeEventAction {
	public string action; 

	public Transform targetPosition;

	void OnEnable(){
		if (targetPosition == null) {
			var go = GameObject.CreatePrimitive (PrimitiveType.Cube);
			go.transform.parent = transform;
			targetPosition = go.transform;
		}
	}

	public override ActionArgValue[] Argments{
		get { 
			args = new ActionArgValue[4];
			args [0].intValue = (int)lifeType; 
			args [1].strValue = lifeId; 
			args [2].strValue = action;  
			args [3].posValue = targetPosition.position;  
			return args;
		}
	}

	public override string displayName{
		get { 
			return "让"+lifeId+"移动到"+targetPosition.position;
		}
	} 
}
