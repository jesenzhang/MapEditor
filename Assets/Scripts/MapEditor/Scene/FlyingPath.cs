using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FlyingPath : SceneObject,IRecycle { 
	public SceneMap map;

	public override string displayName{
		get { return "飞行路点"+objectId;}
	}

	public int recycleId{
		get { return objectId;}
	}

	void OnDrawGizmos(){ 
		var points = GetComponentsInChildren<FlyingPoint> ();
		for (int i = 0; i < points.Length; ++i) {
			if (i > 0) {
				Gizmos.DrawLine (points[i-1].transform.position,points[i].transform.position);
			}
		}
	}

	void OnDestroy(){ 
		if (map!=null) {
			map.RemoveFlyingPoint (this);
		}
	}
}
