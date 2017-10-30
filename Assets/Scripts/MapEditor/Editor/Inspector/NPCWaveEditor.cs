using UnityEngine;
using UnityEditor;
using System.Collections;

//[CustomEditor(typeof(SceneNPCWave))]
public class NPCWaveEditor : Editor {  

	private SerializedProperty seqProperty;
	private SerializedProperty delayProperty; 


	void OnEnable(){
		seqProperty = serializedObject.FindProperty ("waveSequence"); 
		delayProperty = serializedObject.FindProperty ("delayTime");  
	}

	public override void OnInspectorGUI ()
	{
		var wave = (target as SceneNPCWave);

		int seq = wave.waveSequence;

		EditorGUILayout.PropertyField (seqProperty, new GUIContent ("波次"));
		EditorGUILayout.PropertyField (delayProperty, new GUIContent("延迟(毫秒)"));  


		serializedObject.ApplyModifiedProperties ();

		if (seq != wave.waveSequence) { 
			wave.gameObject.name = wave.displayName;
		}
	}
}
