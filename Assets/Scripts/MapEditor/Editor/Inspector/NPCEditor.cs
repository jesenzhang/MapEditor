using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(SceneNPC))]
public class NPCEditor : Editor {  

	private SerializedProperty idProperty;
	private SerializedProperty resProperty; 
	private SerializedProperty fractionProperty; 
	private SerializedProperty posProperty; 
	private SerializedProperty rotProperty; 

	void OnEnable(){
		idProperty = serializedObject.FindProperty ("objectId"); 
		resProperty = serializedObject.FindProperty ("resId");  
		fractionProperty = serializedObject.FindProperty ("fraction");  
		posProperty = serializedObject.FindProperty ("worldPos");  
		rotProperty = serializedObject.FindProperty ("worldRotation");  
	}

	public override void OnInspectorGUI ()
	{
		 
		EditorGUILayout.PropertyField (idProperty, new GUIContent("NPC ID"));
		EditorGUILayout.PropertyField (resProperty, new GUIContent("资源ID")); 
		EditorGUILayout.PropertyField (fractionProperty, new GUIContent("阵营")); 
		EditorGUILayout.PropertyField (posProperty, new GUIContent("世界坐标")); 
		EditorGUILayout.PropertyField (rotProperty, new GUIContent("朝向")); 

		serializedObject.ApplyModifiedProperties ();
	}
}
