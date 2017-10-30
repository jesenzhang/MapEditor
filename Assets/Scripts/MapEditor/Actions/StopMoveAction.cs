using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class StopMoveAction : LifeEventAction
{
    public override string displayName
    {
        get
        {
            return "让" + lifeId + "停止移动";
        }
    }
}
