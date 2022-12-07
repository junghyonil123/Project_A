using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SkillManager : MonoBehaviour
{
    #region Singleton
    private static SkillManager instance;
    public static SkillManager Instance
    {
        get
        {
            if (instance == null) //instance 가 존재하지않는다면
            {
                var obj = FindObjectOfType<SkillManager>(); //Player 타입이 존재하는지 확인
                if (obj != null)
                {
                    instance = obj; //null이 아니라면 instance에 넣어줌
                }
                else
                {
                    var newBattleManager = new GameObject("SkillManager").AddComponent<SkillManager>(); //null이라면 새로만들어줌
                    instance = newBattleManager;
                }
            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }

    private void Awake()
    {
        ////생성과 동시에 실행되는 Awake는 이미 생성되어있는 싱글톤 오브젝트가 있는지 검사하고 있다면 지금 생성된 오브젝트를 파괴

        var objs = FindObjectsOfType<SkillManager>();
        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }

    }
    #endregion

    public GameObject content;
    public GameObject skillSlot;
    private GameObject newSkill;

    public List<Skill> allSkillList = new List<Skill>();
    public List<Skill> playerSkillList = new List<Skill>();

    public void getSkill(Skill skill)
    {
        NoticeCanvas.Instance.NoticeSkill(skill); //스킬 획득 창을 띄움
        newSkill = Instantiate(skillSlot, content.transform); //스킬슬롯을 스킬창에 하나 추가
        newSkill.GetComponent<SkillSlot>().SetItem(skill); //추가한 스킬슬롯에 스킬을 저장

        switch (skill.skillType)
        {
            case SkillType.Always:
                skill.SkillEffect();
                StatusCanvas.Instance.SetStatus();
                break;
            case SkillType.OnlyAttack:
                break;
            case SkillType.OnlyDefence:
                break;
            default:
                break;
        }

        playerSkillList.Add(skill);
    }

    public void CheckSkillUnlock()
    {
        //gameManager에 정보가 업데이트 될때마다 체크
        if (gameObject)
        {

        }

        for (int i = 0; i < allSkillList.Count; i++)
        {
            if (playerSkillList.Contains(allSkillList[i]))
            {
                continue;
            }
            allSkillList[i].SkillUnLock();
        }
    }   
}
