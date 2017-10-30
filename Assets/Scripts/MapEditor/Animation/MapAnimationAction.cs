using UnityEngine;
using System.Collections;
 

public enum AnimationActionType{
	NONE = 0,
	PLAY_ACTION = 4,
	SHOW_EFFECT = 6,
	BRAKE = 8,
}

[ExecuteInEditMode]
public abstract class MapAnimationAction : MonoBehaviour {
	
	public AnimationActionType actionType; 

	protected ActionArgValue[] args;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public abstract ActionArgValue[] Arguments {
		get;
	}
}
