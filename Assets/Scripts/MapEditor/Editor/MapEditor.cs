using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class MapEditor : EditorWindow {

	[MenuItem("MapEditor/初始化地形",false,1)]
	static void InitTerrian(){
		MapManager.InitMapEditor ();
	}

	[MenuItem("MapEditor/初始化数据",false,2)]
	static void LoadData(){
		MapManager.LoadData ();
	}

	[MenuItem("MapEditor/创建地图",false,3)]
	static void CreateSceneMap(){
		//var go = new GameObject ("地图编辑器(在这里操作创建npc组和波次)");
		ScriptableWizard.DisplayWizard<SceneCreateWizard> ("创建场景", "创建"); 
	}

	[MenuItem("MapEditor/导出地图数据",false,4)]
	static void ExportMapData(){
		var map = MapManager.current;
		if (map == null) {
			Debug.LogError ("No Map Data");
			return;
		}
		MapManager.ExportMap (map);
		//MapEventManager.ExportEvents (map);
	}	



	[MenuItem("MapEditor/创建地图事件",false,4)]
	static void CreateSceneMapEvent(){ 
		MapEventHelper.GetOrCreateGameObject ("GameEvents");  
		MapEventHelper.GetOrCreateGameObject ("GameTriggers");  
		MapEventHelper.GetOrCreateGameObject ("GameWalls");   
		MapEventHelper.GetOrCreateGameObject ("GameItems"); 
		MapEventHelper.GetOrCreateGameObject ("GameAudios"); 
		MapEventHelper.GetOrCreateGameObject ("GameAreas"); 
	}

	[MenuItem("MapEditor/导出地图事件数据",false,4)]
	static void ExportMapEventData(){ 
		MapEventHelper.ExportEvents (MapManager.current);
	}	

	[MenuItem("MapEditor/生成小地图",false,4)]
	static void GenerateMiniMap(){ 
		MiniMapHelper.GenerateMiniMap();
	}	

	[MenuItem("MapEditor/导出小地图",false,4)]
	static void ExportMiniMap(){ 
		MiniMapHelper.ExportBundle();
	}	

	[MenuItem("MapEditor/Hidden/Scene/创建NPC",false,5)] 
	[MenuItem("MapEditor/Hidden/NPC Wave/创建NPC",false,5)]
	static void CreateNpc(){
		var sceneMap = MapManager.current;
		if (sceneMap == null) {
			CreateSceneMap (); 
		}
		ScriptableWizard.DisplayWizard<NPCCreateWizard>("创建NPC","创建"); 
	}

	[MenuItem("MapEditor/Hidden/Scene/NPC落地",false,5)] 
	[MenuItem("MapEditor/Hidden/NPC/落地",false,5)]
	static void ResetNPC(){
		if (MapManager.selectionNPC == null) {
			return;
		}
		MapManager.ResetNPC (MapManager.selectionNPC);
	}

	[MenuItem("MapEditor/Hidden/NPC/删除NPC",false,5)]
	static void RemoveNpc(){
		if (MapManager.selectionNPC == null) {
			return;
		}
		MapManager.RemoveNPC (MapManager.selectionNPC);
	}

	//[MenuItem("MapEditor/NPC Group/创建NPC组",false,6)]
	[MenuItem("MapEditor/Hidden/Map/创建NPC组",false,6)]
	static void CreateNpcGroup(){
		var sceneMap = MapManager.current;
		if (sceneMap == null) {
			return;
		}
		ScriptableWizard.DisplayWizard<NPCGroupCreateWizard>("创建NPC组","创建"); 
	}

	[MenuItem("MapEditor/Hidden/NPC Group/删除NPC组",false,6)]
	static void RemoveNpcGroup(){ 
		if (MapManager.selectionGroup == null) {
			return;
		}
		MapManager.RemoveNPCGroup (MapManager.selectionGroup);
	}

	[MenuItem("MapEditor/Hidden/NPC Group/创建NPC波次",false,5)] 
	static void CreateNpcWave(){
		var sceneMap = MapManager.current;
		if (sceneMap == null) {
			return;
		}
		ScriptableWizard.DisplayWizard<NPCWaveCreateWizard>("创建NPC波次","创建"); 
	}
	 
	[MenuItem("MapEditor/Hidden/NPC Wave/删除NPC波次",false,7)]
	static void RemoveNpcWave(){
		if (MapManager.selectionWave == null) {
			return;
		}
		MapManager.RemoveNPCWave (MapManager.selectionWave);
	}

	[MenuItem("MapEditor/Hidden/NPC Wave/选择NPC",false,6)]
	static void SelectNPC(){
		if (MapManager.selectionWave == null) {
			return;
		}

		ScriptableWizard.DisplayWizard<NPCSelectWizard>("选择NPC","确定");  
	}

	[MenuItem("MapEditor/Hidden/Scene/创建出生点",false,7)] 
	[MenuItem("MapEditor/Hidden/Map/创建出生点",false,7)] 
	static void CreateTransmitNode(){
		var sceneMap = MapManager.current;
		if (sceneMap == null) {
			return;
		}
		MapManager.CreateTransmitNode (sceneMap,0, -1, -1, new Vector3(3,3,3));
	}

	[MenuItem("MapEditor/Hidden/Scene/创建跳转点",false,8)] 
	[MenuItem("MapEditor/Hidden/Map/创建跳转点",false,8)] 
	static void CreateTransmitBornNode(){
		var sceneMap = MapManager.current;
		if (sceneMap == null) {
			return;
		}
		ScriptableWizard.DisplayWizard<TransmitNodeCreateWizard>("创建跳转点","创建"); 
	}

    [MenuItem("MapEditor/Hidden/Scene/创建路点", false, 9)]
    [MenuItem("MapEditor/Hidden/Map/创建路点", false, 9)]
    static void CreatePathNode()
    {
        SceneMap sceneMap = MapManager.current;
        if (sceneMap == null)
            return;

        ScriptableWizard.DisplayWizard<PathNodeCreateWizard>("创建路点", "创建");
    }
    [MenuItem("MapEditor/Hidden/Scene/创建矩形路点", false, 9)]
    [MenuItem("MapEditor/Hidden/Map/创建矩形路点", false, 9)]
    static void CreateRectPathNode()
    {
        SceneMap sceneMap = MapManager.current;
        if (sceneMap == null)
            return;

        MapManager.CreateScenePathSetting(sceneMap);
    }

    [MenuItem("MapEditor/Hidden/Map/创建飞行路径", false, 10)]
	static void CreateFlyingPath()
	{
		SceneMap sceneMap = MapManager.current;
		if (sceneMap == null)
			return;
		MapManager.CreateFlyingPath (sceneMap);
	}
	 
	[MenuItem("MapEditor/Hidden/FlyingPath/创建飞行路点", false, 11)]
	static void CreateFlyingPathPoint()
	{
		var  go = Selection.activeGameObject;
		if (go == null)
			return;
		var path = go.GetComponent<FlyingPath> ();
		MapManager.CreateFlyingPoint (path);
	}

	[MenuItem("MapEditor/Hidden/FlyingPoint/创建飞行路点动作", false, 11)]
	static void CreateFlyingPathAction()
	{
		var  go = Selection.activeGameObject;
		if (go == null)
			return;
		var path = go.GetComponent<FlyingPoint> ();
		MapManager.CreateFlyingAction (path);
	}


	[MenuItem("MapEditor/Test/MapUnit", false, 100)] 
	static void TestMapUnit()
	{
		var path = EditorUtility.OpenFilePanel ("Select MapUnit", "Export", "bytes");
		MapDebugHelper.PrintMapUnit (path);
	}

	[MenuItem("MapEditor/Test/MapEvent", false, 101)] 
	static void TestMapEvent()
	{
		var path = EditorUtility.OpenFilePanel ("Select MapEvent", "Export", "bytes");
		MapDebugHelper.PrintMapEvent (path);
	}
    //快速排序  
    static  void quick_sort(ScenePathNode[] s, int l, int r)
    {
        if (l < r)
        {
            //Swap(s[l], s[(l + r) / 2]); //将中间的这个数和第一个数交换 参见注1  
            int i = l, j = r, x = s[l].nodeID;
            while (i < j)
            {
                while (i < j && s[j].nodeID >= x) // 从右向左找第一个小于x的数  
                    j--;
                if (i < j)
                    s[i++] = s[j];

                while (i < j && s[i].nodeID < x) // 从左向右找第一个大于等于x的数  
                    i++;
                if (i < j)
                    s[j--] = s[i];
            }
            s[i].nodeID = x;
            quick_sort(s, l, i - 1); // 递归调用   
            quick_sort(s, i + 1, r);
        }
    }
    [MenuItem("MapEditor/Editor/重新排序全部路点", false, 102)]
    static void ReSortNode()
    {
        Transform parent = MapManager.current.transform.Find("Path");
        ScenePathNode[] nodes = parent.gameObject.GetComponentsInChildren<ScenePathNode>();
        int N = nodes.Length;
        quick_sort(nodes, 0, N - 1);

        for (int i = 0; i < N; i++)
        {
            int id = nodes[i].nodeID;
            nodes[i].gameObject.name = "PathNode-" + i;
            for (int j = 0; j < N; j++)
            {
               List<int> indexs = nodes[j].prevNodeIDs;
                for (int k = 0; k < indexs.Count; k++)
                {
                    if (indexs[k] == id)
                    {
                        indexs[k] = i;
                    }
                }
            }
        }
        for (int i = 0; i < N; i++)
        {
            nodes[i].nodeID = i;
        }
    }
    

    [MenuItem("MapEditor/Editor/添加新的独立路点", false, 102)]
    static public void AddNode()
    {
        Transform parent = MapManager.current.transform.Find("Path");
        ScenePathNode[] nodes = parent.gameObject.GetComponentsInChildren<ScenePathNode>();
        int N = nodes.Length;
        quick_sort(nodes, 0, N - 1);
        int id = nodes[N - 1].nodeID+1;
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        go.name = "PathNode-" + id;
        ScenePathNode node = go.AddComponent<ScenePathNode>();
        node.nodeID = id;  
        node.map = MapManager.current;
        go.transform.parent = parent;
        go.transform.localScale = Vector3.one;
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;
        go.transform.position = MapManager.selectionPosition;
    }

    [MenuItem("MapEditor/Editor/连接所选路点", false, 102)]
    static void LinkNodes()
    {
        if (Selection.objects != null && Selection.objects.Length == 2)
        {
            if (Selection.objects[0] is GameObject && Selection.objects[1] is GameObject)
            {
                GameObject node0 = Selection.objects[0] as GameObject;
                ScenePathNode path0= node0.GetComponent<ScenePathNode>(); 
                GameObject node1 = Selection.objects[1] as GameObject;
                ScenePathNode path1 = node1.GetComponent<ScenePathNode>();
                if (path0 && path1)
                {
                    int id0 = path0.nodeID;
                    int id1 = path1.nodeID;
                    if (!path0.prevNodeIDs.Contains(id1))
                        path0.prevNodeIDs.Add(id1);
                    if (!path1.prevNodeIDs.Contains(id0))
                        path1.prevNodeIDs.Add(id0);
                }
                else
                {
                    EditorUtility.DisplayDialog("错误 ", "必须都是ScenePathNode", "我的错");
                }
            }
        }
        else
        {
            EditorUtility.DisplayDialog("错误 ", "必须有且只有两个节点", "我的错");
        }

    }

    [MenuItem("MapEditor/Editor/断开所选路点", false, 102)]
    static void DeleteLinkNodes()
    {
        if (Selection.objects != null && Selection.objects.Length == 2)
        {
            if (Selection.objects[0] is GameObject && Selection.objects[1] is GameObject)
            {
                GameObject node0 = Selection.objects[0] as GameObject;
                ScenePathNode path0 = node0.GetComponent<ScenePathNode>();
                GameObject node1 = Selection.objects[1] as GameObject;
                ScenePathNode path1 = node1.GetComponent<ScenePathNode>();
                if (path0 && path1)
                {
                    int id0 = path0.nodeID;
                    int id1 = path1.nodeID;
                    if (path0.prevNodeIDs.Contains(id1))
                        path0.prevNodeIDs.Remove(id1);
                    if (path1.prevNodeIDs.Contains(id0))
                        path1.prevNodeIDs.Remove(id0);
                }
                else
                {
                    EditorUtility.DisplayDialog("错误 ", "必须都是ScenePathNode", "我的错");
                }
            }
        }
        else
        {
            EditorUtility.DisplayDialog("错误 ", "必须有且只有两个节点", "我的错");
        }

    }
    [MenuItem("MapEditor/Editor/删除所选路点", false, 102)]
    static void DeleteNode()
    {
        if (Selection.objects != null)
        {
            for (int i = 0; i < Selection.objects.Length; i++)
            {
             
                if (Selection.objects[i] is GameObject)
                {
                    GameObject node0 = Selection.objects[i] as GameObject;
                    ScenePathNode node = node0.GetComponent<ScenePathNode>();
                    DeleteOnePathNode(node);
                }
            }
        }

    }

    static void DeleteOnePathNode(ScenePathNode node)
    {
        int id = node.nodeID;
        ScenePathNode[] list = node.gameObject.transform.parent.GetComponentsInChildren<ScenePathNode>();
        for (int j = 0; j < list.Length; j++)
        {
            if (list[j].prevNodeIDs.Contains(id))
            {
                list[j].prevNodeIDs.Remove(id);
            }
        }
        GameObject.DestroyImmediate(node.gameObject);
    }

    [MenuItem("MapEditor/Editor/所选路点贴地/Surface", false, 102)]
    static void DropToSurface0()
    {
        DropToSurface("Surface");
    }
    [MenuItem("MapEditor/Editor/所选路点贴地/Walkable", false, 102)]
    static void DropToSurface1()
    {
        DropToMavMesh();
    }
    static void DropToSurface(string layer)
    {
        if (Selection.objects != null)
        {
            for (int i = 0; i < Selection.objects.Length; i++)
            {
                GameObject node0 = Selection.objects[i] as GameObject;
                ScenePathNode node = node0.GetComponent<ScenePathNode>();
                Vector3 origin = node.gameObject.transform.position;
                RaycastHit hit;
                if (Physics.Raycast(origin, Vector3.down, out hit, 1000, LayerMask.GetMask(layer)))
                {
                    node.gameObject.transform.position = hit.point;
                }
                 
                NavMeshHit Navhit;
                bool blocked = false; 
                blocked = NavMesh.Raycast(origin, origin+ 1000*Vector3.down, out Navhit, 1);
                Debug.DrawLine(origin, origin + 1000 * Vector3.down, blocked ? Color.red : Color.green);
                 Debug.Log("blocked" + blocked + Navhit.position+ Navhit.mask);
                if (blocked)
                    Debug.DrawRay(Navhit.position, Vector3.up, Color.red);
                }
        }
     
    }
    static void DropToMavMesh()
    {
        if (Selection.objects != null)
        {
            List<ScenePathNode> deleteList = new List<ScenePathNode>();
            for (int i = 0; i < Selection.objects.Length; i++)
            {
                GameObject node0 = Selection.objects[i] as GameObject;
                ScenePathNode node = node0.GetComponent<ScenePathNode>();
                Vector3 origin = node.gameObject.transform.position; 
                NavMeshHit Navhit;
                bool blocked = false;
                blocked = NavMesh.Raycast(origin, origin + 1000 * Vector3.down, out Navhit, 1);
                Debug.DrawLine(origin, origin + 1000 * Vector3.down, blocked ? Color.red : Color.green);

                if (blocked)
                {
                    Debug.DrawRay(Navhit.position, Vector3.up, Color.red);
                    deleteList.Add(node); 
                }
                else
                {
                    if(Navhit.position.x != Mathf.Infinity && Navhit.position.y != Mathf.Infinity && Navhit.position.z != Mathf.Infinity)
                        node.gameObject.transform.position = Navhit.position;
                }

            }
            for (int i = 0; i < deleteList.Count; i++)
            {
                DeleteOnePathNode(deleteList[i]);
            }
            deleteList.Clear();
            deleteList = null;
        }

    }
    [PreferenceItem("MapEditor")]
	static void SetExportFolder(){
		EditorGUILayout.LabelField ("Data Export Directory");
		MapUtil.UPDATE_DIR = EditorGUILayout.TextField (PlayerPrefs.GetString("DataRoot",MapUtil.UPDATE_DIR));
		PlayerPrefs.SetString ("DataRoot", MapUtil.UPDATE_DIR);
	}

	[InitializeOnLoadMethod]
	static void StartInitializeOnLoadMethod(){
		//MapManager.InitMapEditor ();
		MapManager.LoadData();
		//MapHelper.CheckSetting ();
		EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyGUI; 
		Selection.selectionChanged += OnSelectionChange;
		SceneView.onSceneGUIDelegate += OnSceneGUI;
	}

	static void OnSceneGUI(SceneView sceneView){ 
		if(MapManager.current!=null)
			SceneViewHelper.DrawSceneMap (MapManager.current);
		SceneViewHelper.ProcessEvent ("MapEditor/Hidden/Scene");
		 
	} 

	static void OnHierarchyGUI(int instanceID,Rect selectionRect){ 

		if (Event.current != null && selectionRect.Contains (Event.current.mousePosition) && Event.current.button == 1 && Event.current.type <= EventType.MouseUp) {
			GameObject selectedGameObject = EditorUtility.InstanceIDToObject (instanceID) as GameObject;
			if (selectedGameObject) {
				var mousePosition = Event.current.mousePosition;
				if (selectedGameObject.GetComponent<SceneMap> () != null) {
					EditorUtility.DisplayPopupMenu (new Rect (mousePosition.x,mousePosition.y,0,0),"MapEditor/Hidden/Map",null);
					Event.current.Use ();
				}else if(selectedGameObject.GetComponent<SceneNPCGroup> () != null){
					EditorUtility.DisplayPopupMenu (new Rect (mousePosition.x,mousePosition.y,0,0),"MapEditor/Hidden/NPC Group",null);
					Event.current.Use ();
				}else if(selectedGameObject.GetComponent<SceneNPCWave> () != null){
					EditorUtility.DisplayPopupMenu (new Rect (mousePosition.x,mousePosition.y,0,0),"MapEditor/Hidden/NPC Wave",null);
					Event.current.Use ();
				}else if(selectedGameObject.GetComponent<SceneNPC> () != null){
					EditorUtility.DisplayPopupMenu (new Rect (mousePosition.x,mousePosition.y,0,0),"MapEditor/Hidden/NPC",null);
					Event.current.Use ();
				}else if(selectedGameObject.GetComponent<FlyingPath> () != null){
					EditorUtility.DisplayPopupMenu (new Rect (mousePosition.x,mousePosition.y,0,0),"MapEditor/Hidden/FlyingPath",null);
					Event.current.Use ();
				}else if(selectedGameObject.GetComponent<FlyingPoint> () != null){
					EditorUtility.DisplayPopupMenu (new Rect (mousePosition.x,mousePosition.y,0,0),"MapEditor/Hidden/FlyingPoint",null);
					Event.current.Use ();
				}
				else if(selectedGameObject.name=="GameItems"){
					EditorUtility.DisplayPopupMenu (new Rect (mousePosition.x,mousePosition.y,0,0),"MapEditor/道具",null);
					Event.current.Use ();
				}else if(selectedGameObject.name=="GameTriggers"){
					EditorUtility.DisplayPopupMenu (new Rect (mousePosition.x,mousePosition.y,0,0),"MapEditor/创建触发器",null);
					Event.current.Use ();
				}else if(selectedGameObject.name=="GameWalls"){
					EditorUtility.DisplayPopupMenu (new Rect (mousePosition.x,mousePosition.y,0,0),"MapEditor/创建墙体",null);
					Event.current.Use ();
				}else if(selectedGameObject.name=="GameAudios"){
					EditorUtility.DisplayPopupMenu (new Rect (mousePosition.x,mousePosition.y,0,0),"MapEditor/音频",null);
					Event.current.Use ();
				}else if(selectedGameObject.name=="GameAreas"){
					EditorUtility.DisplayPopupMenu (new Rect (mousePosition.x,mousePosition.y,0,0),"MapEditor/场景区域",null);
					Event.current.Use ();
				}else if(selectedGameObject.name=="GameEvents"){
					EditorUtility.DisplayPopupMenu (new Rect (mousePosition.x,mousePosition.y,0,0),"MapEditor/创建事件机制",null);
					Event.current.Use ();
				}else if(selectedGameObject.GetComponent<SceneTrigger> () != null){
					EditorUtility.DisplayPopupMenu (new Rect (mousePosition.x,mousePosition.y,0,0),"MapEditor/触发事件",null);
					Event.current.Use ();
				}else if(selectedGameObject.GetComponent<MapEvent> () != null ||selectedGameObject.GetComponent<MapEventTrigger> () != null){
					EditorUtility.DisplayPopupMenu (new Rect (mousePosition.x,mousePosition.y,0,0),"MapEditor/创建动作",null);
					Event.current.Use ();
				}else if(selectedGameObject.GetComponent<SceneItem> () != null){
					EditorUtility.DisplayPopupMenu (new Rect (mousePosition.x,mousePosition.y,0,0),"MapEditor/Hidden/Item",null);
					Event.current.Use ();
				}else if(selectedGameObject.GetComponent<MapAnimation> () != null){
					EditorUtility.DisplayPopupMenu (new Rect (mousePosition.x,mousePosition.y,0,0),"MapEditor/Hidden/Animation",null);
					Event.current.Use ();
				}else if(selectedGameObject.GetComponent<MapAnimationFrame> () != null){
					EditorUtility.DisplayPopupMenu (new Rect (mousePosition.x,mousePosition.y,0,0),"MapEditor/Hidden/AnimationFrame",null);
					Event.current.Use ();
				}
			}
		}
	} 

	static void OnSelectionChange(){
		if (Selection.activeGameObject == null)
			return;
		
		var wave = Selection.activeGameObject.GetComponent<SceneNPCWave>();
		var group = Selection.activeGameObject.GetComponent<SceneNPCGroup>();
		var npc = Selection.activeGameObject.GetComponent<SceneNPC>();

		if (group != null) {
			MapManager.selectionGroup = group;
		}
		if (wave != null) {
			MapManager.selectionWave = wave;
		}
		if (npc != null) {
			MapManager.selectionNPC = npc;
		}
	}


	 
}
 
