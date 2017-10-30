using UnityEngine;
using System.Collections;

public class ResetWallAction : MapEventAction {
	public MapWall wall; 

	public bool show = true;
	 
	[SerializeField]
	private string objPath;

	protected override void OnUpdate(){
		if (wall != null) {
			objPath = MapEventHelper.GetGameObjectPath (wall.transform);
		} else if(objPath!=null&&wall ==null){
			wall = GameObject.Find (objPath).GetComponent<MapWall> ();
		}
	} 

	public override ActionArgValue[] Argments{
		get { 
			args = new ActionArgValue[2];
			args [0].intValue = wall.wallId; 
			args [1].intValue = show?1:0; 
			return args;
		}
	}

	public override string displayName{
		get { 
			return "激活-" + (wall!=null?wall.name:"");;
		}
	} 
}
