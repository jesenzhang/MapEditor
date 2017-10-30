using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BehitEventTrigger : MapEventTrigger {
	
	public LifeType attackerType = LifeType.PLAYER;
	public int attackerID = -1;
	public string skillID = "-1";

	public override ActionArgValue Condition {
		get {
			condition.strValue = skillID;
			condition.intValue = attackerID;
			return condition;
		}
	}
}
