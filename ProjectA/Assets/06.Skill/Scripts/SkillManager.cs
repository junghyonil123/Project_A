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


    public List<Skill> skillList = new List<Skill>();

    public void getSkill(Skill skill)
    {
        newSkill = Instantiate(skillSlot, content.transform);
        newSkill.GetComponent<SkillSlot>().SetItem(skill);

        switch (skill.skillType)
        {
            case SkillType.Always:

                switch (skill.skillNumber)
                {
                    case 1:
                        break;

                    case 2:
                        break;

                    default:
                        break;
                }
                break;

            case SkillType.OnlyAttack:

                switch (skill.skillNumber)
                {
                    case 0:
                        Player.Instance.AttackSkillDelegate += SkillEffect_0;
                        break;

                    case 1:
                        Player.Instance.AttackSkillDelegate += SkillEffect_1;
                        break;

                    default:
                        break;
                }
                break;

            case SkillType.OnlyDefence:

                break;
            default:
                break;
        }
    }

    private void Start()
    {
        getSkill(skillList[0]);
    }


    private void Update()
    {
        
    }

    private void CheckSkillUnlock()
    {

    }

    #region AttackSkillList

    public int SkillEffect_0(ref int atk)
    {
        Debug.Log("SkillEffect_1 발동");
        return atk += 2;
    }
    
    public int SkillEffect_1(ref int atk)
    {
        Debug.Log("SkillEffect_2 발동");
        return atk + 3;
    }

    #endregion
}
