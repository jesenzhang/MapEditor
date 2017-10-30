using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LifeType{
	NONE = 0,
	PLAYER = 1,
	NPC = 2,
	HELPER = 3,
}

public class LifeEventAction : MapEventAction {
	public LifeType lifeType = LifeType.NPC;
	public string lifeId;
}
