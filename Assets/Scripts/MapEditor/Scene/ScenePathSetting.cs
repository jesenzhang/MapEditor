using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePathSetting : MonoBehaviour {

    public GameObject StartPoint;
    public GameObject EndPoint;
    public int M = 3;
    public int N = 2;

    public int StartNodeId=0;
    public float length = 1;
    public float wide = 1;
    public Vector3 up = Vector3.up;

    public List<ScenePathNode> Nodes;
    // Use this for initialization
    void Start () {
      
    }

    private void OnDestroy()
    {
        if (StartPoint != null)
            GameObject.DestroyObject(StartPoint);
        if (EndPoint != null)
            GameObject.DestroyObject(EndPoint);
    }
}
