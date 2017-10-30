using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class TransmitNode : SceneObject,IRecycle {
	public int mapId;
	public int nodeId;

	[HideInInspector]
	[SerializeField]
	public SceneMap map;

	public string effectName;

	public int recycleId{
		get { return objectId;}
	}

	public override string displayName{
		get { return objectId==0?"出生点":"跳转点"+objectId;}
	}
		

	void OnDestroy(){ 
		if (map!=null) {
			map.RemoveTransmitNode (this);
		}
	}
}
