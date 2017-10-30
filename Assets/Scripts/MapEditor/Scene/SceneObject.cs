using UnityEngine; 
using System.Collections;

public enum SceneObjectType{	
	Npc,
	Tigger,
	Item,
	Transmit,
    PathNode,
}

public enum SceneObjectShape{
	Sphere,
	Box,
	Capsule,
	Circle,
	CyLinder,
}

public enum SceneObjectAnchor{
	BOTTOM,
	CENTER,
	TOP,
}



[ExecuteInEditMode]
public class SceneObject : MonoBehaviour { 
	 
	public int objectId;

	[HideInInspector]
	public SceneObjectType objectType; 

	[HideInInspector]
	public SceneObjectShape objectShape; 

	//[HideInInspector]
	public Vector3 objectSize;

	public Vector3 worldPos;

	public Vector3 worldRotation;

	public virtual string displayName {
		get { 
			return "Obj-"+objectId;
		}
	}

	protected virtual void Update(){
		worldPos = transform.position;
		worldRotation = transform.rotation.eulerAngles;
	}
	 


}
