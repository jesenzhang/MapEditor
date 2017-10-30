using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ItemEventAction : MapEventAction {  
	public SceneItem itemObj;

	[HideInInspector]
	public string objPath; 

	protected override void OnUpdate(){ 
		if (itemObj != null) {
			objPath = MapEventHelper.GetGameObjectPath (itemObj.transform); 
		} else if (!string.IsNullOrEmpty (objPath)) {
			itemObj = GameObject.Find (objPath).GetComponent<SceneItem> ();
		}
	}
}
