using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class MapManager  {
	private static MapManager _instance;

	public static MapManager instance{
		get { 
			if (_instance == null) {
				_instance = new MapManager ();
			}
			return _instance;
		}
	}

	private static Bounds _terrianBounds;   

	private static SceneMap _current;

	private static SceneNPCGroup _selectionGroup;

	private static SceneNPCWave _selectionWave;

	private static SceneNPC _selectionNPC ;

	private static Vector3 _selectionPosition;  

	private static bool _isUpdated = true;

	public static void InitMapEditor(){
		Vector3 min = new Vector3(0,0,0);
		Vector3 max = new Vector3(1,1,1);

		var meshObjects = GameObject.FindObjectsOfType<MeshRenderer> ();
		foreach (var meshRenderer in meshObjects) {
			if (meshRenderer.gameObject.name == "Col_Map")
				continue;

			if (meshRenderer.gameObject.layer == LayerMask.NameToLayer ("Sky"))
				continue;

			if (meshRenderer.gameObject.name.ToLower().IndexOf ("tree") >= 0) {
				if (meshRenderer.gameObject.layer == LayerMask.NameToLayer ("Surface")){
					meshRenderer.gameObject.layer = LayerMask.NameToLayer ("Default");
				}
				var col = meshRenderer.GetComponent<Collider> ();
				if (col != null) {
					Object.DestroyImmediate (col);
				}
				continue;
			}
			meshRenderer.gameObject.layer = LayerMask.NameToLayer ("Surface");
			
			if (meshRenderer.GetComponent<Collider> () == null) {
				Debug.Log ("addcoll "+meshRenderer.name);
				meshRenderer.gameObject.AddComponent<MeshCollider> (); 
			}
			if (meshRenderer.GetComponent<SceneExt> () == null) {
				meshRenderer.gameObject.AddComponent<SceneExt> (); 
			}
			if (meshRenderer.bounds.min.x < min.x) {
				min.x = meshRenderer.bounds.min.x;
			}
			if (meshRenderer.bounds.min.y < min.y) {
				min.y = meshRenderer.bounds.min.y;
			}
			if (meshRenderer.bounds.min.z < min.z) {
				min.z = meshRenderer.bounds.min.z;
			}

			if (meshRenderer.bounds.max.x > max.x) {
				max.x = meshRenderer.bounds.max.x;
			}
			if (meshRenderer.bounds.max.y > max.y) {
				max.y = meshRenderer.bounds.max.y;
			}
			if (meshRenderer.bounds.max.z > max.z) {
				max.z = meshRenderer.bounds.max.z;
			}
		}

		Debug.LogFormat ("Bounds {0},{1},Center {2}",min,max,(min+max)/2);

		MapManager.terrianBounds = new Bounds ((min+max)/2, max - min); 

		LoadData ();
	}

	public static void LoadData(){
		MapManager.ImportNpcAtt (Application.dataPath +"/Res/Data/NPCAtt.bytes");
	}
	 
	public static SceneMap CreateSceneMap(int sceneId,string resId){ 		 
		var go = new GameObject ("场景-"+sceneId);
		var sceneMap = go.AddComponent<SceneMap> (); 
		sceneMap.sceneId = sceneId;
		sceneMap.resId = resId; 

		go.transform.position = MapManager.terrianBounds.center;
		go.transform.localScale = Vector3.one;
		go.transform.rotation = Quaternion.identity;

		Selection.activeGameObject = go;

		return sceneMap;
	}

	private static GameObject _cylinder;

	public static GameObject cylinder{
		get { 
			if (_cylinder == null) {
				_cylinder = GameObject.CreatePrimitive (PrimitiveType.Capsule);
				_cylinder.SetActive (false);
			}
			return _cylinder;
		}
	}
    public static List<ScenePathNode> CreateRectNodes(SceneMap map, int startID,float l, float a, Vector3 up,Vector3 startPos,Vector3 endPos,int M,int N)
    { 
        List<ScenePathNode> nodelist = new List<ScenePathNode>();
        Vector3 dir = (endPos - startPos);
        float length = dir.magnitude;
        dir = dir.normalized;
        Vector3 crossDir = Vector3.Cross(dir, up).normalized; 
        List<Vector3> startposlist = new List<Vector3>();
        for (int i = 0; i < M; i++)
        {
            int index =  M/2 -i;
            Vector3 pos = startPos + index*crossDir * a;
            startposlist.Add(pos);
        }
        Transform parent = map.transform.Find("Path");
        if (parent == null)
        {
            GameObject temp = new GameObject();
            temp.name = "Path";
            parent = temp.transform;
            parent.parent = map.transform;
            parent.localScale = Vector3.one;
            parent.localPosition = Vector3.zero;
            parent.localRotation = Quaternion.identity;
        }
        for (int i = 0; i <= N; i++)
        {
            float per = i / (float)N;
            if (M > 1)
            {
                for (int j = 0; j <= M - 1; j++)
                {
                    List<int> prevNodeIDs = new List<int>();
                    int nodeID = startID + i * M + j;
                    int preid = startID + (i - 1) * M + j;
                    int preid1 = startID + (i - 1) * M + j+1;
                    int preid2 = startID + (i - 1) * M + j - 1;
                    int nodeID1 = nodeID+1;
                    int nodeID2 = nodeID - 1;
                    int afterid = startID + (i +1) * M + j;
                    int afterid1 = startID + (i + 1) * M + j + 1;
                    int afterid2 = startID + (i + 1) * M + j - 1;

                    int last = startID + (N + 1) * M - 1;
                    int prestart = startID + (i-1) * M;
                    int preend = startID + i * M-1;
                    int afterstart = startID + (i+1) * M;
                    int afterend = startID + (i+2) * M - 1;


                    if (preid>=0 && preid >= prestart && preid <= preend)
                    {
                        prevNodeIDs.Add(preid);
                    }
                    if (preid1 >= 0 && preid1 >= prestart && preid1 <= preend)
                    {
                        prevNodeIDs.Add(preid1);
                    }
                    if (preid2 >= 0 && preid2 >= prestart && preid2 <= preend)
                    {
                        prevNodeIDs.Add(preid2);
                    }
                    if (nodeID1 >= 0 && nodeID1 >= startID + i * M && nodeID1 <= startID + (i+1) * M - 1)
                    {
                        prevNodeIDs.Add(nodeID1);
                    }
                    if (nodeID2 >= 0 && nodeID2 >= startID + i * M && nodeID2 <= startID + (i + 1) * M - 1)
                    {
                        prevNodeIDs.Add(nodeID2);
                    }

                    if (afterid >= 0 && afterid<= last && afterid >= afterstart && afterid <= afterend)
                    {
                        prevNodeIDs.Add(afterid);
                    }
                    if (afterid1 >= 0 && afterid1 <= last && afterid1 >= afterstart && afterid1 <= afterend)
                    {
                        prevNodeIDs.Add(afterid1);
                    }
                    if (afterid2 >= 0 && afterid2 <= last  && afterid2 >= afterstart && afterid2 <= afterend)
                    {
                        prevNodeIDs.Add(afterid2);
                    }
                    /*
                    if (i > 0)
                    {
                        for (int k = 0; k <= M - 1; k++)
                        {
                            prevNodeIDs.Add(startID + (i - 1) * M + k);
                        }
                    }
                    if (j == 0)
                    {
                        if (!prevNodeIDs.Contains(nodeID + 1))
                            prevNodeIDs.Add(nodeID + 1);
                    }
                    else if (j == M - 1)
                    {
                        if (!prevNodeIDs.Contains(nodeID - 1))
                            prevNodeIDs.Add(nodeID - 1);
                    }
                    else
                    {
                        if (!prevNodeIDs.Contains(nodeID + 1))
                            prevNodeIDs.Add(nodeID + 1);
                        if (!prevNodeIDs.Contains(nodeID - 1))
                            prevNodeIDs.Add(nodeID - 1);
                    }*/

                    GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    go.name = "PathNode-" + nodeID;
                    ScenePathNode node = go.AddComponent<ScenePathNode>();
                    node.nodeID = nodeID;
                    node.prevNodeIDs = new List<int>();
                    if (prevNodeIDs != null && prevNodeIDs.Count > 0)
                        node.prevNodeIDs.AddRange(prevNodeIDs);

                    node.map = map;
                    go.transform.parent = parent;
                    go.transform.localScale = Vector3.one;
                    go.transform.localPosition = Vector3.zero;
                    go.transform.localRotation = Quaternion.identity;
                    go.transform.position = startposlist[j] + per * length * dir;
                    nodelist.Add(node);
                }
            }
            else if (M == 1)
            {
                List<int> prevNodeIDs = new List<int>();
                int nodeID = startID + i;
                if (i > 0)
                {  prevNodeIDs.Add(startID + (i - 1));
                }

                GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                go.name = "PathNode-" + nodeID;
                ScenePathNode node = go.AddComponent<ScenePathNode>();
                node.nodeID = nodeID;
                node.prevNodeIDs = new List<int>();
                if (prevNodeIDs != null && prevNodeIDs.Count > 0)
                    node.prevNodeIDs.AddRange(prevNodeIDs);

                node.map = map;
                go.transform.parent = parent;
                go.transform.localScale = Vector3.one;
                go.transform.localPosition = Vector3.zero;
                go.transform.localRotation = Quaternion.identity;
                go.transform.position = startposlist[0] + per * length * dir;
                nodelist.Add(node);
            }
               
        }
        return nodelist;
    }
    public static ScenePathSetting CreateScenePathSetting(SceneMap map)
    {
        var pathID = 0;
        var go = new GameObject("ScenePathSetting-" + pathID);
        var obj = go.AddComponent<ScenePathSetting>();

        go.transform.SetParent(map.transform);
        go.transform.position = selectionPosition;
        go.transform.localScale = Vector3.one;
        go.transform.rotation = Quaternion.identity;

        if (obj.StartPoint == null)
            obj.StartPoint = GameObject.CreatePrimitive(PrimitiveType.Cube);
        if (obj.EndPoint == null)
            obj.EndPoint = GameObject.CreatePrimitive(PrimitiveType.Cube);
        obj.StartPoint.name = "StartPoint";
        obj.EndPoint.name = "EndPoint";
        obj.StartPoint.transform.SetParent(obj.transform);
        obj.EndPoint.transform.SetParent(obj.transform);
        Selection.activeGameObject = go;

        return obj;
    }

    public static ScenePathNode CreatePathNode(SceneMap map, int nodeID, List<int> prevNodeIDs)
    {
        Transform parent= map.transform.Find("Path");
        if (parent == null)
        {
            GameObject temp = new GameObject();
            temp.name = "Path";
            parent = temp.transform;
            parent.parent = map.transform;
            //GameBase.GameCommon.ResetTrans(parent);
			parent.localScale = Vector3.one;
			parent.localPosition = Vector3.zero;
			parent.localRotation = Quaternion.identity;
        }

        if (parent == null)
            return null;

        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        go.name = "PathNode-" + nodeID;

        ScenePathNode node = go.AddComponent<ScenePathNode>();
        node.nodeID = nodeID;
        node.prevNodeIDs = new List<int>();
        if(prevNodeIDs != null && prevNodeIDs.Count > 0)
            node.prevNodeIDs.AddRange(prevNodeIDs);
        node.map = map;

        go.transform.parent = parent;
		go.transform.localScale = Vector3.one;
		go.transform.localPosition = Vector3.zero;
		go.transform.localRotation = Quaternion.identity; 
        go.transform.position = selectionPosition;

        _isUpdated = true;

        Selection.activeGameObject = go;
        
        return null;
    }

    public static SceneNPC CreateNPC(SceneMap map, SceneObjectBuilder builder)
    {
        var npcId = AutoIncrementGenerator.Get(MapManager.current.npcList);
        //var go = new GameObject ("NPC-"+npcId);
        var go = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        go.name = "NPC-" + npcId;
        var obj = go.AddComponent<SceneNPC>();
        obj.objectId = npcId;
        obj.resId = builder.res;
        obj.objectType = builder.type;
        obj.objectShape = builder.shape;
        obj.objectSize = builder.size;

        //var offset = CaculatePostionOffset (builder);
        //CreateCollider (go, builder);

        var offset = new Vector3(0, obj.objectSize.y, 0);

        var parent = map.transform;
        if (MapManager.selectionWave != null)
        {
            parent = MapManager.selectionWave.transform;
        }
        else if (MapManager.selectionGroup != null)
        {
            parent = MapManager.selectionGroup.transform;
        }

        go.transform.SetParent(parent);
        go.transform.position = selectionPosition + offset;
        go.transform.localScale = Vector3.one;
        go.transform.rotation = Quaternion.identity;

        map.AddNPC(obj);

        _isUpdated = true;

        Selection.activeGameObject = go;

        return obj;
    }

    public static void ResetNPC(SceneNPC npc)
    {
        Ray ray = new Ray(npc.transform.position, Vector3.down);
        RaycastHit hitInfo;
        //if (Physics.Raycast(ray, out hitInfo, 10000, LayerMask.GetMask(new string[] { "Surface" })))
		if (Physics.Raycast(ray, out hitInfo))
        {
            npc.gameObject.transform.position = hitInfo.point + new Vector3(0, npc.objectSize.y, 0);
        }
    }

    private static Vector3 CaculatePostionOffset(SceneObjectAnchor anchor, SceneObjectShape shape, Vector3 size)
    {
        Vector3 offset = new Vector3(0, 0, 0);
        switch (anchor)
        {
            case SceneObjectAnchor.BOTTOM:
                switch (shape)
                {
                    case SceneObjectShape.Box:
                        offset.y = size.y / 2;
                        break;
                    case SceneObjectShape.Capsule:
                        offset.y = size.y / 2;
                        break;
                    case SceneObjectShape.Sphere:
                        offset.y = size.x / 2;
                        break;
                }
                break;
            case SceneObjectAnchor.TOP:
                switch (shape)
                {
                    case SceneObjectShape.Box:
                    case SceneObjectShape.Capsule:
                        offset.y = -size.y;
                        break;
                    case SceneObjectShape.Sphere:
                        offset.y = -size.x;
                        break;
                }
                break;
        }

        return offset;
    }

    private static Collider CreateCollider(GameObject go, SceneObjectShape shape, Vector3 size)
    {
        Collider collider = null;
        switch (shape)
        {
            case SceneObjectShape.Box:
                var boxCollider = go.AddComponent<BoxCollider>();
                boxCollider.size = new Vector3(size.x, size.y, size.z);
                break;
            case SceneObjectShape.Sphere:
                var sphereCollider = go.AddComponent<SphereCollider>();
                sphereCollider.radius = size.x / 2;
                break;
            case SceneObjectShape.Capsule:
                var capsuleCollider = go.AddComponent<CapsuleCollider>();
                capsuleCollider.radius = size.x / 2;
                capsuleCollider.height = size.y;
                break;
        }
        return collider;
    }



    public static void CreateNPCGroup(SceneMap map, int delayTime)
    {
        int groupId = AutoIncrementGenerator.Get(map.GetComponentsInChildren<SceneNPCGroup>());
        var go = new GameObject("组-" + groupId);
        var group = go.AddComponent<SceneNPCGroup>();
        group.groupId = groupId;
        group.delayTime = delayTime;

        go.transform.SetParent(map.transform);
        go.transform.position = selectionPosition;
        go.transform.localScale = Vector3.one;
        go.transform.rotation = Quaternion.identity;

        Selection.activeGameObject = go;

        _isUpdated = true;
    }

    public static void RemoveNPCGroup(SceneNPCGroup group)
    {
        GameObject.DestroyImmediate(group.gameObject);
        _isUpdated = true;
    }

    public static void CreateNPCWave(SceneNPCGroup group, int delayTime)
    {
        int sequene = group.transform.childCount + 1;
        var go = new GameObject("波次-" + sequene);
        var wave = go.AddComponent<SceneNPCWave>();
        wave.waveSequence = sequene;
        wave.delayTime = delayTime;

        go.transform.SetParent(group.transform);
        go.transform.position = selectionPosition;
        go.transform.localScale = Vector3.one;
        go.transform.rotation = Quaternion.identity;

        _isUpdated = true;

        Selection.activeGameObject = go;
    }

    public static void RemoveNPCWave(SceneNPCWave wave)
    {
        GameObject.DestroyImmediate(wave.gameObject);
        _isUpdated = true;
    }

    public static void RemoveNPC(SceneNPC npc)
    {
        GameObject.DestroyImmediate(npc.gameObject);
        _isUpdated = true;
    }

    public static void CreateTransmitNode(SceneMap map, int mapId, int nodeId, Vector3 nodeSize)
    {
		int transmitId = AutoIncrementGenerator.Get(map.transmitList);
		CreateTransmitNode (map,transmitId,mapId,nodeId,nodeSize);
    }

	public static void CreateTransmitNode(SceneMap map,int transmitId, int mapId, int nodeId, Vector3 nodeSize)
	{
		var go = new GameObject((transmitId==0?"出生点-":"跳转点-") + transmitId);
		var node = go.AddComponent<TransmitNode>();
		node.mapId = mapId;
		node.nodeId = nodeId;
		node.objectId = transmitId;
		node.objectSize = nodeSize;
		node.objectShape = SceneObjectShape.Circle;
		node.objectType = SceneObjectType.Transmit;

		go.transform.SetParent(map.transform);
		go.transform.position = selectionPosition;
		go.transform.localScale = Vector3.one;
		go.transform.rotation = Quaternion.identity;

		map.AddTransmitNode(node);

		_isUpdated = true;

		Selection.activeGameObject = go;
	}


	public static FlyingPath CreateFlyingPath(SceneMap map)
	{
		var pathID = AutoIncrementGenerator.Get(MapManager.current.flyingPathList);
		//var go = new GameObject ("NPC-"+npcId);
		var go = new GameObject("FlyingPath-" + pathID);
		var obj = go.AddComponent<FlyingPath>();
		obj.objectId = pathID; 
		 

		go.transform.SetParent(map.transform);
		go.transform.position = selectionPosition;
		go.transform.localScale = Vector3.one;
		go.transform.rotation = Quaternion.identity;

		map.AddFlyingPoint(obj); 

		Selection.activeGameObject = go;

		return obj;
	}

	public static GameObject CreateFlyingPoint(FlyingPath path)
	{
		var nodeID = path.GetComponentsInChildren<FlyingPoint>().Length + 1;
		//var go = new GameObject ("NPC-"+npcId);
		var go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		go.name = "Node-" + nodeID; 
		go.AddComponent<FlyingPoint> ().objectId = nodeID;
		 

		go.transform.SetParent(path.transform);
		go.transform.position = selectionPosition;
		go.transform.localScale = Vector3.one;
		go.transform.rotation = Quaternion.identity; 

		Selection.activeGameObject = go;

		return go;
	}

	public static GameObject CreateFlyingAction(FlyingPoint point)
	{
		var nodeID = point.GetComponentsInChildren<FlyingAction>().Length + 1;
		//var go = new GameObject ("NPC-"+npcId);
		var go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		go.name = "Action-" + nodeID; 
		go.AddComponent<FlyingAction> ().objectId = nodeID;;


		go.transform.SetParent(point.transform);
		go.transform.localPosition = Vector3.zero;
		go.transform.localScale = Vector3.one;
		go.transform.rotation = Quaternion.identity; 

		Selection.activeGameObject = go;

		return go;
	}



    public static void ExportMap(SceneMap map)
    {
        //string exportDir = Application.dataPath+"/../Export/Data";

		string exportDir = MapUtil.DataPath();
        exportDir += "MapData/";

        if (!Directory.Exists(exportDir))
        {
            Directory.CreateDirectory(exportDir);
        }

        string path = exportDir + "/MapUnit_" + map.sceneId + ".bytes";
        MapHelper.ExportMap(path, map);

		MapHelper.ExportMapNPCIDS (exportDir + "/MapUnit_" + map.sceneId + ".csv", map);

        Debug.LogFormat("导出场景{0}数据到{1}", map.sceneId, path); 
    }

    private static Dictionary<string, int> NPCIDS = new Dictionary<string, int>();

    public static void ImportNpcAtt(string path = "Res/Data/NPCAtt.bytes")
    {
        NPCIDS = MapHelper.ImportNPCAtt(path);

        foreach (var NPCID in NPCIDS)
        {
            Debug.LogFormat("NPCID={0}", NPCID);
        }
    }

    public static int FindNPCId(string strId)
    {
        int value = -1;
        if (!NPCIDS.TryGetValue(strId.Trim(), out value))
        {
            value = -1;
        }
        return value;
    }

	public static string FindNPCResId(int id)
	{ 
		foreach (var item in NPCIDS) {
			if (item.Value == id)
				return item.Key;
		}
		return "";
	}


    public static Bounds terrianBounds
    {
        get
        {
            return _terrianBounds;
        }

        set
        {
            _terrianBounds = value;
        }
    }

    public static SceneMap current
    {
        get
        {
            if (_current == null)
            {
                _current = GameObject.FindObjectOfType<SceneMap>();
            }
            return _current;
        }
    }

    public static SceneNPCGroup selectionGroup
    {
        get
        {
            return _selectionGroup;
        }
        set
        {
            _selectionGroup = value;
        }
    }

    public static SceneNPCWave selectionWave
    {
        get
        {
            return _selectionWave;
        }
        set
        {
            _selectionWave = value;
        }
    }

    public static SceneNPC selectionNPC
    {
        get
        {
            return _selectionNPC;
        }
        set
        {
            _selectionNPC = value;
        }
    }

    public static Vector3 selectionPosition
    {
        get
        {
            return _selectionPosition;
        }
        set
        {
            _selectionPosition = value;
        }
    }

    public static bool isUpdated
    {
        get
        {
            return _isUpdated;
        }
        set
        {
            _isUpdated = value;
        }
    }
}

public struct SceneObjectBuilder
{

    public SceneObjectType type;

    public string res;

    public SceneObjectShape shape;

    public SceneObjectAnchor anchor;

    public Vector3 size;

}
