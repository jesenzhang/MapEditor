using UnityEngine;
using System.Collections.Generic;
using System;

public class ScenePathNode : MonoBehaviour
{
    [HideInInspector]
    [SerializeField]
    public SceneMap map;

    public int nodeID;
    public int Level = 0;
    public List<int> prevNodeIDs = new List<int>();
	public Example.ScenePathNode.PathNodeType type;

	private Renderer render;
	private Material mat;

    private List<ScenePathNode> prevNodes =  new List<ScenePathNode>();

    private void OnDrawGizmos()
    {
		if (map == null) 
		{
			map = transform.parent.parent.gameObject.GetComponent<SceneMap> ();
		}

		if(mat == null)
		{
			render = gameObject.GetComponent<Renderer>();
			if(render != null)
			{
				Material[] arr = render.sharedMaterials;
				if(arr.Length > 0)
				{
					Shader shader = Shader.Find("Standard");
					mat = new Material(shader);
					render.sharedMaterials = new Material[]{mat};
				}
			}
		}

		if(mat != null)
		{
			if(type == Example.ScenePathNode.PathNodeType.PNT_TRANSMIT)
			{
				mat.color = Color.green;
			}
			else if(type == Example.ScenePathNode.PathNodeType.PNT_KEY)
			{
				mat.color = Color.red;
			}
			else
			{
				mat.color = Color.white;
			}
		}

		if (prevNodeIDs.Count >= 0)
		{
			while (prevNodes.Count > prevNodeIDs.Count)
			{
				prevNodes.RemoveAt(prevNodes.Count - 1);
			}

			for (int i = 0, count = prevNodes.Count; i < count; i++)
			{
				ScenePathNode node = prevNodes[i];
				if (node.nodeID != prevNodeIDs[i])
				{
					Transform trans = map.transform.Find("Path/PathNode-" + prevNodeIDs[i]);
					if (trans)
					{
						ScenePathNode prevNode = trans.GetComponent<ScenePathNode>();
						if (prevNode)
							prevNodes[i] = prevNode;
						else
							Debug.LogError("Scene Path Node:" + nodeID + " prevNodeID is invalid->" + prevNodeIDs[i]);
					}
				}
                if (type == node.type && node.type ==Example.ScenePathNode.PathNodeType.PNT_KEY)
                {
                    Gizmos.color = Color.HSVToRGB( Level / 255.0f, 1, 1);
                   
                }
              
                Gizmos.DrawLine(transform.position, prevNodes[i].transform.position);
                Gizmos.color = Color.white;
            }

			while (prevNodes.Count < prevNodeIDs.Count)
			{
				int index = prevNodes.Count;
				//Debug.LogError ("map=>" + (map == null));
				Transform trans = map.transform.Find("Path/PathNode-" + prevNodeIDs[index]);
				if (trans)
				{
					ScenePathNode prevNode = trans.GetComponent<ScenePathNode>();
					if (prevNode)
					{
						prevNodes.Add(prevNode);
						Gizmos.DrawLine(transform.position, prevNode.transform.position);
					}
					else
						Debug.LogError("Scene Path Node:" + nodeID + " prevNodeID is invalid->" + prevNodeIDs[index]);
				}
			}

			/*
			   if (prevNode == null || prevNodeID != prevNode.nodeID)
			   {
			   Transform trans = map.transform.Find("Path/PathNode-" + prevNodeID);
			   if (trans)
			   {
			   prevNode = trans.GetComponent<ScenePathNode>();
			   }
			   }

			   if (prevNode == null)
			   {
			   if (nodeID > 0)
			   Debug.LogError("pathNode prevNodeID must be set a valid value->" + prevNodeID);
			   return;
			   }

			   Gizmos.DrawLine(transform.position, prevNode.transform.position);
			   */
		}
	}
}
