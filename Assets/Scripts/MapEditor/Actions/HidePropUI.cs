using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
[ExecuteInEditMode]
public class HidePropUI : HideUIAction { 
	
	void Start(){
		uiName = "UI_Props";
	}

	public override string displayName{
		get { 
			return "隐藏功能按钮";
		}
	} 
}
