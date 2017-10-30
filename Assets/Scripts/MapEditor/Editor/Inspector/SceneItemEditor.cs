using UnityEngine;
using UnityEditor;
using System.Collections;

//[CustomEditor(typeof(SceneItem))]
public class SceneItemEditor : Editor {  

	private SerializedProperty idProperty;
	private SerializedProperty countProperty;  

	void OnEnable(){
		idProperty = serializedObject.FindProperty ("itemId"); 
		countProperty = serializedObject.FindProperty ("requiredKey");   
	}

	public override void OnInspectorGUI ()
	{
		 
		EditorGUILayout.PropertyField (idProperty, new GUIContent("道具ID"));
		EditorGUILayout.PropertyField (countProperty, new GUIContent("需要的碎片数量"));  
		serializedObject.ApplyModifiedProperties ();
	}
}
