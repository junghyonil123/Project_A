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
            if (instance == null) //instance 가 존재하지않는다면
            {
                var obj = FindObjectOfType<NoticeCanvas>(); //Player 타입이 존재하는지 확인
                if (obj != null)
                {
                    instance = obj; //null이 아니라면 instance에 넣어줌
                }
                else
                {
                    var newBattleManager = new GameObject("NoticeCanvas").AddComponent<NoticeCanvas>(); //null이라면 새로만들어줌
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
            if (!noticeBoardList[i].noticeBoard.activeSelf) //가장 첫번째 보드부터 확인해서 꺼져있다면 킴
            {
                GameManager.Instance.openedUiCount += 1; //Ui가 켜질때 Ui카운트를 하나올림
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
        for (int i = noticeBoardList.Count - 1; i >= 0; i--) //가장 마지막에 켜져있는 것부터 확인해 하나씩 종료함
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
