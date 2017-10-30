using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PlaySkillAction : LifeEventAction
{
    public int skillIndex;

    public override ActionArgValue[] Argments
    {
        get
        {
            args = new ActionArgValue[3];
            args[0].intValue = (int)lifeType;
            args[1].strValue = lifeId;
            args[2].intValue = skillIndex;
            return args;
        }
    }

    public override string displayName
    {
        get
        {
            return "让" + lifeId + "释放" + skillIndex + "号技能";
        }
    }
}
