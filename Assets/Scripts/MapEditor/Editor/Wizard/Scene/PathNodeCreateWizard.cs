using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

public class PathNodeCreateWizard : ScriptableWizard
{
    SceneObjectBuilder builder;
    public int nodeID = -1;
    public List<int> prevNodeIDs = new List<int>();

    private void OnWizardCreate()
    {
        MapManager.CreatePathNode(MapManager.current, nodeID, prevNodeIDs);
    }

    private void OnWizardUpdate()
    {
        errorString = "";
        isValid = true;

        if (nodeID < 0)
        {
            errorString = "enter valid path node id";
            isValid = false;
            return;
        }

        if (nodeID > 0)
        {
            if (prevNodeIDs.Count == 0)
            {
                errorString = "enter valid prev path node id, only node(0) can set no prev node";
                isValid = false;
            }
        }
    }
}
