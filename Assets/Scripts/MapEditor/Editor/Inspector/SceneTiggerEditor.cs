using UnityEngine;
using UnityEditor;
using System.Collections;

//[CustomEditor(typeof(SceneTrigger))]
public class SceneTriggerEditor : Editor {  

	private SerializedProperty idProperty;
	private SerializedProperty triggerTypeProperty; 
	private SerializedProperty triggerShapeProperty;
	private SerializedProperty triggerDataProperty;  
	private SerializedProperty triggerObjProperty;  

	void OnEnable(){
		idProperty = serializedObject.FindProperty ("objectId"); 
		triggerTypeProperty = serializedObject.FindProperty ("triggerType");  
		triggerShapeProperty = serializedObject.FindProperty ("shape");  
		triggerDataProperty = serializedObject.FindProperty ("triggerData");   
		triggerObjProperty = serializedObject.FindProperty ("obj");   
	}

	public override void OnInspectorGUI ()
	{
		 
		EditorGUILayout.PropertyField (idProperty, new GUIContent("触发器ID"));
		EditorGUILayout.PropertyField (triggerTypeProperty, new GUIContent("触发器类型")); 
		EditorGUILayout.PropertyField (triggerShapeProperty, new GUIContent("触发器形状")); 
		EditorGUILayout.PropertyField (triggerDataProperty, new GUIContent("触发器数据")); 
		EditorGUILayout.PropertyField (triggerObjProperty, new GUIContent ("目标点对象")); 
		serializedObject.ApplyModifiedProperties ();
	}
}
