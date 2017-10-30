using UnityEngine;
using UnityEditor;
using System.Collections;

public class MapEventActionManager  {

	public static T CreateAction<T>(GameObject target,Example.MapEventAction.Type actionType,string actionTarget) where T : MapEventAction{ 
		
		var go = new GameObject ("动作"); 
		go.transform.SetParent (target.transform);
		go.transform.position = target.transform.position; 

		var node = go.AddComponent<T> ();   
		node.actionType = actionType; 
		node.actionTarget = actionTarget;  

		go.name = node.displayName;

		return node;
	}

	public static void CreateDropItemAction(GameObject target,SceneItem itemObj,int count){  

		var action = CreateAction<DropItemAction> (target, Example.MapEventAction.Type.DROP_ITEM,itemObj.itemId);	
		action.itemObj = itemObj;
		action.dropCount = count;
		Selection.activeGameObject = action.gameObject;
	}

	public static void CreatePickupItemAction(GameObject target,SceneItem itemObj){  

		var action = CreateAction<PickupItemAction> (target, Example.MapEventAction.Type.SHOW_PICKUP,itemObj.itemId);	 
		action.itemObj = itemObj;
		Selection.activeGameObject = action.gameObject;
	}

	public static void CreateUseItemAction(GameObject target,SceneItem itemObj){  

		var action = CreateAction<UseItemAction> (target, Example.MapEventAction.Type.SHOW_USEITEM,itemObj.itemId);	 
		action.itemObj = itemObj;
		Selection.activeGameObject = action.gameObject;
	}

	public static void CreateClearWallAction(GameObject target,MapWall wall,int  winKey){  
		var action = CreateAction<ClearWallAction> (target, Example.MapEventAction.Type.CLEARWALL,null);  
		action.wall = wall;
		action.key = winKey;

		Selection.activeGameObject = action.gameObject;
	}

	public static void CreateResetWallAction(GameObject target,MapWall wall,bool  show){  
		var action = CreateAction<ResetWallAction> (target, Example.MapEventAction.Type.SHOW_WALL,null);  
		action.wall = wall;
		action.show = show;

		Selection.activeGameObject = action.gameObject;
	}

	public static void CreateNewGroupAction(GameObject target,SceneNPCGroup group){  
		var action = CreateAction<GenerateNewGroupAction> (target, Example.MapEventAction.Type.OPEN_NPC_GROUP,null);  
		action.group = group; 

		Selection.activeGameObject = action.gameObject;
	}

	public static void CreateDestroyTriggerAction(GameObject target,SceneTrigger trigger){  
		var action = CreateAction<TriggerEventAction> (target, Example.MapEventAction.Type.DESTROY_TRIGGER,null);  
		action.triggerObj = trigger;  
		Selection.activeGameObject = action.gameObject;
	}

	public static void CreateResetTriggerAction(GameObject target,SceneTrigger trigger){  
		var action = CreateAction<TriggerEventAction> (target, Example.MapEventAction.Type.RESET_TRIGGER,null);  
		action.triggerObj = trigger;  
		Selection.activeGameObject = action.gameObject;
	}

}
