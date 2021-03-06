using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Reflection;

public class MapAnimationEditor {
	[MenuItem("MapEditor/Hidden/NPC/添加出生动画",false,5)]  
	static void CreateNPCBornAnimation(){ 
		var npc = Selection.activeGameObject.GetComponent<SceneNPC> ();
		if (npc == null) {
			Debug.LogError ("NPC is null");
			return;
		}

		var anim = npc.GetComponentInChildren<BornAnimation> ();
		if (anim == null) {
			var go = new GameObject ("出生动画");
			anim = go.AddComponent<BornAnimation> ();
			go.transform.SetParent (npc.transform);
			go.transform.localPosition = Vector3.zero;
			go.transform.localRotation = Quaternion.identity;
			go.transform.localScale = Vector3.one;
		}
		Selection.activeGameObject = anim.gameObject;
	} 

	[MenuItem("MapEditor/Hidden/NPC/添加死亡动画",false,6)]  
	static void CreateNPCDieAnimation(){ 
		var npc = Selection.activeGameObject.GetComponent<SceneNPC> ();
		if (npc == null) {
			Debug.LogError ("NPC is null");
			return;
		}

		var anim = npc.GetComponentInChildren<DieAnimation> ();
		if (anim == null) {
			var go = new GameObject ("死亡动画");
			anim = go.AddComponent<DieAnimation> ();
			go.transform.SetParent (npc.transform);
			go.transform.localPosition = Vector3.zero;
			go.transform.localRotation = Quaternion.identity;
			go.transform.localScale = Vector3.one;
		}
		Selection.activeGameObject = anim.gameObject;
	} 

	[MenuItem("MapEditor/Hidden/Animation/添加关键帧",false,6)]  
	static void CreateAnimationFrame(){ 
		var anim = Selection.activeGameObject.GetComponent<MapAnimation> ();
		if (anim == null) {
			Debug.LogError ("Animation is null");
			return;
		}

		var frames = anim.GetComponentsInChildren<MapAnimationFrame> (); 

		var go = GameObject.CreatePrimitive (PrimitiveType.Cylinder);
		go.name = ("Frame "+(frames.Length+1));
		var frame = go.AddComponent<MapAnimationFrame> ();
		go.transform.SetParent (anim.transform);
		go.transform.localPosition = Vector3.zero;
		go.transform.localRotation = Quaternion.identity;
		go.transform.localScale = Vector3.one; 

		Selection.activeGameObject = go;
	} 

	[MenuItem("MapEditor/Hidden/AnimationFrame/播放动作",false,7)]  
	static void CreatePlayAction(){ 
		var frame = Selection.activeGameObject.GetComponent<MapAnimationFrame> ();
		if (frame == null) {
			Debug.LogError ("Frame is null");
			return;
		}
		Selection.activeGameObject = CreateAnimationAction<PlayAction>(frame,AnimationActionType.PLAY_ACTION).gameObject;
	} 

	[MenuItem("MapEditor/Hidden/AnimationFrame/显示特效",false,8)]  
	static void CreateShowEffect(){ 
		var frame = Selection.activeGameObject.GetComponent<MapAnimationFrame> ();
		if (frame == null) {
			Debug.LogError ("Frame is null");
			return;
		}
		Selection.activeGameObject = CreateAnimationAction<PlayEffect>(frame,AnimationActionType.SHOW_EFFECT).gameObject;
	} 

	[MenuItem("MapEditor/Hidden/AnimationFrame/破碎",false,9)]  
	static void CreateBrakeEffect(){ 
		var frame = Selection.activeGameObject.GetComponent<MapAnimationFrame> ();
		if (frame == null) {
			Debug.LogError ("Frame is null");
			return;
		}
		Selection.activeGameObject = CreateAnimationAction<BrakeAction>(frame,AnimationActionType.BRAKE).gameObject;
	} 

	[MenuItem("MapEditor/Hidden/Test/GetType",false,9)]  
	static void GetAllEventActions(){
		Assembly assm = Assembly.GetAssembly (typeof(SceneItem));
		var mapEventType = assm.GetType ("MapEvent");
		Debug.LogFormat ("mapEventType={0}", mapEventType);
	}

	static MapAnimationAction CreateAnimationAction<T>(MapAnimationFrame frame,AnimationActionType actionType) where T:MapAnimationAction{
		var go = new GameObject ("Action-"+actionType);
		var action = go.AddComponent<T> ();
		go.transform.SetParent (frame.transform);
		go.transform.localPosition = Vector3.zero;
		go.transform.localRotation = Quaternion.identity;
		go.transform.localScale = Vector3.one;
		action.actionType = actionType;
		return action;
	}

}
