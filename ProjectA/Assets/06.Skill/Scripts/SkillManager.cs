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

    public List<Skill> allSkillList = new List<Skill>();
    public List<Skill> playerSkillList = new List<Skill>();

    public void getSkill(Skill skill)
    {
        NoticeCanvas.Instance.NoticeSkill(skill); //��ų ȹ�� â�� ���
        newSkill = Instantiate(skillSlot, content.transform); //��ų������ ��ųâ�� �ϳ� �߰�
        newSkill.GetComponent<SkillSlot>().SetItem(skill); //�߰��� ��ų���Կ� ��ų�� ����

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
        //gameManager�� ������ ������Ʈ �ɶ����� üũ
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
