
using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(ScenePathSetting))]
public class ScenePathSettingEditor : Editor
{
    private SerializedProperty StartPoint;
    private SerializedProperty EndPoint;
    private SerializedProperty N;
    private SerializedProperty M;
    private SerializedProperty StartNodeId;
    private SerializedProperty wide;
    private SerializedProperty up;

    void OnEnable()
    {
        StartPoint = serializedObject.FindProperty("StartPoint");
        EndPoint = serializedObject.FindProperty("EndPoint");
        N = serializedObject.FindProperty("N");
        M = serializedObject.FindProperty("M");
        StartNodeId = serializedObject.FindProperty("StartNodeId");
        wide = serializedObject.FindProperty("wide");
        up = serializedObject.FindProperty("up");
    }

    public override void OnInspectorGUI()
    {
        ScenePathSetting setting = (ScenePathSetting)target;
        EditorGUILayout.PropertyField(StartPoint, new GUIContent("开始点"));
        EditorGUILayout.PropertyField(EndPoint, new GUIContent("结束点"));
        EditorGUILayout.PropertyField(N, new GUIContent("长度划分份数"));
        EditorGUILayout.PropertyField(M, new GUIContent("宽度方向上 路点个数（奇数）"));
        EditorGUILayout.PropertyField(StartNodeId, new GUIContent("开始节点号"));
        EditorGUILayout.PropertyField(wide, new GUIContent("宽度间隔"));
        EditorGUILayout.PropertyField(up, new GUIContent("上方向"));
        serializedObject.ApplyModifiedProperties();

        if (GUILayout.Button("生成路点"))
        {
            if (setting.Nodes != null)
            {
                for (int i = 0; i < setting.Nodes.Count; i++)
                {
                    if (setting.Nodes[i] != null)
                        GameObject.DestroyImmediate(setting.Nodes[i].gameObject);
                }
                setting.Nodes.Clear();
            }
           
            setting.Nodes = MapManager.CreateRectNodes(MapManager.current, setting.StartNodeId, setting.length, setting.wide, setting.up, setting.StartPoint.transform.position, setting.EndPoint.transform.position, setting.M,setting.N);
        };
       
    }
}
