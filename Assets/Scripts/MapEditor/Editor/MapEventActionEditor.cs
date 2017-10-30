using UnityEngine;
using UnityEditor;
using System.Collections;

public class MapEventActionEditor  {
	 

	[MenuItem("MapEditor/创建动作/提示使用道具",false,401)] 
	static void CreateNeedItemAction(){ 
		ScriptableWizard.DisplayWizard<UseItemActionCreateWizard>("创建动作","创建"); 
	}
	 
	[MenuItem("MapEditor/创建动作/提示拾取道具",false,402)] 
	static void CreatePickupItemAction(){ 
		ScriptableWizard.DisplayWizard<PickupItemActionCreateWizard>("创建动作","创建"); 
	} 

	[MenuItem("MapEditor/创建动作/隐藏拾取道具提示",false,403)] 
	static void CreateHidePickupItemAction(){ 
		MapEventActionManager.CreateAction<MapEventAction> (Selection.activeGameObject,Example.MapEventAction.Type.HIDE_PICKUP,null);
	}

	[MenuItem("MapEditor/创建动作/隐藏使用道具提示",false,404)] 
	static void CreateHideUseItemAction(){ 
		MapEventActionManager.CreateAction<MapEventAction> (Selection.activeGameObject,Example.MapEventAction.Type.HIDE_USEITEM,null);
	} 

	[MenuItem("MapEditor/Hidden/Wall/添加动作/清理墙体",false,404)] 
	[MenuItem("MapEditor/创建动作/清理墙体",false,404)] 
	static void CreateClearWallAction(){ 
		ScriptableWizard.DisplayWizard<ClearWallActionCreateWizard>("创建动作","创建"); 
	} 

	[MenuItem("MapEditor/创建动作/激活墙体",false,404)] 
	static void CreateResetWallAction(){ 
		ScriptableWizard.DisplayWizard<ResetWallActionCreateWizard>("创建动作","创建"); 
	} 
	 
	[MenuItem("MapEditor/创建动作/产生新的npc组",false,405)] 
	static void CreateNewGroupAction(){ 
		ScriptableWizard.DisplayWizard<GenerateNewGroupActionCreateWizard>("创建动作","创建"); 
	} 
	 
	[MenuItem("MapEditor/创建动作/掉落道具",false,405)] 
	static void CreateDropItemAction(){ 
		ScriptableWizard.DisplayWizard<DropItemActionCreateWizard>("创建动作","创建"); 
	}

	[MenuItem("MapEditor/创建动作/销毁触发器",false,405)] 
	static void CreateDestroyTriggerAction(){ 
		ScriptableWizard.DisplayWizard<DestroyTriggerActionCreateWizard>("创建动作","创建"); 
	}

	[MenuItem("MapEditor/创建动作/激活触发器",false,405)] 
	static void CreateResetTriggerAction(){ 
		ScriptableWizard.DisplayWizard<ResetTriggerActionCreateWizard>("创建动作","创建"); 
	}

	[MenuItem("MapEditor/创建动作/显示UI",false,406)] 
	static void CreateShowUIAction(){ 
		ScriptableWizard.DisplayWizard<ShowUIEventActionCreateWizard>("创建动作","创建"); 
	}

	[MenuItem("MapEditor/创建动作/隐藏UI",false,407)] 
	static void CreateHideUIAction(){ 
		ScriptableWizard.DisplayWizard<HideUIEventActionCreateWizard>("创建动作","创建"); 
	}

	[MenuItem("MapEditor/创建动作/显示路径点",false,407)] 
	static void CreateShowWayPointAction(){ 
		ScriptableWizard.DisplayWizard<ShowWayPointEventActionCreateWizard>("创建动作","创建"); 
	}

	[MenuItem("MapEditor/创建动作/播放剧情",false,408)] 
	static void CreatePlaySequenceAction(){ 
		ScriptableWizard.DisplayWizard<PlaySequeueEventActionCreateWizard>("创建动作","创建");  
	}

	[MenuItem("MapEditor/创建动作/开始自动战斗",false,409)] 
	static void CreateStartAutoFightAction(){ 
		var action = MapEventActionManager.CreateAction<StartAutoFightAction> (Selection.activeGameObject, Example.MapEventAction.Type.START_AUTOFIGHT, null);
		Selection.activeGameObject = action.gameObject;
	}

	[MenuItem("MapEditor/创建动作/停止自动战斗",false,410)] 
	static void CreateStopAutoFightAction(){ 
		var action = MapEventActionManager.CreateAction<StopAutoFightAction> (Selection.activeGameObject, Example.MapEventAction.Type.STOP_AUTOFIGHT, null);
		Selection.activeGameObject = action.gameObject;
	} 

	[MenuItem("MapEditor/创建动作/添加BUFF",false,411)] 
	static void CreateAddBuffAction(){ 
		var action = MapEventActionManager.CreateAction<AddBuffAction> (Selection.activeGameObject, Example.MapEventAction.Type.ADD_BUFF, null);
		Selection.activeGameObject = action.gameObject;
	}

	[MenuItem("MapEditor/创建动作/移除BUFF",false,411)] 
	static void CreateDelBuffAction(){ 
		var action = MapEventActionManager.CreateAction<DelBuffAction> (Selection.activeGameObject, Example.MapEventAction.Type.DEL_BUFF, null);
		Selection.activeGameObject = action.gameObject;
	}

	[MenuItem("MapEditor/创建动作/修改阵营",false,412)] 
	static void CreateChangeFractionAction(){ 
		var action = MapEventActionManager.CreateAction<ChangeFractionAction> (Selection.activeGameObject, Example.MapEventAction.Type.CHANGE_FRACTION, null);
		Selection.activeGameObject = action.gameObject;
	}

	[MenuItem("MapEditor/创建动作/播放动作",false,413)] 
	static void CreatePlayModelAction(){ 
		var action = MapEventActionManager.CreateAction<PlayModelAction> (Selection.activeGameObject, Example.MapEventAction.Type.PLAY_ACTION, null);
		Selection.activeGameObject = action.gameObject;
	}

	[MenuItem("MapEditor/创建动作/移动到目标点",false,414)] 
	static void CreateMoveToAction(){ 
		var action = MapEventActionManager.CreateAction<MoveToAction> (Selection.activeGameObject, Example.MapEventAction.Type.MOVE_TO, null);
		Selection.activeGameObject = action.gameObject;
	}

    [MenuItem("MapEditor/创建动作/弹出气泡对话",false,415)] 
	static void CreateShowPopAction(){ 
		var action = MapEventActionManager.CreateAction<ShowTalkWordAction> (Selection.activeGameObject, Example.MapEventAction.Type.SHOW_TALK_WORD, null); 
		Selection.activeGameObject = action.gameObject;
	}

	[MenuItem("MapEditor/创建动作/弹出带头像的对话",false,415)] 
	static void CreateShowDialogPopAction(){ 
		var action = MapEventActionManager.CreateAction<ShowTalkWordAction> (Selection.activeGameObject, Example.MapEventAction.Type.SHOW_TALK_WORD, null);  
		Selection.activeGameObject = action.gameObject;
	}

	[MenuItem("MapEditor/创建动作/冻结或开启AI",false,416)] 
	static void CreateFreezeAIAction(){ 
		var action = MapEventActionManager.CreateAction<FreezeAIAction> (Selection.activeGameObject, Example.MapEventAction.Type.FREEZE_AI, null);
		Selection.activeGameObject = action.gameObject;
	}

	[MenuItem("MapEditor/创建动作/设置目标boss",false,417)] 
	static void CreateSetBossAction(){ 
		var action = MapEventActionManager.CreateAction<SetAsTargetBossAction> (Selection.activeGameObject, Example.MapEventAction.Type.SET_ASBOSS, null);
		Selection.activeGameObject = action.gameObject;
	}

	[MenuItem("MapEditor/创建动作/接取任务",false,418)] 
	static void CreateAcceptTaskAction(){ 
		var action = MapEventActionManager.CreateAction<AcceptTaskAction> (Selection.activeGameObject, Example.MapEventAction.Type.ACCEPT_TASK, null);
		Selection.activeGameObject = action.gameObject;
	}

	[MenuItem("MapEditor/创建动作/提交任务",false,419)] 
	static void CreateSubmitTaskAction(){ 
		var action = MapEventActionManager.CreateAction<SubmitTaskAction> (Selection.activeGameObject, Example.MapEventAction.Type.SUBMIT_TASK, null);
		Selection.activeGameObject = action.gameObject;
	}

	[MenuItem("MapEditor/创建动作/重置任务",false,419)] 
	static void CreateResetTaskAction(){ 
		var action = MapEventActionManager.CreateAction<ResetTaskAction> (Selection.activeGameObject, Example.MapEventAction.Type.RESET_TASK, null);
		Selection.activeGameObject = action.gameObject;
	}

	[MenuItem("MapEditor/创建动作/显示npc功能",false,420)] 
	static void CreateShowNPCFuncAction(){ 
		var action = MapEventActionManager.CreateAction<ShowNPCFuncAction> (Selection.activeGameObject, Example.MapEventAction.Type.GET_NPCFUNCS, null);
		Selection.activeGameObject = action.gameObject;
	}

	[MenuItem("MapEditor/创建动作/飞到某个点",false,421)] 
	static void CreateFlyAction(){ 
		var action = MapEventActionManager.CreateAction<FlyToAction> (Selection.activeGameObject, Example.MapEventAction.Type.FLY_TO, null);
		Selection.activeGameObject = action.gameObject;
	}

	[MenuItem("MapEditor/创建动作/播放特效",false,422)] 
	static void CreatePlayEffect(){ 
		var action = MapEventActionManager.CreateAction<PlayEffectAction> (Selection.activeGameObject, Example.MapEventAction.Type.PLAY_EFFECT, null);
		Selection.activeGameObject = action.gameObject;
	}

	[MenuItem("MapEditor/创建动作/传送",false,423)] 
	static void CreateTransferAction(){ 
		var action = MapEventActionManager.CreateAction<TransferAction> (Selection.activeGameObject, Example.MapEventAction.Type.TRANSFER_TO, null);
		Selection.activeGameObject = action.gameObject;
	}

	[MenuItem("MapEditor/创建动作/显示传送点",false,424)] 
	static void CreateShowTransferAction(){ 
		var action = MapEventActionManager.CreateAction<ShowTransferAction> (Selection.activeGameObject, Example.MapEventAction.Type.SHOW_TRANSFER, null);
		Selection.activeGameObject = action.gameObject;
	}

	[MenuItem("MapEditor/创建动作/隐藏传送点",false,426)] 
	static void CreateHideTransferAction(){ 
		var action = MapEventActionManager.CreateAction<HideTransferAction> (Selection.activeGameObject, Example.MapEventAction.Type.HIDE_TRANSFER, null);
		Selection.activeGameObject = action.gameObject;
	}

	[MenuItem("MapEditor/创建动作/触发事件",false,427)] 
	static void CreateExecuteEventAction(){ 
		var action = MapEventActionManager.CreateAction<ExecuteEventAction> (Selection.activeGameObject, Example.MapEventAction.Type.EXECUTE_EVENT, null);
		Selection.activeGameObject = action.gameObject;
	}

	[MenuItem("MapEditor/创建动作/提示功能按钮",false,428)] 
	static void CreateShowPropsAction(){ 
		var action = MapEventActionManager.CreateAction<ShowPropUI> (Selection.activeGameObject, Example.MapEventAction.Type.SHOW_PROP_UI, null);
		Selection.activeGameObject = action.gameObject;
	}

	[MenuItem("MapEditor/创建动作/隐藏功能按钮",false,429)] 
	static void CreateHidePropsAction(){ 
		var action = MapEventActionManager.CreateAction<HidePropUI> (Selection.activeGameObject, Example.MapEventAction.Type.HIDE_UI, null);
		Selection.activeGameObject = action.gameObject;
	}
	 

	[MenuItem("MapEditor/创建动作/设置镜头文件",false,430)] 
	static void CreateCameraSettingAction(){ 
		var action = MapEventActionManager.CreateAction<CameraSettingAction> (Selection.activeGameObject, Example.MapEventAction.Type.CAMERA_SETTING, null);
		Selection.activeGameObject = action.gameObject;
	}

    [MenuItem("MapEditor/创建动作/播放技能", false, 431)]
    static void CreatePlaySkillAction()
    {
        var action = MapEventActionManager.CreateAction<PlaySkillAction>(Selection.activeGameObject, Example.MapEventAction.Type.PLAY_SKILL, null);
        Selection.activeGameObject = action.gameObject;
    }

	[MenuItem("MapEditor/创建动作/按路点飞行", false, 432)]
	static void CreateFlyByWaypointAction()
	{
		var action = MapEventActionManager.CreateAction<FlyingByWayPointAction>(Selection.activeGameObject, Example.MapEventAction.Type.REGULAR_FLY, null);
		Selection.activeGameObject = action.gameObject;
	}

	[MenuItem("MapEditor/创建动作/显示或隐藏物体", false, 433)]
	static void ActiveGameObjectAction()
	{
		var action = MapEventActionManager.CreateAction<ActiveObjectAction>(Selection.activeGameObject, Example.MapEventAction.Type.ACTIVE_GAMEOBJECT, null);
		Selection.activeGameObject = action.gameObject;
	}

	[MenuItem("MapEditor/创建动作/让物品破碎", false, 434)]
	static void BrakeGameObjectAction()
	{
		var action = MapEventActionManager.CreateAction<BrakeObjectAction>(Selection.activeGameObject, Example.MapEventAction.Type.BRAKE_GAMEOBJECT, null);
		Selection.activeGameObject = action.gameObject;
	}

    [MenuItem("MapEditor/创建动作/停止移动", false, 433)]
    static void CreateStopAction()
    {
        var action = MapEventActionManager.CreateAction<StopMoveAction>(Selection.activeGameObject, Example.MapEventAction.Type.STOP_MOVE, null);
        Selection.activeGameObject = action.gameObject;
    }

	[MenuItem("MapEditor/创建动作/修改音效", false, 434)]
	static void CreateChangeAudioAction()
	{
		var action = MapEventActionManager.CreateAction<ChangeAudioAction>(Selection.activeGameObject, Example.MapEventAction.Type.CHANGE_AUDIO, null);
		Selection.activeGameObject = action.gameObject;
	}


	[MenuItem("MapEditor/创建动作/打开音频源", false, 435)]
	static void CreatePlayMusicAction()
	{
		var action = MapEventActionManager.CreateAction<PlayMusicAction>(Selection.activeGameObject, Example.MapEventAction.Type.OPEN_MUSIC, null);
		Selection.activeGameObject = action.gameObject;
	}

	[MenuItem("MapEditor/创建动作/关闭音频源", false, 436)]
	static void CreateStopMusicAction()
	{
		var action = MapEventActionManager.CreateAction<StopMusicAction>(Selection.activeGameObject, Example.MapEventAction.Type.STOP_MUSIC, null);
		Selection.activeGameObject = action.gameObject;
	}

	[MenuItem("MapEditor/创建动作/打开新手引导", false, 437)]
	static void CreateStartGuideAction()
	{
		var action = MapEventActionManager.CreateAction<UserGuideAction>(Selection.activeGameObject, Example.MapEventAction.Type.BEGIN_GUIDE, null);
		action.begin = true;
		Selection.activeGameObject = action.gameObject;
	}

	[MenuItem("MapEditor/创建动作/关闭新手引导", false, 438)]
	static void CreateStopGuideAction()
	{
		var action = MapEventActionManager.CreateAction<UserGuideAction>(Selection.activeGameObject, Example.MapEventAction.Type.BEGIN_GUIDE, null);
		action.begin = false;
		Selection.activeGameObject = action.gameObject;
	}

	[MenuItem("MapEditor/创建动作/清理副本", false, 439)]
	static void CreateCleanMapAction()
	{
		var action = MapEventActionManager.CreateAction<CleanMapAction>(Selection.activeGameObject, Example.MapEventAction.Type.CLEAN_MAP, null); 
		Selection.activeGameObject = action.gameObject;
	}

	[MenuItem("MapEditor/创建动作/提交副本", false, 440)]
	static void CreateSubmitDungenonAction()
	{
		var action = MapEventActionManager.CreateAction<SubmitDungenonAcion>(Selection.activeGameObject, Example.MapEventAction.Type.SUBMIT_DUNGENON, null); 
		Selection.activeGameObject = action.gameObject;
	}
}
