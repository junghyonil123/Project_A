using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongFoot : Skill
{
    public override void SkillEffect()
    {
        Player.Instance.maxActivePoint += 3;
    }

    public override void SkillUnLock()
    {
        if (GameManager.Instance.moveCount >=3)
        {
            isUnlock = true;
            SkillManager.Instance.getSkill(this);
        }
    }
}
