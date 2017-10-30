using UnityEngine; 
using System.Collections;
using System.Collections.Generic;
using System.IO; 
using UnityEditor;

public class MapHelper  { 

    public static void ExportMap(string path,SceneMap map){
		 

		Example.MapUnit mapUnit = new Example.MapUnit (); 
		mapUnit.Id = map.sceneId;
		mapUnit.ResID = map.resId;

		List<Example.NPCUnit> npcUnits = ExportNpcList (map.npcList); 
		mapUnit.Npcs = npcUnits;

		int npcGroupId = 0;
		int lastNpcGroupId = 0;
		Example.NPCGroup npcGroup = null;
		List<Example.NPCGroup> npcGroups = new List<Example.NPCGroup> ();
		foreach (var group in map.GetComponentsInChildren<SceneNPCGroup>()) {
			npcGroupId = group.groupId;
			if (npcGroupId - lastNpcGroupId > 1) {
				for(int i = lastNpcGroupId + 1;i < npcGroupId;++i){
					npcGroup = new Example.NPCGroup ();
					npcGroup.DelayTime = -1;
					npcGroups.Add (npcGroup);
				}
				lastNpcGroupId = npcGroupId;
			}

			npcGroup = new Example.NPCGroup ();
			npcGroup.DelayTime = group.delayTime;
			npcGroup.NextGroup = -1; 

			List<Example.NPCPass> npcPasses = new List<Example.NPCPass> ();
			npcGroup.Passes = npcPasses;

			foreach (var wave in group.GetComponentsInChildren<SceneNPCWave>()) {
				Example.NPCPass pass = new Example.NPCPass ();
				pass.DelayTime = wave.delayTime;  
				List<int> npcIds = new List<int> ();
				foreach (var npc in wave.GetComponentsInChildren<SceneNPC>()) {
					npcIds.Add (npc.objectId);
					//Debug.LogWarningFormat ("Export Group {0} Npc {1}",npcGroupId,npc.objectId);
				}
				pass.Npcs = npcIds;
				npcPasses.Add (pass); 
			}

			npcGroups.Add (npcGroup);
			lastNpcGroupId = npcGroupId;
		}

		List<Example.TransmitNode> transmitNodes = new List<Example.TransmitNode> ();
		int nodeId = 0;
		int lastNodeId = 0;
		Example.TransmitNode transmitNode = null;
		foreach (var transmit in map.GetComponentsInChildren<TransmitNode>()) {
			nodeId = transmit.objectId;
			if (nodeId - lastNodeId > 1) {
				for(int i = lastNodeId + 1;i < nodeId;++i){
					transmitNode = new Example.TransmitNode ();
					transmitNode.Id = -1;
					transmitNodes.Add (transmitNode);
				}
				lastNodeId = nodeId;
			}

			Debug.LogFormat ("transmit.nodeId={0}", transmit.objectId);
			transmitNode = new Example.TransmitNode ();
			transmitNode.Id = transmit.objectId;
			transmitNode.Map = transmit.mapId;
			transmitNode.Node = transmit.nodeId;
			transmitNode.Position = MapUtil.ToVector3f(transmit.transform.position);
			transmitNode.Rotation = MapUtil.ToVector3f(transmit.transform.rotation.eulerAngles);
			transmitNode.Eff = transmit.effectName;

			transmitNodes.Add (transmitNode);
		};

		List<Example.FlyingPath> flyingPaths = new List<Example.FlyingPath> ();
		nodeId = 0;
		lastNodeId = 0;
		Example.FlyingPath flyingPath = null;
		foreach (var flying in map.GetComponentsInChildren<FlyingPath>()) {
			nodeId = flying.objectId;
			if (nodeId - lastNodeId > 1) {
				for(int i = lastNodeId + 1;i < nodeId;++i){
					flyingPath = new Example.FlyingPath ();
					flyingPath.Id = -1;
					flyingPaths.Add (flyingPath);
				}
				lastNodeId = nodeId;
			}

			List<Example.FlyingPoint> points = new List<Example.FlyingPoint> ();
			foreach (var p in flying.GetComponentsInChildren<FlyingPoint>()) {
				List<Example.FlyingAction> actions = new List<Example.FlyingAction> ();
				foreach (var a in p.GetComponentsInChildren<FlyingAction>()) {
					List<string> args = new List<string> ();
					args.Add (a.arg1);
					args.Add (a.arg2);

					var action = new Example.FlyingAction ();
					action.Action = a.action;
					action.Args = args;
					action.Id = a.objectId;
					actions.Add (action);
				}

				var flyingPoint = new Example.FlyingPoint ();
				flyingPoint.Id = p.objectId;
				flyingPoint.Position = MapUtil.ToVector3f (p.transform.position); 
				flyingPoint.Action = actions;
				points.Add (flyingPoint);
			}

			Debug.LogFormat ("path.nodeId={0}", flying.objectId);
			flyingPath = new Example.FlyingPath ();
			flyingPath.Id = flying.objectId;  
			flyingPath.Points = points;
			flyingPaths.Add (flyingPath);
		};

		 
		mapUnit.TransmitNodes = transmitNodes;
		mapUnit.Groups = npcGroups;
        mapUnit.FlyingPaths = flyingPaths;
        //pathnode
        Transform pathParent = map.transform.Find("Path");
        if (pathParent != null)
        {
            List<Example.ScenePathNode> list = new List<Example.ScenePathNode>();
            ScenePathNode[] nodes = pathParent.GetComponentsInChildren<ScenePathNode>();
            List<List<Example.ScenePathNodeContext>> prevLists = new List<List<Example.ScenePathNodeContext>>();
            if (nodes != null && nodes.Length > 0)
            {
                List<Example.ScenePathNodeContext> prevList = null;
                for (int i = 0, count = nodes.Length; i < count; i++)
                {
                    ScenePathNode node = nodes[i];
                    Example.ScenePathNode dataNode = new Example.ScenePathNode();
                    dataNode.Id = node.nodeID;
					dataNode.Pos = MapUtil.Vector3ToVector3f(node.transform.position);
                    //dataNode.PrevNodes = new List<Example.ScenePathNodeContext>();
                    dataNode.AdjacentNodes = new List<Example.ScenePathNodeContext>();
					dataNode.Type = node.type;

                    if (dataNode.Id >= prevLists.Count)
                    {
                        for (int j = prevLists.Count; j <= dataNode.Id; j++)
                            prevLists.Add(new List<Example.ScenePathNodeContext>());
                    }

                    prevList = prevLists[dataNode.Id];

                    for (int j = 0, jcount = node.prevNodeIDs.Count; j < jcount; j++)
                    {
                        Example.ScenePathNodeContext context = new Example.ScenePathNodeContext();
                        context.Cid = node.prevNodeIDs[j];
                        //dataNode.PrevNodes.Add(context);
                        prevList.Add(context);
                    }
					for (int j = list.Count; j <= node.nodeID; j++)
					{
						Example.ScenePathNode nn = new Example.ScenePathNode ();
						nn.Id = -1;
						list.Add(nn);
					}
                    list[node.nodeID] = dataNode;
                }
            }

            for (int i = 0, count = list.Count; i < count; i++)
            {
                Example.ScenePathNode node = list[i];
				if (node != null && node.Id >= 0)
                {
                    List<Example.ScenePathNodeContext> prevList = prevLists[node.Id];
                    //if (node.PrevNodes.Count == 0)
                    if(prevList.Count == 0)
                    {
                        if (node.Id > 0)
                        {
                            Debug.LogError("scene path node:" + node.Id + " prev node is invalid 0");
                            return;
                        }
                    }

                    int prevID = -1;
                    //for (int j = 0, jcount = node.PrevNodes.Count; j < jcount; j++)
                    for (int j = 0, jcount = prevList.Count; j < jcount; j++)
                    {
                        //Example.ScenePathNodeContext c1 = node.PrevNodes[j];
                        Example.ScenePathNodeContext c1 = prevList[j];
                        prevID = c1.Cid;

                        if (prevID < 0)
                        {
                            if (node.Id > 0)
                            {
                                Debug.LogError("scene path node:" + node.Id + " prev node is invalid 1->" + prevID);
                                return;
                            }
                        }

                        if (prevID >= 0)
                        {
                            if (prevID >= list.Count)
                            {
                                Debug.LogError("scene path node:" + node.Id + " prev node is invalid 1->" + prevID + "^" + list.Count);
                                return;
                            }

                            Example.ScenePathNode temp = list[prevID];
							if (temp == null || temp.Id < 0)
                            {
                                Debug.LogError("scene path node:" + node.Id + " prev node is invalid 2");
                                return;
                            }

                            //if (temp.NextNodes == null)
                            //    temp.NextNodes = new List<Example.ScenePathNodeContext>();

                            Example.ScenePathNodeContext context = new Example.ScenePathNodeContext();
                            context.Cid = node.Id;
							context.Cost = Vector3.Distance(MapUtil.Vector3fToVector3(node.Pos), MapUtil.Vector3fToVector3(temp.Pos));
                            c1.Cost = context.Cost;
                            //temp.NextNodes.Add(context);
                            temp.AdjacentNodes.Add(context);
                        }
                    }

                    node.AdjacentNodes.AddRange(prevList);
                }
            }

			/*
            for (int i = 0, count = list.Count; i < count; i++)
            {
                Example.ScenePathNode node = list[i];
                Debug.Log("-----log node----->" + node.Id);
                for (int j = 0, jcount = node.AdjacentNodes.Count; j < jcount; j++)
                {
                    Debug.Log("adjacent->" + j + "^" + node.AdjacentNodes[j].Cid);
                }
                }*/


            if (mapUnit.ScenePath == null)
            {
                mapUnit.ScenePath = new Example.ScenePath();
                mapUnit.ScenePath.Nodes = new List<Example.ScenePathNode>();
            }
            mapUnit.ScenePath.Nodes.AddRange(list);
        }

        //var bytes = MapInfo.SerializeToBytes (mapInfo);
        var bytes = Example.MapUnit.SerializeToBytes(mapUnit);

        FileStream fs = new FileStream(path, FileMode.Create);
        fs.Write(bytes, 0, bytes.Length);
        fs.Close();

        /*
        using (fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.None))
        {
            byte[] arr = new byte[fs.Length];
            fs.Read(arr, 0, (int)fs.Length);
            MemoryStream ms = new MemoryStream(arr);
            Example.MapUnit m = Example.MapUnit.Deserialize(ms);

            for (int i = 0, count = m.ScenePath.Nodes.Count; i < count; i++)
            {
                Example.ScenePathNode node = m.ScenePath.Nodes[i];
                Debug.Log("-------------node-------------->" + node.Id);
                if (node != null)
                {
                    for (int j = 0, jcount = node.AdjacentNodes.Count; j < jcount; j++)
                    {
                        Example.ScenePathNodeContext context = node.AdjacentNodes[j];
                        Debug.Log("adjacent context------------>" + context.Cid + "^" + context.Cost);
                    }
                }
            }
        }
        */

        /*var prefab = PrefabUtility.GetPrefabParent(map.gameObject);
        PrefabUtility.ReplacePrefab(map.gameObject, prefab);*/
    }

    private static List<Example.NPCUnit> ExportNpcList(List<SceneNPC> npcList)
    {
        npcList.Sort(delegate (SceneNPC x, SceneNPC y)
        {
            return x.objectId - y.objectId;
        });

        List<Example.NPCUnit> npcUnits = new List<Example.NPCUnit>();
        int lastNpcId = -1;
        int npcId = 0;
        int resId = -1;
        Example.NPCUnit npcUnit = null;
        foreach (var npc in npcList)
        {
            npcId = npc.objectId;
            if (npcId - lastNpcId > 1)
            {
                for (int i = lastNpcId + 1; i < npcId; ++i)
                {
                    npcUnit = new Example.NPCUnit();
                    npcUnit.ResID = -1;
                    npcUnits.Add(npcUnit);
                }
                lastNpcId = npc.objectId;
            }

            resId = MapManager.FindNPCId(npc.resId);
            if (resId < 0)
            {
                Debug.LogErrorFormat("{0} 资源ID {1} 不存在", npc.name, npc.resId);
            }
            npcUnit = new Example.NPCUnit();
            npcUnit.NpcID = npcId;
            npcUnit.ResID = resId;
			npcUnit.Position = MapUtil. ToVector3f(npc.transform.position - new Vector3(0,1,0));
			npcUnit.Rotation = MapUtil.ToVector3f(npc.transform.rotation.eulerAngles);
			npcUnit.SelfFaction = (int)npc.fraction;
            npcUnit.FriendFaction = 0;

			var bornAnimation = npc.GetComponentInChildren<BornAnimation> ();
			var dieAnimation = npc.GetComponentInChildren<DieAnimation> ();

			npcUnit.BornAnimation = MapAnimationHelper.ExportMapAnimation (bornAnimation,AnimationFrameAppendPolicy.AFTER);
			npcUnit.DieAnimation = MapAnimationHelper.ExportMapAnimation (dieAnimation,AnimationFrameAppendPolicy.AFTER);

            var group = npc.transform.parent.parent.GetComponent<SceneNPCGroup>();
            npcUnit.NpcGroupId = group.groupId;

            npcUnits.Add(npcUnit);
            lastNpcId = npc.objectId;

        }
        return npcUnits;
    }

	static string AddCSVColumn(string value){
		return "\"" + value + "\"";
	}

	static string[] columns  = {"MapID","ResID","npcID","Position"};

	public static void ExportMapNPCIDS(string path,SceneMap map){ 
		List<string> lines = new List<string> ();
		var line = new List<string> ();
		lines.Add (string.Join(",",columns));
		foreach (var npc in map.npcList) {
			line.Add (AddCSVColumn(map.sceneId.ToString()));
			line.Add (AddCSVColumn(npc.resId));
			line.Add (AddCSVColumn(npc.objectId.ToString()));
			line.Add (AddCSVColumn(npc.transform.position.ToString()));
			lines.Add (string.Join(",",line.ToArray()));
			line.Clear ();
		}
		File.WriteAllLines (path, lines.ToArray());
	}

    

    public static Dictionary<string, int> ImportNPCAtt(string path)
    {
        FileStream fs = new FileStream(path, FileMode.Open);
        Example.AllNPCs allNpcs = Example.AllNPCs.Deserialize(fs);
        fs.Close();

        Dictionary<string, int> npcIDs = new Dictionary<string, int>();
        int index = 0;
        foreach (var id in allNpcs.StringID.Ids)
        {
            npcIDs.Add(id.Id, index++);
        }
        return npcIDs;
    }



	public static void CheckSetting(){
		MapUtil.UPDATE_DIR = PlayerPrefs.GetString ("DataRoot", MapUtil.UPDATE_DIR);
		if (string.IsNullOrEmpty(MapUtil.UPDATE_DIR)) {
			ScriptableWizard.DisplayWizard<MapEditorSetting>("设置地图导出目录","保存"); 
		}
	}

}
