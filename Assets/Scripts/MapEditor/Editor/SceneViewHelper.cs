using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class SceneViewHelper  {

	private static GUIStyle labelStyle = new GUIStyle();  	  

	public static void ProcessEvent(string menuPath){

		if (MapManager.current == null) {
			return;
		}
        /*
        if (Event.current != null && Event.current.isKey)
        {
            GameObject play = GameObject.Find("EditorPlayer");
            if (play != null)
            {
                Transform player = play.transform;
                int ix = 0, iy = 0;
                if (Event.current.keyCode == KeyCode.W)
                    iy = -1;
                if (Event.current.keyCode == KeyCode.S)
                    iy = 1;
                if (Event.current.keyCode == KeyCode.A)
                    ix = -5;
                if (Event.current.keyCode == KeyCode.D)
                    ix = 5;
                player.rotation = Quaternion.Euler(player.rotation.eulerAngles + new Vector3(0, ix, 0));
                player.Translate(-0.1f * iy * Vector3.forward);
                MapManager.selectionPosition = player.position;
                if (Event.current.keyCode == KeyCode.Space)
                {
                    MapEditor.AddNode();
                }
            }
        }*/
        if (Event.current != null && !Event.current.alt && Event.current.type <= EventType.MouseUp) {  
			var mousePosition = Event.current.mousePosition;

			if (Event.current.button == 0 && Event.current.control) {
				var ray = HandleUtility.GUIPointToWorldRay (mousePosition);
				RaycastHit hitInfo;
				if (Physics.Raycast (ray, out hitInfo)) {
					MapManager.selectionPosition = hitInfo.point;
				} else {
					MapManager.selectionPosition = ray.origin;
				}  
			}
            else if (Event.current.shift && Event.current.type == EventType.MouseDown)
            {
                if(Event.current.button == 0)
                {
                  
                }
                if (Event.current.button == 1)
                {
                    EditorUtility.DisplayPopupMenu(new Rect(mousePosition.x, mousePosition.y, 0, 0), "MapEditor/Editor", null);
                    Event.current.Use();
                }
                    
            }
            if (Event.current.button == 1 && !Event.current.control && !Event.current.shift)
            {
                EditorUtility.DisplayPopupMenu(new Rect(mousePosition.x, mousePosition.y, 0, 0), menuPath, null);
                Event.current.Use();
            }

        }
      

        if (MapManager.current != null) {
			/*float arrowSize = 5;

			Handles.color = Handles.xAxisColor;
			Handles.ArrowCap( 0, MapManager.selectionPosition,   Quaternion.Euler( 0, 90, 0 ), arrowSize );

			Handles.color = Handles.yAxisColor;
			Handles.ArrowCap( 0, MapManager.selectionPosition,   Quaternion.Euler( -90, 0, 0 ), arrowSize );

			Handles.color = Handles.zAxisColor;
			Handles.ArrowCap( 0, MapManager.selectionPosition, Quaternion.Euler( 0, 0, 90 ), arrowSize ); */

			Handles.ConeCap (0, MapManager.selectionPosition, Quaternion.Euler( -90, 0, 0 ), 0.5f);
			Handles.Label (MapManager.selectionPosition, "当前选中位置");
		}


	}


	public static void DrawSceneMap(SceneMap map){ 
		foreach (var npc in map.npcList) {
			if (npc == null)
				continue;
			DrawSceneObject (npc,Color.green);
		}

		foreach (var npc in map.transmitList) {
			if (npc == null)
				continue;
			DrawSceneObject (npc,Color.blue);
		}
		 

		var areaRoot = MapEventHelper.FindAreaRoot ();
		if (areaRoot != null) {
			foreach (var area in areaRoot.GetComponentsInChildren<MapArea>()) {
				DrawArea (area);
			}
		}


		Handles.BeginGUI ();
		GUI.contentColor = Color.green;
		GUI.Label (new Rect (10, 10, 1000, 20), "选择位置:按下Control键的同时点击鼠标左键");
		GUI.Label (new Rect (10, 30, 1000, 20), "创建:点击鼠标右键");
        GUI.Label(new Rect(10, 50, 1000, 20), "路点编辑菜单:按下Shift键的同时点击鼠标右键");
        Handles.EndGUI ();
	}

	static void DrawSceneObject(SceneObject npc,Color shapeColor){
		var oldColor = Handles.color;
		Handles.color = shapeColor;
		switch (npc.objectShape) {
		case SceneObjectShape.Box:
			//Handles.CubeCap (0, item.transform.position, Quaternion.identity, item.x);
			Handles.DrawWireCube (npc.transform.position, new Vector3 (npc.objectSize.x,npc.objectSize.y,npc.objectSize.z));
			break;
		case SceneObjectShape.Sphere:
			//Handles.SphereCap (0, item.transform.position, Quaternion.identity, item.x);
			DrawWireSphere (npc.transform.position,npc.objectSize.x/2);
			break;
		case SceneObjectShape.Capsule:  
			//Handles.CylinderCap (0,npc.transform.position,Quaternion.Euler(90,0,0),npc.objectSize.x); 
			//DrawWireCylinder (npc.transform.position,npc.objectSize.x/2,npc.objectSize.y);
			if (npc.GetComponent<MeshFilter> () == null) {
				//var render = npc.gameObject.AddComponent<MeshFilter> ();
				//var source = MapManager.cylinder.GetComponent<MeshFilter> ();
			}
			break;
		case SceneObjectShape.Circle:
			Handles.DrawWireDisc (npc.transform.position, Vector3.up, npc.objectSize.x/2); 
			break;
		}

		Handles.color = Color.white;
		labelStyle.normal.textColor = Color.red;
		Handles.Label (npc.transform.position, npc.displayName,labelStyle); 
		Handles.color = oldColor;
	}

	static void DrawArea(MapArea area){
		var oldColor = Handles.color;
		Handles.color = Color.white;
		labelStyle.normal.textColor = area.displayColor;
		Handles.Label (area.transform.position, area.displayName,labelStyle); 
		Handles.color = oldColor;
	}

	static void DrawWireSphere(Vector3 position,float radius){
		Handles.DrawWireDisc (position, Vector3.up, radius);
		Handles.DrawWireDisc (position, Vector3.left, radius);
		Handles.DrawWireDisc (position, Vector3.forward, radius);
	}

	static void DrawWireCylinder(Vector3 position,float radius,float height){
		Handles.DrawWireDisc (position + new Vector3 (0, height / 2, 0), Vector3.up, radius);
		Handles.DrawWireDisc (position + new Vector3 (0, -height / 2, 0), Vector3.up, radius);  

		Handles.DrawLine (position + new Vector3 (-radius, height / 2, 0),position + new Vector3 (-radius, -height / 2, 0));
		Handles.DrawLine (position + new Vector3 (radius, height / 2, 0),position + new Vector3 (radius, -height / 2, 0));

		Handles.DrawLine (position + new Vector3 (0, height / 2, radius),position + new Vector3 (0, -height / 2, radius));
		Handles.DrawLine (position + new Vector3 (0, height / 2, -radius),position + new Vector3 (0, -height / 2, -radius));
	}	 

	[DrawGizmo(GizmoType.InSelectionHierarchy|GizmoType.NonSelected)]
	static void DrawGameObjectName(Transform transform,GizmoType gizmoType){ 
		var target = transform.GetComponent<SceneMap> ();
		if (target) { 
			DrawSceneMap (target);
		}
	}
}
