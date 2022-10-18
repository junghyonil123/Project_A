using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public GameObject content;
    public GameObject skillSlot;
    private GameObject newSkill;

    public List<Skill> skillList = new List<Skill>();

    public void getSkill(Skill skill)
    {
        newSkill = Instantiate(skillSlot, content.transform);
        newSkill.GetComponent<SkillSlot>().SetItem(skill);
    }

    private void Start()
    {
        getSkill(skillList[0]);
    }
}
