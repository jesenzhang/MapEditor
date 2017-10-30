using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class MapDebugHelper {

	public static void PrintMapUnit(string path){
		var mapName = System.IO.Path.GetFileName (path);
		var root = MapEventHelper.GetOrCreateGameObject (mapName);

		var data = File.ReadAllBytes (path);
		Example.MapUnit mapUnit = Example.MapUnit.Deserialize (data);

		int groupID = 0;
		foreach (var group in mapUnit.Groups) {
			if (groupID > 0) {
				var groupGo = MapEventHelper.AddChildGameObject (root, "group-" + (groupID));  
				int waveID = 0;
				foreach (var wave in group.Passes) {
					var waveGO = MapEventHelper.AddChildGameObject (groupGo, "wave-" + (++waveID));  
					foreach (var npcID in wave.Npcs) {
						var npc = mapUnit.Npcs[npcID];
						var npcGO = MapEventHelper.AddChildGameObject (PrimitiveType.Capsule,waveGO, "npc-" + npcID);  
						var npcObj = npcGO.AddComponent<SceneNPC> ();
						npcObj.objectId = npcID;
						npcObj.resId = MapManager.FindNPCResId (npcID);
						npcObj.transform.position = MapUtil.Vector3fToVector3(npc.Position) + Vector3.up;
						npcObj.transform.rotation = Quaternion.Euler(MapUtil.Vector3fToVector3(npc.Rotation));
					}
				}
			}
			++groupID;
		}

		foreach (var transmitNode in mapUnit.TransmitNodes) {
			var transimitGo = MapEventHelper.AddChildGameObject (root, "transimit-" + (transmitNode.Id));   
			transimitGo.transform.position = MapUtil.Vector3fToVector3(transmitNode.Position);
			transimitGo.transform.rotation = Quaternion.Euler(MapUtil.Vector3fToVector3(transmitNode.Rotation));
		}
	}

	public static void PrintMapEvent(string path){
		var mapName = System.IO.Path.GetFileName (path);
		var root = MapEventHelper.GetOrCreateGameObject (mapName);

		var wallRoot = MapEventHelper.AddChildGameObject (root, "GameWalls");   
		var triggerRoot = MapEventHelper.AddChildGameObject (root, "GameTriggers");   
		var eventRoot = MapEventHelper.AddChildGameObject (root, "GameEvents");   

		var data = File.ReadAllBytes (path);
		Example.MapEventData mapEventData = Example.MapEventData.Deserialize (data);
		 

		foreach (var mapWall in mapEventData.Walls) {
			var go = MapEventHelper.AddChildGameObject (wallRoot, "wall-" + (mapWall.Id));   
			go.transform.position = MapUtil.Vector3fToVector3(mapWall.Center);
			go.transform.rotation = Quaternion.Euler(MapUtil.Vector3fToVector3(mapWall.Rotation));
			var box = go.AddComponent<BoxCollider> ();
			box.size = MapUtil.Vector3fToVector3(mapWall.Size);
		}

		foreach (var mapTrigger in mapEventData.Triggers) {
			var go = MapEventHelper.AddChildGameObject (triggerRoot, "trigger-" + (mapTrigger.Id));   
			go.transform.position = MapUtil.Vector3fToVector3(mapTrigger.Center);
			go.transform.rotation = Quaternion.Euler(MapUtil.Vector3fToVector3(mapTrigger.Rotation));

			if (mapTrigger.shape == Example.MapTrigger.Shape.BOX) {
				var box = go.AddComponent<BoxCollider> ();
				box.size = MapUtil.Vector3fToVector3(mapTrigger.Size);
			}else if (mapTrigger.shape == Example.MapTrigger.Shape.CIRCLE) {
				var sphere = go.AddComponent<SphereCollider> ();
				sphere.radius = mapTrigger.Radius;
			}
		}

		foreach (var mapEvent in mapEventData.Events) {
			var go = MapEventHelper.AddChildGameObject (eventRoot, "event-" + (mapEvent.Id));    
			var eventObj = go.AddComponent<MapEvent> ();
			eventObj.eventType = (MapEventType)mapEvent.EventType;
			eventObj.eventId = mapEvent.Id;

			foreach (var condition in mapEvent.Conditions) {
				var conditionGO = MapEventHelper.AddChildGameObject (go, "condition"); 
				PrintArgment (conditionGO,condition.Arg2); 
			}

			foreach (var action in mapEvent.Actions) {
				var actionGO = MapEventHelper.AddChildGameObject (go, "action-"+action.ActionType); 
				foreach (var arg in action.Args) {
					var argGO = MapEventHelper.AddChildGameObject (actionGO, "arg"); 
					PrintArgment (argGO,arg);
					if (argGO.transform.childCount == 0) {
						GameObject.DestroyImmediate (argGO);
					}
				}
			}
		}
	}

	private static void PrintArgment(GameObject go,Example.ContentValue arg){
		if (arg.IntValue != 0) {
			MapEventHelper.AddChildGameObject (go, "intValue-"+arg.IntValue); 
		}

		if (arg.FloatValue != 0) {
			MapEventHelper.AddChildGameObject (go, "floatValue-"+arg.FloatValue); 
		}

		if (!string.IsNullOrEmpty(arg.StrValue)) {
			MapEventHelper.AddChildGameObject (go, "strValue-"+arg.StrValue); 
		} 
		var pos = MapUtil.Vector3fToVector3 (arg.Vector3Value);
		if (pos.magnitude > 0) {
			MapEventHelper.AddChildGameObject (go, "vecValue-"+pos); 
		}

	}
}
