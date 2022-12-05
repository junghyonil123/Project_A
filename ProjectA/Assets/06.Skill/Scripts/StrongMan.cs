using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongMan : Skill
{
    public override void SkillEffect()
    {
        Player.Instance.atk += 3;
    }

    public override void SkillUnLock()
    {
        SkillManager.Instance.getSkill(this);
    }
}
