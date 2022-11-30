using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType{
    Always,
    OnlyAttack,
    OnlyDefence
}

public class Skill : MonoBehaviour
{
    public string skillName;
    public Sprite skillSprite;
    public string skillExplanation;
    public int skillNumber;
    public SkillType skillType;
}
