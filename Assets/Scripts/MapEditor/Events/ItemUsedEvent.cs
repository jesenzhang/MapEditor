using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ItemUsedEvent : MapEvent {  
	public string[] itemIds;

	public bool right = true;

	public override ActionArgValue[] Conditions {
		get { 
			conditions = new ActionArgValue[itemIds.Length+1]; 
			for(int i = 0;i<itemIds.Length;++i) {
				conditions [i].intValue = int.Parse(itemIds[i]);  
			}
			conditions [itemIds.Length].intValue = right ? 1 : 0;
			return conditions;
		}
	}

	public override string displayName{
		get { 
			return string.Format("使用道具{0}",string.Join(",",itemIds));
		}
	}  
}
