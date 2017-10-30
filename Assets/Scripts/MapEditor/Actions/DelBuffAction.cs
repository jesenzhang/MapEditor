using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class DelBuffAction : LifeEventAction {
	 
	public string buffID; 

	public override ActionArgValue[] Argments{
		get { 
			args = new ActionArgValue[3];
			args [0].intValue = (int)lifeType; 
			args [1].strValue = lifeId; 
			args [2].strValue = buffID;  
			return args;
		}
	}

	public override string displayName{
		get { 
			return "给"+lifeId+"移出BUFF-" +buffID;
		}
	} 
}
