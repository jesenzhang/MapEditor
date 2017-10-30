using UnityEngine; 
using System.Collections;
 
[ExecuteInEditMode]
public class SceneNPC : SceneObject ,IRecycle{
	
	[HideInInspector]
	[SerializeField]
	public SceneMap map;

	public string resId; 

	public FractionType fraction = FractionType.EMEMY;


	 

	void OnDestroy(){ 

		if (map!=null) {
			map.RemoveNPC (this);
		}
	}

	public int recycleId{
		get { 
			return objectId;
		}
	}

	public override string displayName {
		get { 
			return "NPC-"+objectId;
		}
	}

}
