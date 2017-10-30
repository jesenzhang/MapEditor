using UnityEngine;
using UnityEditor;
using System.Collections;

public class NPCCreateWizard : ScriptableWizard {

	public string resId = ""; 
	private Vector3 npcSize = new Vector3(1,1,1.6f);

	void OnWizardUpdate(){
		isValid = true;
		errorString = "";

		if (string.IsNullOrEmpty(resId)) {
			errorString = "Please enter NPC Resource Id";
			isValid = false;
		}

	}
	void OnWizardCreate(){ 
		builder.res = resId;
		builder.shape = SceneObjectShape.Capsule;
		builder.type = SceneObjectType.Npc;
		builder.anchor = SceneObjectAnchor.BOTTOM;
		builder.size = npcSize;

		MapManager.CreateNPC(MapManager.current,builder);
	
	}

	SceneObjectBuilder builder;

}
