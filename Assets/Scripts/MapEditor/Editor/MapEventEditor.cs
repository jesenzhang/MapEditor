using UnityEngine;
using UnityEditor;
using System.Collections;

public class MapEventEditor  {
	[MenuItem("MapEditor/创建触发器/球形触发器",false,101)] 
	static void CreateSphereTrigger(){ 
		ScriptableWizard.DisplayWizard<SphereSceneTriggerCreateWizard>("创建触发器","创建"); 
	}

	[MenuItem("MapEditor/创建触发器/长方体触发器",false,102)] 
	static void CreateBoxTrigger(){ 
		ScriptableWizard.DisplayWizard<BoxSceneTriggerCreateWizard>("创建触发器","创建"); 
	}
	 

	[MenuItem("MapEditor/创建事件机制/NPC组死亡数量达到",false,201)] 
	static void CreateNPCDieMapEvent(){ 
		var groupId = 0;
		var group = Selection.activeGameObject.GetComponent<SceneNPCGroup> ();
		if (group != null) {
			groupId = group.groupId;
		}
		var mapEvent = MapEventManager.CreateEvent<NPCGroupDieEvent> (MapEventType.NPCGroupDieCount);
		mapEvent.groupid = groupId;
	}

	[MenuItem("MapEditor/创建事件机制/拾取了道具",false,202)] 
	static void CreateItemPickMapEvent(){  
		MapEventManager.CreateEvent<ItemPickEvent> (MapEventType.ItemPick);
	}

	[MenuItem("MapEditor/创建事件机制/使用了道具",false,203)] 
	static void CreateItemUseMapEvent(){  
		MapEventManager.CreateEvent<ItemUsedEvent> (MapEventType.ItemUsed);
	}


	[MenuItem("MapEditor/创建事件机制/进入副本",false,210)] 
	static void CreateEnterMapEvent(){  
		var mapEvent = MapEventManager.CreateEvent<MapEvent> (MapEventType.SceneIn);
		Selection.activeGameObject = mapEvent.gameObject;
	}

	[MenuItem("MapEditor/创建事件机制/离开副本",false,211)] 
	static void CreateLeaveMapEvent(){  
		var mapEvent = MapEventManager.CreateEvent<MapEvent> (MapEventType.SceneOut);
		Selection.activeGameObject = mapEvent.gameObject;
	}

	[MenuItem("MapEditor/创建事件机制/初始化副本",false,211)] 
	static void CreateInitMapEvent(){  
		var mapEvent = MapEventManager.CreateEvent<MapEvent> (MapEventType.SceneInit);
		Selection.activeGameObject = mapEvent.gameObject;
	}

	[MenuItem("MapEditor/创建事件机制/副本销毁",false,211)] 
	static void CreateDestroyMapEvent(){  
		var mapEvent = MapEventManager.CreateEvent<MapEvent> (MapEventType.SceneDestroy);
		Selection.activeGameObject = mapEvent.gameObject;
	}

	[MenuItem("MapEditor/创建事件机制/剧情播放完成事件",false,214)] 
	static void CreateStoryOverMapEvent(){  
		var mapEvent = MapEventManager.CreateEvent<StoryOverEvent> (MapEventType.StoryOver);
		Selection.activeGameObject = mapEvent.gameObject;
	}

	[MenuItem("MapEditor/创建事件机制/掉血达到一定百分比事件",false,215)] 
	static void CreateNPCHPDropEvent(){  
		var mapEvent = MapEventManager.CreateEvent<NPCHPDropEvent> (MapEventType.NPCHPDropTo);
		Selection.activeGameObject = mapEvent.gameObject;
	}

	[MenuItem("MapEditor/创建事件机制/任务接取事件",false,216)] 
	static void CreateTaskAcceptEvent(){  
		var mapEvent = MapEventManager.CreateEvent<TaskMapEvent> (MapEventType.TaskEvent);
		mapEvent.taskStatus = TaskEventStatus.ACCEPTED;
		Selection.activeGameObject = mapEvent.gameObject;
	}

	[MenuItem("MapEditor/创建事件机制/任务完成事件",false,217)] 
	static void CreateTaskFinisedEvent(){  
		var mapEvent = MapEventManager.CreateEvent<TaskMapEvent> (MapEventType.TaskEvent);
		mapEvent.taskStatus = TaskEventStatus.FINISED;
		Selection.activeGameObject = mapEvent.gameObject;
	}

	[MenuItem("MapEditor/创建事件机制/回调事件",false,218)] 
	static void CreateCallbackEvent(){  
		var mapEvent = MapEventManager.CreateEvent<CallbackEvent> (MapEventType.CallbackEvent); 
		Selection.activeGameObject = mapEvent.gameObject;
	}

	[MenuItem("MapEditor/创建事件机制/其他机制",false,299)] 
	static void CreateMapEvent(){ 
		ScriptableWizard.DisplayWizard<MapEventCreateWizard>("创建机制","创建"); 
	} 
	 
	[MenuItem("MapEditor/触发事件/进入触发器",false,301)] 
	static void CreateSceneTriggerIn(){ 
		var trigger = Selection.activeGameObject.GetComponent<SceneTrigger> ();
		if (trigger == null) {
			Debug.LogError ("请先选择触发器");
			return;
		}
		MapEventManager.CreateEventTrigger<MapEventTrigger> (Selection.activeGameObject, MapEventTriggerType.TriggerIn, trigger.objectId.ToString());
	} 

	[MenuItem("MapEditor/触发事件/离开触发器",false,302)] 
	static void CreateSceneTriggerOut(){ 
		var trigger = Selection.activeGameObject.GetComponent<SceneTrigger> ();
		if (trigger == null) {
			Debug.LogError ("请先选择触发器");
			return;
		}
		MapEventManager.CreateEventTrigger<MapEventTrigger> (Selection.activeGameObject, MapEventTriggerType.TriggerOut, trigger.objectId.ToString());
	} 

	[MenuItem("MapEditor/Hidden/NPC/触发死亡事件",false,15)] 
	[MenuItem("MapEditor/触发事件/NPC死亡",false,303)] 
	static void CreateNPCDieTrigger(){ 
		var npc = Selection.activeGameObject.GetComponent<SceneNPC> ();
		if (npc == null) {
			Debug.LogError ("请先选择NPC");
			return;
		}
			
		MapEventManager.CreateEventTrigger<MapEventTrigger> (Selection.activeGameObject, MapEventTriggerType.Die, npc.objectId.ToString());
	}

	[MenuItem("MapEditor/Hidden/NPC/触发被攻击事件",false,16)] 
	[MenuItem("MapEditor/触发事件/NPC被攻击",false,304)] 
	static void CreateNPCBehitTrigger(){ 
		var npc = Selection.activeGameObject.GetComponent<SceneNPC> ();
		if (npc == null) {
			Debug.LogError ("请先选择NPC");
			return;
		}

		MapEventManager.CreateEventTrigger <BehitEventTrigger>(Selection.activeGameObject, MapEventTriggerType.Behit, npc.objectId.ToString());
	}

	[MenuItem("MapEditor/Hidden/NPC/触发释放技能事件",false,16)] 
	[MenuItem("MapEditor/触发事件/NPC释放技能",false,304)] 
	static void CreateNPCAttackTrigger(){ 
		var npc = Selection.activeGameObject.GetComponent<SceneNPC> ();
		if (npc == null) {
			Debug.LogError ("请先选择NPC");
			return;
		}

		MapEventManager.CreateEventTrigger <AttackEventTrigger>(Selection.activeGameObject, MapEventTriggerType.Attack, npc.objectId.ToString());
	}

	[MenuItem("MapEditor/Hidden/NPC/触发NPC掉血事件",false,16)] 
	[MenuItem("MapEditor/触发事件/NPC掉血",false,304)] 
	static void CreateNPCDropHPTrigger(){ 
		var npc = Selection.activeGameObject.GetComponent<SceneNPC> ();
		if (npc == null) {
			Debug.LogError ("请先选择NPC");
			return;
		}

		MapEventManager.CreateEventTrigger <HPDropEventTrigger>(Selection.activeGameObject, MapEventTriggerType.HPChanged, npc.objectId.ToString());
	}
	 
	[MenuItem("MapEditor/道具/创建道具",false,305)] 
	static void CreateItem(){		 
		var mapEvent = MapEventManager.CreateItem("","item",""); 
		Selection.activeGameObject = mapEvent.gameObject;
	}	
	 

	[MenuItem("MapEditor/创建墙体/长方形墙体",false,500)] 
	static void CreateClearWall(){ 
		ScriptableWizard.DisplayWizard<MapWallCreateWizard>("创建墙体","创建");  
	} 


	[MenuItem("MapEditor/音频/创建音频源",false,600)] 
	static void CreateAudioSource(){		 
		var mapEvent = MapEventManager.CreateAudioSource(""); 
		Selection.activeGameObject = mapEvent.gameObject;
	}	

	[MenuItem("MapEditor/场景区域/创建球形场景区域",false,701)] 
	static void CreateSphereArea(){		 
		var mapArea = MapEventManager.CreateMapArea (SceneTriggerShape.Sphere); 
	}	

	[MenuItem("MapEditor/场景区域/创建长方形场景区域",false,702)] 
	static void CreateBoxArea(){		 
		var mapArea = MapEventManager.CreateMapArea (SceneTriggerShape.Box); 
	}
}
