using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FlyingPoint : MonoBehaviour {
	public int objectId;

	void Update () {
		gameObject.name = "Point-" + objectId;
	}
}
