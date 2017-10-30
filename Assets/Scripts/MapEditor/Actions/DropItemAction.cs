using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class DropItemAction : ItemEventAction {  
	public int dropCount; 

	public override ActionArgValue[] Argments{
		get { 
			args = new ActionArgValue[2];
			args [0].strValue = itemObj.itemId;  
			args [1].intValue = dropCount;
			return args;
		}
	} 

	public override string displayName{
		get { 
			return "掉落-" +(itemObj!=null?itemObj.name:"");
		}
	} 
}
