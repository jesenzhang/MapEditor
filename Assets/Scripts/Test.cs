using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
		var camGo = new GameObject ("RoleCamera");
		var roleCamera = camGo.AddComponent<Camera> ();
		roleCamera.CopyFrom (Camera.main);
		roleCamera.depth = Camera.main.depth + 1;
		roleCamera.cullingMask = LayerMask.GetMask ("Role");
		roleCamera.clearFlags = CameraClearFlags.Depth;

		roleCamera.transform.parent = transform;
		roleCamera.transform.localPosition = Vector3.zero;
		roleCamera.transform.localScale = Vector3.one;
		roleCamera.transform.localRotation = Quaternion.identity;

		Camera.main.cullingMask = LayerMask.GetMask ("Default","Wall","Life");


		var role1 = GameObject.Find ("Role");


		var go = GameObject.CreatePrimitive (PrimitiveType.Capsule);
		go.transform.position = role1.transform.position;
		go.name = "Role_dy";
		go.layer = 12;

		GameObject.DontDestroyOnLoad (go);

		role1.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
