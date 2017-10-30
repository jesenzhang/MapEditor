using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class NPCGroupDieEvent : MapEvent { 
	public SceneNPCGroup group = null;
	public int groupid = -1;
	public int dieCount = 1;  

	private string objPath ;

	protected override void OnUpdate(){ 
		if (group != null) {
			objPath = MapEventHelper.GetGameObjectPath (group.transform);
			groupid = group.groupId;
		} else if (!string.IsNullOrEmpty (objPath)) {
			group = GameObject.Find (objPath).GetComponent<SceneNPCGroup> ();
		} else {
			group = FindGroup (groupid);
		}
	}

	public override ActionArgValue[] Conditions {
		get { 
			conditions = new ActionArgValue[2];
			conditions [0].intValue = groupid;  
			conditions [1].intValue = dieCount;  
			return conditions;
		}
	}

	public override string displayName{
		get { 
			return string.Format("{0}死亡数量>={1}",group.name,dieCount);
		}
	}  

	SceneNPCGroup FindGroup(int groupId){
		var groups = GameObject.FindObjectOfType<SceneMap>().GetComponentsInChildren<SceneNPCGroup> ();
		foreach (var group in groups) {
			if (group.groupId == groupId) {
				return group;
			}
		}
		return null;
	}
}
