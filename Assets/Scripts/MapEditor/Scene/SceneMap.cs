using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class SceneMap : MonoBehaviour {

	public int sceneId; 
	public string resId;

	[SerializeField]
	//[HideInInspector]
	private List<SceneNPC> npcs = new List<SceneNPC>(); 

	[SerializeField]
	//[HideInInspector]
	private List<TransmitNode> transmitNodes = new List<TransmitNode>();

	[SerializeField]
	//[HideInInspector]
	private List<FlyingPath> flyingPaths = new List<FlyingPath>();


	public SceneNPC GetNPC(int id){
		foreach (var npc in npcs) {
			if (npc.objectId == id) {
				return npc;
			}
		}
		return null;
	}

	public void AddNPC(SceneNPC item){
		item.map = this;
		npcs.Add (item);
	}

	public void RemoveNPC(SceneNPC item){
		item.map = null;
		npcs.Remove (item);
	}

	public void ClearNPCS(){ 
		npcs.Clear ();
	}

	public void AddTransmitNode(TransmitNode node){
		node.map = this;
		transmitNodes.Add (node);
	}

	public void RemoveTransmitNode(TransmitNode node){
		node.map = this;
		transmitNodes.Remove (node);
	}

	public void AddFlyingPoint(FlyingPath node){
		node.map = this;
		flyingPaths.Add (node);
	}

	public void RemoveFlyingPoint(FlyingPath node){
		node.map = this;
		flyingPaths.Remove (node);
	}
	 

	public List<SceneNPC> npcList{
		get { 
			return npcs;
		}
	} 

	public List<TransmitNode> transmitList{
		get { 
			return transmitNodes;
		}
	} 

	public List<FlyingPath> flyingPathList{
		get { 
			return flyingPaths;
		}
	} 

	public string displayName {
		get { 
			return "场景-"+sceneId;
		}
	}
}
