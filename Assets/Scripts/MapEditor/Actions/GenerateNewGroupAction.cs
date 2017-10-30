using UnityEngine;
using System.Collections;

public class GenerateNewGroupAction : MapEventAction {
	public SceneNPCGroup group;  
	 
	[SerializeField]
	private string objPath;

	protected override void OnUpdate(){
		if (group != null) {
			objPath = MapEventHelper.GetGameObjectPath (group.transform);
		} else if(objPath!=null){
			group = GameObject.Find (objPath).GetComponent<SceneNPCGroup> ();
		}
	} 

	public override ActionArgValue[] Argments{
		get { 
			args = new ActionArgValue[1];
			args [0].intValue = group.groupId;  
			return args;
		}
	}

	public override string displayName{
		get { 
			return "生成-" + group.name;
		}
	} 
}
