using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatusCanvas : MonoBehaviour
{

    #region Singleton
    private static StatusCanvas instance;
    public static StatusCanvas Instance
    {
        get
        {
            if (instance == null) //instance 가 존재하지않는다면
            {
                var obj = FindObjectOfType<StatusCanvas>(); //Player 타입이 존재하는지 확인
                if (obj != null)
                {
                    instance = obj; //null이 아니라면 instance에 넣어줌
                }
                else
                {
                    var newBattleManager = new GameObject("StatusCanvas").AddComponent<StatusCanvas>(); //null이라면 새로만들어줌
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

        var objs = FindObjectsOfType<StatusCanvas>();
        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }

    }
    #endregion

    public GameObject statusWindow;

    public TextMeshProUGUI jobText;
    public TextMeshProUGUI atkText;
    public TextMeshProUGUI defText;
    public TextMeshProUGUI hpText;
    
    public TextMeshProUGUI strText;
    public TextMeshProUGUI dexText;
    public TextMeshProUGUI conText;

    public TextMeshProUGUI statusPointText;

    public List<GameObject> uiWindowList = new List<GameObject>();

    public GameObject explanationCanvas;

    public bool isOpenCanvas = true;


    private void Start()
    {
        statusWindow.GetComponent<RectTransform>().Translate(new Vector3(-500, 0, 0));
    }

    public void WindowOnOff(string name)
    {
        for (int i = 0; i < uiWindowList.Count; i++)
        {
            if (uiWindowList[i].name == name)
            {
                if (uiWindowList[i].GetComponent<RectTransform>().position.x < 0)
                {
                    uiWindowList[i].GetComponent<RectTransform>().Translate(new Vector3(+485, 0, 0));
                }
            }
            else
            {
                if (uiWindowList[i].GetComponent<RectTransform>().position.x > 0)
                {
                    uiWindowList[i].GetComponent<RectTransform>().Translate(new Vector3(-485, 0, 0));
                }
            }
        }
        SetStatus();
    }

    public void ProfilWindowOnOff()
    {
        WindowOnOff("ProfileWindow");
    }

    public void EquipmentWindowOnOff()
    {
        WindowOnOff("EquipmentWindow");
    }

    public void SkillWindowOnOff()
    {
        WindowOnOff("SkillWindow");
    }

    public void InventoryWindowOnOff()
    {
        WindowOnOff("InventoryWindow");
    }

    public void OnOffStatusWindow()
    {
        if (!isOpenCanvas)
        {
            //켜져있다면 끈다
            GameManager.Instance.openedUiCount -= 1;
            statusWindow.GetComponent<RectTransform>().Translate(new Vector3(-485, 0, 0));
            isOpenCanvas = true;
            explanationCanvas.SetActive(false);

        }
        else
        {
            GameManager.Instance.openedUiCount += 1;
            statusWindow.GetComponent<RectTransform>().Translate(new Vector3(485, 0, 0));
            isOpenCanvas = false;
        }
        SetStatus();
    }

    public void SetStatus()
    {
         //플레이어의 스탯수치를 갱신해줌

        jobText.text = Player.Instance.job;
        atkText.text = "공격력 :" +Player.Instance.atk;
        defText.text = "방어력 :" + Player.Instance.def;
        hpText.text = "체력 :" + Player.Instance.nowHp+ "/" + Player.Instance.maxHp;

        strText.text = "Str : " + Player.Instance.str;
        dexText.text = "Dex :" + Player.Instance.dex;
        conText.text = "Con :" + Player.Instance.con;

        statusPointText.text = "잔여 스탯 포인트 :" + Player.Instance.statusPoint;
    }

    public void UpStr()
    {
        Debug.Log("눌렸음");

        if (Player.Instance.statusPoint > 0)
        {
            Debug.Log("이프문들어옴");
            Player.Instance.str += 1;
            Player.Instance.atk += 2;
            Player.Instance.statusPoint -= 1;
            SetStatus();
        }
    }

    public void UpDex()
    {
        if (Player.Instance.statusPoint > 0)
        {
            Player.Instance.dex += 1;
            Player.Instance.def += 1;
            Player.Instance.statusPoint -= 1;
            SetStatus();
        }
    }

    public void UpCon()
    {
        if (Player.Instance.statusPoint > 0)
        {
            Player.Instance.con += 1;
            Player.Instance.maxHp += 5;
            Player.Instance.statusPoint -= 1;
            SetStatus();
        }
    }
}

