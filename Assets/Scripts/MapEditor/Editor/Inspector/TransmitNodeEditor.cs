using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(TransmitNode))]
public class TransmitNodeEditor : Editor {  

	private SerializedProperty idProperty;
	private SerializedProperty mapIdProperty; 
	private SerializedProperty nodeIdProperty; 
	private SerializedProperty effProperty; 
	private SerializedProperty posProperty; 

	void OnEnable(){
		idProperty = serializedObject.FindProperty ("objectId"); 
		mapIdProperty = serializedObject.FindProperty ("mapId");  
		nodeIdProperty = serializedObject.FindProperty ("nodeId");  
		effProperty = serializedObject.FindProperty ("effectName");  
		posProperty = serializedObject.FindProperty ("worldPos");  
	}

	public override void OnInspectorGUI ()
	{
		 
		EditorGUILayout.PropertyField (idProperty, new GUIContent("跳转点ID"));
		EditorGUILayout.PropertyField (mapIdProperty, new GUIContent("跳转到的地图ID")); 
		EditorGUILayout.PropertyField (nodeIdProperty, new GUIContent("跳转到的地图上的挑战点")); 
		EditorGUILayout.PropertyField (effProperty, new GUIContent("跳转点特效")); 
		EditorGUILayout.PropertyField (posProperty, new GUIContent("世界坐标")); 

		serializedObject.ApplyModifiedProperties ();
	}
}
