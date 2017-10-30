using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class AttackEventTrigger : MapEventTrigger {
	public string skillID = "-1";

	 public override ActionArgValue Condition {
		get {
			condition.strValue = skillID;
			return condition;
		}
	}
}
