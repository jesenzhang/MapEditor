using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(SceneNPCGroup))]
public class NPCGroupEditor : Editor {  

	private SerializedProperty idProperty;
	private SerializedProperty delayProperty; 
	private SerializedProperty nextProperty; 


	void OnEnable(){
		idProperty = serializedObject.FindProperty ("groupId"); 
		delayProperty = serializedObject.FindProperty ("delayTime");  
		nextProperty = serializedObject.FindProperty ("nextGroupId");  
	}

	public override void OnInspectorGUI ()
	{
		 
		EditorGUILayout.PropertyField (idProperty, new GUIContent("NPC组ID"));
		EditorGUILayout.PropertyField (delayProperty, new GUIContent("延迟(毫秒)")); 
		EditorGUILayout.PropertyField (nextProperty, new GUIContent("下一组ID)")); 

		serializedObject.ApplyModifiedProperties ();
	}
}
