using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class HPDropEventTrigger : MapEventTrigger {
	 
	[Range(0,100)]
	public int dropHPPercent = 50; 

	public override ActionArgValue Condition {
		get { 
			condition.intValue = dropHPPercent;
			return condition;
		}
	}
}
