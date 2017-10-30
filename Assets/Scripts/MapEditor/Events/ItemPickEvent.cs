using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ItemPickEvent : MapEvent {  
	public string[] itemIds;

	public override ActionArgValue[] Conditions {
		get { 
			conditions = new ActionArgValue[itemIds.Length];
			for(int i = 0;i<itemIds.Length;++i) {
				conditions [i].intValue = int.Parse(itemIds[i]);  
			}
			return conditions;
		}
	}

	public override string displayName{
		get { 
			return string.Format("拾取道具{0}",string.Join(",",itemIds));
		}
	}  
}
