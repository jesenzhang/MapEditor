using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(SceneMap))]
public class SceneMapEditor : Editor {  

	private SerializedProperty idProperty;
	private SerializedProperty resProperty;
	private SerializedProperty nameProperty;
	private SerializedProperty descProperty;


	void OnEnable(){
		idProperty = serializedObject.FindProperty ("sceneId"); 
		resProperty = serializedObject.FindProperty ("resId"); 
		nameProperty = serializedObject.FindProperty ("sceneName"); 
		descProperty = serializedObject.FindProperty ("sceneDesc"); 
	}

	public override void OnInspectorGUI ()
	{
		 
		EditorGUILayout.PropertyField (idProperty, new GUIContent("场景ID"));
		//EditorGUILayout.PropertyField (nameProperty, new GUIContent("场景名称"));
		EditorGUILayout.PropertyField (resProperty, new GUIContent("场景资源ID"));
		//EditorGUILayout.PropertyField (descProperty, new GUIContent("场景描述"));

		serializedObject.ApplyModifiedProperties ();
	}
}
