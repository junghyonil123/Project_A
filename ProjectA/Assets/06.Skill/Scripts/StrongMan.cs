using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongMan : Skill
{
    public override void SkillEffect()
    {
        Debug.Log("적용됫습니다");
        Player.Instance.atk += 3;
        Debug.Log(Player.Instance.atk);
    }

    public override void SkillUnLock()
    {
        SkillManager.Instance.getSkill(this);
    }
}
