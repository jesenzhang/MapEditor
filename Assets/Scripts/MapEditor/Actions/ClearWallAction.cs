using UnityEngine;
using System.Collections;

public class ClearWallAction : MapEventAction {
	public MapWall wall; 

	public int key;
	 
	[SerializeField]
	private string objPath;

	protected override void OnUpdate(){
		if (wall != null) {
			objPath = MapEventHelper.GetGameObjectPath (wall.transform);
		} else if(!string.IsNullOrEmpty(objPath)&&wall==null){
			wall = GameObject.Find (objPath).GetComponent<MapWall> ();
		}
	} 

	public override ActionArgValue[] Argments{
		get { 
			args = new ActionArgValue[2];
			args [0].intValue = wall.wallId; 
			args [1].intValue = key; 
			return args;
		}
	}

	public override string displayName{
		get { 
			return "清理-" + (wall!=null?wall.name:"");
		}
	} 
}
