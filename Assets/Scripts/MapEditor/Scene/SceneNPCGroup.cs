using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
 
[ExecuteInEditMode]
public class SceneNPCGroup : MonoBehaviour,IRecycle  {
	 
	public int groupId;
	public int delayTime = 0;
	public int nextGroupId = -1; 

	 
	public string displayName{
		get { 
			return "组-"+groupId;
		}
	}

	public int recycleId{
		get { 
			return groupId;
		}
	} 
	 
}
