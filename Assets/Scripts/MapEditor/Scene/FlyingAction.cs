using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FlyingAction : MonoBehaviour {
	public int objectId;

	public Example.FlyingAction.Type action;

	public string arg1;

	public string arg2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.name = "Action-" + objectId +"-" + action;
	}
}
