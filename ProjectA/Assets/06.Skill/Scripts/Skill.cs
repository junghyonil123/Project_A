using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType{
    Always,
    OnlyAttack,
    OnlyDefence
}

public abstract class Skill : MonoBehaviour
{
    public string skillName;
    public Sprite skillSprite;
    public string skillExplanation;
    public SkillType skillType;
    public bool isUnlock;

    public abstract void SkillEffect();
    public abstract void SkillUnLock();
}
