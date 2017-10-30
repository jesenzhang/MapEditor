using UnityEngine;
using System.Collections;

public enum FractionType{
	FRIEND = 1,
	EMEMY = 2,
	MIDDLE = -1,
	INVINCIBLE = -2,
}

[ExecuteInEditMode]
public class ChangeFractionAction : LifeEventAction { 

	public FractionType fraction; 

	public override ActionArgValue[] Argments{
		get { 
			args = new ActionArgValue[3];
			args [0].intValue = (int)lifeType; 
			args [1].strValue = lifeId; 
			args [2].intValue = (int)fraction;  
			return args;
		}
	}

	public override string displayName{
		get { 
			return "修改阵营" +lifeId+ "to " +fraction;
		}
	} 
}
