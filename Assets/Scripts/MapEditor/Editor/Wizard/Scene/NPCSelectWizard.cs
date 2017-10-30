using UnityEngine;
using UnityEditor;
using System.Collections;

public class NPCSelectWizard : ScriptableWizard { 
	
	public int npcId1 = -1;
	public int npcId2 = -1;
	public int npcId3 = -1;
	public int npcId4 = -1;
	public int npcId5 = -1; 
	 
	void OnWizardUpdate(){
		isValid = true;
		errorString = "";		

		if (!CheckNPC ("Npc 1", npcId1))
			return;

		if (!CheckNPC ("Npc 2", npcId2))
			return;
		
		if (!CheckNPC ("Npc 3", npcId3))
			return;

		if (!CheckNPC ("Npc 4", npcId4))
			return;

		if (!CheckNPC ("Npc 5", npcId5))
			return;
	}

	bool CheckNPC(string label,int id){
		if (id == -1)
			return true;
		
		if (MapManager.current.GetNPC (npcId1) == null) {
			errorString =  string.Format ("{0} {1} 不存在",label,npcId1);
			return false;
		}
		errorString = "";
		return true;
	}

	void OnWizardCreate(){ 
		if(npcId1>0)
			AddNPCToWave (npcId1);
		if(npcId2>0)
			AddNPCToWave (npcId2);
		if(npcId3>0)
			AddNPCToWave (npcId3);
		if(npcId4>0)
			AddNPCToWave (npcId4);
		if (npcId5 > 0)
			AddNPCToWave (npcId5);
	}

	void AddNPCToWave(int id){
		var npc = MapManager.current.GetNPC (id);
		if (npc != null)
			npc.transform.parent = MapManager.selectionWave.transform;
	}
}
