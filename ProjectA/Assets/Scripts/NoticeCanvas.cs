using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

[Serializable]
public struct NoticeBoard
{
    public GameObject noticeBoard;
    public Image itemImage;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemExplanation;
}

public class NoticeCanvas : MonoBehaviour
{
    #region Singleton
    private static NoticeCanvas instance;
    public static NoticeCanvas Instance
    {
        get
        {
            if (instance == null) //instance �� ���������ʴ´ٸ�
            {
                var obj = FindObjectOfType<NoticeCanvas>(); //Player Ÿ���� �����ϴ��� Ȯ��
                if (obj != null)
                {
                    instance = obj; //null�� �ƴ϶�� instance�� �־���
                }
                else
                {
                    var newBattleManager = new GameObject("NoticeCanvas").AddComponent<NoticeCanvas>(); //null�̶�� ���θ������
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

        var objs = FindObjectsOfType<NoticeCanvas>();
        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }

    }
    #endregion
    

    [SerializeField]
    private List<NoticeBoard> noticeBoardList;

    public void NoticeSkill(Skill skill)
    {
        for (int i = 0; i < noticeBoardList.Count; i++)
        {
            if (!noticeBoardList[i].noticeBoard.activeSelf) //���� ù��° ������� Ȯ���ؼ� �����ִٸ� Ŵ
            {
                GameManager.Instance.openedUiCount += 1; //Ui�� ������ Uiī��Ʈ�� �ϳ��ø�
                noticeBoardList[i].noticeBoard.SetActive(true);
                noticeBoardList[i].itemImage.sprite = skill.skillSprite;
                noticeBoardList[i].itemName.text = skill.skillName;
                noticeBoardList[i].itemExplanation.text = skill.skillExplanation;
                break;
            }
        }
    }

    public void Confirm()
    {
        for (int i = noticeBoardList.Count - 1; i >= 0; i--) //���� �������� �����ִ� �ͺ��� Ȯ���� �ϳ��� ������
        {
            if (noticeBoardList[i].noticeBoard.activeSelf)
            {
                noticeBoardList[i].noticeBoard.SetActive(false);
                
                if (i == 0)
                {
                    GameManager.Instance.openedUiCount -= 1;
                }

                break;
            }
        }
    }
    
}
