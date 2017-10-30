using UnityEngine;  
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class MapEventHelper  {  

	public static GameObject GetOrCreateGameObject(string name){
		var go = GameObject.Find (name);
		if (go == null) {
			go = new GameObject (name);  
		}
		return go;
	}

	public static GameObject FindRootGameObject(string name){
		GameObject go = GameObject.Find (name);
		if (go == null) {
			//var prefab = AssetDatabase.LoadAssetAtPath<GameObject> ("Assets/Res/Scene/"+name+".prefab");
			//go = PrefabUtility.InstantiatePrefab (prefab) as GameObject; 
		}
		return go;
	}

	public static GameObject AddChildGameObject(GameObject parent,string name){
		var go = new GameObject (name);
		go.transform.parent = parent.transform;
		go.transform.localScale = Vector3.one;
		go.transform.localRotation = Quaternion.identity;
		go.transform.localPosition = Vector3.zero;
		return go;
	}

	public static GameObject AddChildGameObject(PrimitiveType type, GameObject parent,string name){
		var go = GameObject.CreatePrimitive (type);
		go.name = name;
		go.transform.parent = parent.transform;
		go.transform.localScale = Vector3.one;
		go.transform.localRotation = Quaternion.identity;
		go.transform.localPosition = Vector3.zero;
		return go;
	}

	public static GameObject FindWallRoot(){
		return  FindRootGameObject ("GameWalls");
	}

	public static GameObject FindEventRoot(){
		return  FindRootGameObject ("GameEvents");
	}

	public static GameObject FindTriggerRoot(){
		return  FindRootGameObject ("GameTriggers");
	}

	public static SceneMap FindSceneRoot(){
		return GameObject.FindObjectOfType<SceneMap> ();
	}

	public static GameObject FindItemRoot(){
		//var obj = GameObject.FindObjectOfType<SceneItemManager> ();
		//return  obj!=null?obj.gameObject:null;
		return  FindRootGameObject ("GameItems");
	}

	public static GameObject FindAudioRoot(){
		//var obj = GameObject.FindObjectOfType<SceneItemManager> ();
		//return  obj!=null?obj.gameObject:null;
		return  FindRootGameObject ("GameAudios");
	}

	public static GameObject FindAreaRoot(){
		//var obj = GameObject.FindObjectOfType<SceneItemManager> ();
		//return  obj!=null?obj.gameObject:null;
		return  FindRootGameObject ("GameAreas");
	}

	public static string GetGameObjectPath(Transform go){
		string path = go.gameObject.name;
		while (go.parent != null) {
			go = go.parent;
			path = go.name +"/"+path;
		}
		return path;
	}

	public static void ExportEvents(SceneMap map,string path = null){
		int mapId = 10;
		if (map != null) {
			mapId = map.sceneId;
		}

		string exportDir = MapUtil.DataPath();
		exportDir += "MapData/";
		if (!Directory.Exists (exportDir)) {
			Directory.CreateDirectory (exportDir);
		} 

		if (path == null) {			
			path = exportDir +"/MapEvent_" + mapId + ".bytes";
		} 

		SceneMap mapRoot = FindSceneRoot ();
		GameObject triggerRoot = FindTriggerRoot();
		GameObject wallRoot = FindWallRoot ();
		GameObject eventRoot = FindEventRoot (); 
		GameObject itemRoot = FindItemRoot ();
		GameObject audioRoot = FindAudioRoot ();
		GameObject areaRoot = FindAreaRoot ();

		List<Example.MapWall> walls = new List<Example.MapWall> ();
		List<Example.MapEvent> events = new List<Example.MapEvent> ();
		List<Example.MapTrigger> triggers = new List<Example.MapTrigger> (); 
		List<Example.MapEntityEvent> entityEvents = new List<Example.MapEntityEvent> ();
		List<Example.MapItem> items = new List<Example.MapItem> ();
		List<Example.MapAudioSource> audios = new List<Example.MapAudioSource> ();
		List<Example.MapArea> areas = new List<Example.MapArea> ();

		if (mapRoot == null) {
			Debug.LogError ("没有场景配置，将场景预制拖到编辑器中，或者创建一个新的场景");
			return;
		}

		if (eventRoot != null) {
			var sceneEvents = eventRoot.GetComponentsInChildren<MapEvent> ();
			foreach (var sceneEvent in sceneEvents) { 
				var mapEvent = ExportMapEvent (events,sceneEvent);  
			}
		}

		if (wallRoot != null) {
			var sceneWalls = wallRoot.GetComponentsInChildren<MapWall> ();
			foreach (var sceneWall in sceneWalls) { 
				var wall = ExportMapWall (walls,sceneWall); 
				var eventTriggers = sceneWall.GetComponentsInChildren <MapEventTrigger>(); 
				var entityEvent = ExportMapEntityEvent (eventTriggers,wall.Id,Example.MapEntityEvent.Type.WALL);
				if (eventTriggers.Length > 0) {
					entityEvents.Add (entityEvent);
				}
			}
		}

		var sceneTriggers = triggerRoot.GetComponentsInChildren<SceneTrigger> ();
		foreach (var sceneTrigger in sceneTriggers) { 		
			var trigger = ExportMapTrigger (triggers,sceneTrigger);
			var eventTriggers = sceneTrigger.GetComponentsInChildren <MapEventTrigger>();
			foreach (var eventTrigger in eventTriggers) { 	 
				if (eventTrigger.eventType == MapEventTriggerType.TriggerIn) {
					trigger.InActions = ExportMapEventActions (eventTrigger.GetComponentsInChildren<MapEventAction>(),trigger.Id.ToString());
				}else if (eventTrigger.eventType == MapEventTriggerType.TriggerOut) {
					trigger.OutActions = ExportMapEventActions (eventTrigger.GetComponentsInChildren<MapEventAction>(),trigger.Id.ToString());
				}
			}
			 
		}

		foreach (var npc in mapRoot.npcList) {
			var eventTriggers = npc.GetComponentsInChildren <MapEventTrigger>(); 
			var entityEvent = ExportMapEntityEvent (eventTriggers,npc.objectId,Example.MapEntityEvent.Type.NPC);
			if (eventTriggers.Length > 0) {
				entityEvents.Add (entityEvent);
			}
		}

		if (itemRoot != null) { 
			var sceneItems = itemRoot.GetComponentsInChildren<SceneItem> ();
			foreach (var sceneItem in sceneItems) { 
				if (string.IsNullOrEmpty (sceneItem.itemId)) {
					//GameObject.DestroyImmediate (sceneItem.gameObject);
					Debug.LogErrorFormat("{0} not valid item object",MapEventHelper.GetGameObjectPath(sceneItem.transform));
					continue;
				}
				var item = ExportMapItem (items,sceneItem); 
				var eventTriggers = sceneItem.GetComponentsInChildren <MapEventTrigger>(); 
				var entityEvent = ExportMapEntityEvent (eventTriggers,item.Id,Example.MapEntityEvent.Type.ITEM);
				if (eventTriggers.Length > 0) {
					entityEvents.Add (entityEvent);
				}
			}
		}

		if (audioRoot != null) { 
			var audioSources = audioRoot.GetComponentsInChildren<MapAudioSource> ();
			foreach (var audioSource in audioSources) {  
				var audio = ExportMapAudioSource (audios,audioSource); 
			}
		}

		if (areaRoot != null) { 
			var mapAreas = areaRoot.GetComponentsInChildren<MapArea> ();
			foreach (var mapArea in mapAreas) {  
				var area = ExportMapArea (areas,mapArea); 
			}
		}
		 


		Example.MapEventData mapEventData = new Example.MapEventData ();
		mapEventData.Walls = walls;
		mapEventData.Events = events;
		mapEventData.Triggers = triggers;
		mapEventData.EntityEvents = entityEvents;
		mapEventData.Items = items;
		mapEventData.AudioSources = audios;
		mapEventData.Areas = areas;

		var bytes = Example.MapEventData.SerializeToBytes (mapEventData);
		FileStream fs = new FileStream (path, FileMode.Create);
		fs.Write (bytes,0,bytes.Length);
		fs.Close ();

		/*var prefab = PrefabUtility.GetPrefabParent (wallRoot); 
		PrefabUtility.ReplacePrefab (wallRoot,prefab);
		//prefab = PrefabUtility.GetPrefabParent (itemRoot); 
		//PrefabUtility.ReplacePrefab (itemRoot,prefab);
		prefab = PrefabUtility.GetPrefabParent (eventRoot); 
		PrefabUtility.ReplacePrefab (eventRoot,prefab);
		prefab = PrefabUtility.GetPrefabParent (triggerRoot); 
		PrefabUtility.ReplacePrefab (triggerRoot,prefab);*/

		Debug.LogFormat ("导出场景事件{0}数据到{1}",mapId,path);
	}

	static Example.MapTrigger ExportMapTrigger(List<Example.MapTrigger> triggers,SceneTrigger sceneTrigger){
		Example.MapTrigger trigger = new Example.MapTrigger ();
		trigger.type = (Example.MapTrigger.Type)(sceneTrigger.triggerType);
		trigger.shape = (Example.MapTrigger.Shape)(sceneTrigger.shape);
		trigger.Data = sceneTrigger.triggerData;
		trigger.Center = MapUtil.ToVector3f(sceneTrigger.transform.position);
		trigger.Rotation =  MapUtil.ToVector3f(sceneTrigger.transform.rotation.eulerAngles);
		if (sceneTrigger.GetComponent<SphereCollider> () != null) {
			trigger.Radius = sceneTrigger.GetComponent<SphereCollider> ().radius; 
		}
		if (sceneTrigger.GetComponent<BoxCollider> () != null) { 
			var size = sceneTrigger.GetComponent<BoxCollider> ().size;
			var scale = sceneTrigger.transform.localScale;
			trigger.Size = MapUtil.ToVector3f( new Vector3(size.x * scale.x,size.y*scale.y,size.z*scale.z)); 
		} 

		trigger.ObjPath = sceneTrigger.effectName; 
		trigger.AutoShow = sceneTrigger.autoShow;

		triggers.Add (trigger); 
		trigger.Id = triggers.Count;
		sceneTrigger.objectId = trigger.Id;

		return trigger;
	}

	static Example.MapEvent ExportMapEvent(List<Example.MapEvent> events,MapEvent sceneEvent){

		Example.MapEvent mapEvent = new Example.MapEvent (); 
		mapEvent.EventType = (int)sceneEvent.eventType;  

		List<Example.MapEventCondition> conditions = new List<Example.MapEventCondition> (); 
		foreach (var condition in sceneEvent.Conditions) {
			Example.MapEventCondition cond = new Example.MapEventCondition ();

			var value =  new Example.ContentValue ();
			value.IntValue = condition.intValue;
			value.StrValue = condition.strValue;
			value.FloatValue = condition.floatValue;
			value.Vector3Value = MapUtil.ToVector3f(condition.posValue); 

			cond.Arg2 = value;
			conditions.Add (cond);
		}

		List<Example.MapEventAction> actions = new List<Example.MapEventAction> ();

		var mapActions = sceneEvent.GetComponentsInChildren<MapEventAction> ();
		foreach (var mapAction in mapActions) {
			var action = ExportMapEventAction (mapAction);
			actions.Add (action);
		}
		mapEvent.ExecuteCount = sceneEvent.executeCount;
		mapEvent.Interception = sceneEvent.interception;
		mapEvent.Conditions = conditions;
		mapEvent.Actions = actions;
		events.Add (mapEvent);
		mapEvent.Id = events.Count;
		sceneEvent.eventId = mapEvent.Id;

		return mapEvent;
	}

	static Example.MapEntityEvent ExportMapEntityEvent(MapEventTrigger[] eventTriggers,int targetId,Example.MapEntityEvent.Type targetType){ 
		var entityEvent = new Example.MapEntityEvent ();

		var actionTriggers = new List<Example.MapActionTrigger>();
		foreach (var eventTrigger in eventTriggers) { 	
			var trigger = new Example.MapActionTrigger ();  
			trigger.EventType = (Example.MapActionTrigger.Type)eventTrigger.eventType;

			var condition = eventTrigger.Condition;
			var value =  new Example.ContentValue ();
			value.IntValue = condition.intValue;
			value.StrValue = condition.strValue;
			value.FloatValue = condition.floatValue;
			value.Vector3Value = MapUtil.ToVector3f(condition.posValue);  
			trigger.Value = value;

			trigger.Actions = ExportMapEventActions (eventTrigger.GetComponentsInChildren<MapEventAction> (), trigger.Id.ToString ());
			if (eventTrigger.triggerEvent != null) {
				trigger.TriggerEventId = eventTrigger.triggerEvent.eventId;
			}
			actionTriggers.Add (trigger);
		}
		entityEvent.Triggers = actionTriggers;
		entityEvent.TargetId = targetId;
		entityEvent.TargetType = targetType; 

		return entityEvent;
	}

	static List<Example.MapEventAction> ExportMapEventActions(MapEventAction[] sceneActions,string targetId){ 
		List<Example.MapEventAction> actions = new List<Example.MapEventAction> ();
		foreach (var sceneAction in sceneActions) {	
			var action = ExportMapEventAction (sceneAction); 
			action.Target = targetId;
			actions.Add (action);
		}
		return actions;
	}

	static Example.MapEventAction ExportMapEventAction(MapEventAction mapAction){

		var args = new List<Example.ContentValue> ();
		foreach (var mapActionArg in mapAction.Argments) { 
			var value =  new Example.ContentValue ();
			value.IntValue = mapActionArg.intValue;
			value.StrValue = mapActionArg.strValue;
			value.FloatValue = mapActionArg.floatValue;
			value.Vector3Value = MapUtil.ToVector3f(mapActionArg.posValue); 
			args.Add (value);
		}

		Example.MapEventAction action = new Example.MapEventAction ();  
		action.ActionType = (Example.MapEventAction.Type)mapAction.actionType;
		action.Target = mapAction.actionTarget;
		action.Args = args;
		action.DelayTime = mapAction.delayTime;

		return action;
	}

	static Example.MapWall ExportMapWall(List<Example.MapWall> walls,MapWall sceneWall){

		var size = MapUtil.ToVector3f(sceneWall.GetComponent<BoxCollider> ().size); 
		size.X *= sceneWall.transform.localScale.x;
		size.Y *= sceneWall.transform.localScale.y;
		size.Z *= sceneWall.transform.localScale.z;

		Example.MapWall mapWall = new Example.MapWall ();
		mapWall.Center = MapUtil.ToVector3f(sceneWall.transform.position);
		mapWall.Size = size;
		mapWall.Rotation =  MapUtil.ToVector3f(sceneWall.transform.rotation.eulerAngles);
		mapWall.RequiredKey = sceneWall.requiredKey; 
		mapWall.Autoshow = sceneWall.autoshow;
		mapWall.EffectPath = sceneWall.effectPath;

		walls.Add (mapWall);
		mapWall.Id = walls.Count; 
		sceneWall.wallId = mapWall.Id;
		return mapWall;
	}

	static Example.MapAudioSource ExportMapAudioSource(List<Example.MapAudioSource> audios,MapAudioSource audioSource){
		 
		Example.MapAudioSource audio = new Example.MapAudioSource (); 
		audio.Pos = MapUtil.ToVector3f(audioSource.transform.position);
		audio.Path = audioSource.audioPath;
		audio.Autoshow = audioSource.autoplay;  
		audio.Loop = audioSource.loop;
		audio.MinDistance = audioSource.minDistance;
		audio.MaxDistance = audioSource.maxDistance;
		audio.Volume = audioSource.volume;

		audios.Add (audio);
		audio.Id = audios.Count; 
		audioSource.audioID = audio.Id;
		return audio;
	}

	static Example.MapItem ExportMapItem(List<Example.MapItem> items,SceneItem sceneItem){
		 
		Example.MapItem item = new Example.MapItem (); 
		item.Id = int.Parse(sceneItem.itemId);
		item.RequiredKey = sceneItem.requiredKey; 
		item.Path = MapEventHelper.GetGameObjectPath (sceneItem.transform);

		items.Add (item); 
		return item;
	} 


	static Example.MapArea ExportMapArea(List<Example.MapArea> areas,MapArea mapArea){
		Example.MapArea area = new Example.MapArea ();
		area.type = (Example.MapArea.Type)(mapArea.areaType); 
		area.Center = MapUtil.ToVector3f(mapArea.transform.position);
		area.Rotation =  MapUtil.ToVector3f(mapArea.transform.rotation.eulerAngles);
		area.Audio = mapArea.audioPath;

		var scale = mapArea.transform.localScale;

		if (mapArea.GetComponent<SphereCollider> () != null) {
			area.shape = Example.MapArea.Shape.CIRCLE;
			area.Radius = mapArea.GetComponent<SphereCollider> ().radius * Mathf.Max(scale.x,Mathf.Max(scale.y,scale.z)); 
		}
		if (mapArea.GetComponent<BoxCollider> () != null) { 
			var size = mapArea.GetComponent<BoxCollider> ().size;
			area.shape = Example.MapArea.Shape.BOX;
			area.Size = MapUtil.ToVector3f( new Vector3(size.x * scale.x,size.y*scale.y,size.z*scale.z)); 
		} 
		 
		areas.Add (area); 
		area.Id = areas.Count;
		mapArea.areaId = area.Id;

		return area;
	}


}
