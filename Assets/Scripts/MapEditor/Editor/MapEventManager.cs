using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class MapEventManager  { 

	private static Collider CreateCollider(GameObject go,SceneTriggerShape shape,Vector3 size){
		Collider collider = null;
		switch (shape) {
		case SceneTriggerShape.Box:
			var boxCollider = go.AddComponent<BoxCollider> ();
			boxCollider.size = new Vector3 (size.x, size.y, size.z);
			collider = boxCollider;
			break;
		case SceneTriggerShape.Sphere:
			var sphereCollider = go.AddComponent<SphereCollider> ();
			sphereCollider.radius = size.x / 2;
			collider = sphereCollider;
			break; 
		}
		return collider;
	}

	private static Collider CreateSolidCollider(SceneTriggerShape shape,Vector3 size,out GameObject go){
		Collider collider = null;
		switch (shape) {
		case SceneTriggerShape.Box:
			go = GameObject.CreatePrimitive (PrimitiveType.Cube);
			var boxCollider = go.GetComponent<BoxCollider> (); 
			collider = boxCollider;
			break;
		case SceneTriggerShape.Sphere:
			go = GameObject.CreatePrimitive (PrimitiveType.Sphere);
			var sphereCollider = go.GetComponent<SphereCollider> (); 
			collider = sphereCollider;
			break; 
		default:
			go = null;
			break;
		}

		go.transform.localScale = size;
		return collider;
	}

	public static void CreateTrigger(GameObject target,SceneTriggerType tiggerType,string triggerData,SceneTriggerShape triggerShape,Vector3 nodeSize){
		GameObject root = MapEventHelper.FindTriggerRoot ();

		if (target == root) {
			target = null;
		}
		 
		var go = new GameObject ("触发器-"+triggerData); 
		var node = go.AddComponent<SceneTrigger> ();  
		node.triggerType = tiggerType;
		node.triggerData = triggerData; 
		node.shape = triggerShape;  

		go.transform.SetParent (root.transform);
		go.transform.position = target != null?target.transform.position:MapManager.selectionPosition;
		go.transform.localScale = Vector3.one;
		go.transform.rotation = Quaternion.identity; 

		var collider = CreateCollider(go,triggerShape,nodeSize);
		collider.isTrigger = true;

		//map.AddTrigger (node); 

		Selection.activeGameObject = go;
	}

	public static T CreateEvent<T> (MapEventType eventType ) where T : MapEvent{  
		GameObject root = MapEventHelper.FindEventRoot ();

		var go = new GameObject ("事件-"+eventType); 
		var node = go.AddComponent<T> ();  

		node.eventType = eventType;   

		go.transform.SetParent (root.transform);
		go.transform.localPosition = Vector3.zero;
		go.transform.localScale = Vector3.one;
		go.transform.rotation = Quaternion.identity;

		Selection.activeGameObject = go;

		return node;
	}

	public static T CreateEventTrigger<T>(GameObject target,MapEventTriggerType eventType,string targetId) where T:MapEventTrigger{ 
		
		var go = new GameObject ("事件-"+eventType+"-"+targetId); 
		var node = go.AddComponent<T> ();  

		node.eventType = eventType; 
		node.targetId = targetId;

		go.transform.SetParent (target.transform);
		go.transform.localPosition = Vector3.zero;
		go.transform.localScale = Vector3.one;
		go.transform.rotation = Quaternion.identity;

		Selection.activeGameObject = go;

		return node;
	}
	 


	public static MapWall CreateWall(string  wallName,int requiredKey){
		GameObject root = MapEventHelper.FindWallRoot ();
		var go = GameObject.CreatePrimitive (PrimitiveType.Cube);
		go.name = ("墙体-"+wallName); 
		go.transform.SetParent (root.transform);
		go.transform.position = MapManager.selectionPosition;

		var collider = go.GetComponent<BoxCollider> ();   
		collider.isTrigger = true;
		go.transform.localScale = new Vector3 (8, 2, 0.5f);

		var render = go.GetComponent<MeshRenderer> ();  
		render.enabled = false;

		var node = go.AddComponent<MapWall> ();  
		node.wallName = wallName;
		node.requiredKey = requiredKey;  

		return node;
	}


	public static SceneItem CreateItem(string id,string name,string icon){
		GameObject root = MapEventHelper.FindItemRoot ();
		var go = new GameObject (name); 
		go.transform.SetParent (root.transform);
		go.transform.position = MapManager.selectionPosition; 

		var node = go.AddComponent<SceneItem> ();
		node.itemName = name;
		node.itemIcon = icon;
		node.itemId = id;

		return node;
	}

	public static MapAudioSource CreateAudioSource(string  audioName){
		GameObject root = MapEventHelper.FindAudioRoot ();
		var go = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		go.name = ("音频源-"+audioName); 
		go.transform.SetParent (root.transform);
		go.transform.position = MapManager.selectionPosition;
		 

		var render = go.GetComponent<MeshRenderer> ();  
		render.enabled = false;

		var node = go.AddComponent<MapAudioSource> ();  
		node.audioPath = audioName; 

		return node;
	}

	public static MapArea CreateMapArea(SceneTriggerShape shape){
		GameObject root = MapEventHelper.FindAreaRoot ();

		GameObject go = null;
		var collider = CreateSolidCollider(shape,new Vector3(2,2,2),out go);
		collider.isTrigger = true; 

		var node = go.AddComponent<MapArea> ();  
		go.transform.SetParent (root.transform);
		go.transform.position = MapManager.selectionPosition;
		go.transform.localScale = Vector3.one;
		go.transform.rotation = Quaternion.identity;  

		var child = new GameObject ("text");
		child.transform.parent = go.transform;
		child.transform.localPosition = Vector3.zero;
		child.transform.localScale = Vector3.one;
		child.transform.rotation = Quaternion.identity;  

		/*var text = child.AddComponent<TextMesh> (); 
		text.anchor = TextAnchor.MiddleCenter;
		text.alignment = TextAlignment.Center;
		text.text = node.displayName;*/

		Selection.activeGameObject = go;

		return node;
	}

	 
}
