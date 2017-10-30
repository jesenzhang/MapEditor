using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GameObjectAction : MapEventAction {
	public GameObject obj;

	[HideInInspector]
	public string objPath;  

	protected override void OnUpdate(){
		if (obj != null) {
			objPath = MapEventHelper.GetGameObjectPath (obj.transform); 
		} else if (!string.IsNullOrEmpty (objPath) && obj == null) {
			obj = GameObject.Find (objPath);
		}
	} 
}
