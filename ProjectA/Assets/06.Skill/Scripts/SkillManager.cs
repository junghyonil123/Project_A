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
            if (instance == null) //instance �� ���������ʴ´ٸ�
            {
                var obj = FindObjectOfType<SkillManager>(); //Player Ÿ���� �����ϴ��� Ȯ��
                if (obj != null)
                {
                    instance = obj; //null�� �ƴ϶�� instance�� �־���
                }
                else
                {
                    var newBattleManager = new GameObject("SkillManager").AddComponent<SkillManager>(); //null�̶�� ���θ������
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
        ////������ ���ÿ� ����Ǵ� Awake�� �̹� �����Ǿ��ִ� �̱��� ������Ʈ�� �ִ��� �˻��ϰ� �ִٸ� ���� ������ ������Ʈ�� �ı�

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
        Debug.Log("SkillEffect_1 �ߵ�");
        return atk += 2;
    }
    
    public int SkillEffect_1(ref int atk)
    {
        Debug.Log("SkillEffect_2 �ߵ�");
        return atk + 3;
    }

    #endregion
}
