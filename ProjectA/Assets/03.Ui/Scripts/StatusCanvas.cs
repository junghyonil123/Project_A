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
            if (instance == null) //instance �� ���������ʴ´ٸ�
            {
                var obj = FindObjectOfType<StatusCanvas>(); //Player Ÿ���� �����ϴ��� Ȯ��
                if (obj != null)
                {
                    instance = obj; //null�� �ƴ϶�� instance�� �־���
                }
                else
                {
                    var newBattleManager = new GameObject("StatusCanvas").AddComponent<StatusCanvas>(); //null�̶�� ���θ������
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
            //�����ִٸ� ����
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
         //�÷��̾��� ���ȼ�ġ�� ��������

        jobText.text = Player.Instance.job;
        atkText.text = "���ݷ� :" +Player.Instance.atk;
        defText.text = "���� :" + Player.Instance.def;
        hpText.text = "ü�� :" + Player.Instance.nowHp+ "/" + Player.Instance.maxHp;

        strText.text = "Str : " + Player.Instance.str;
        dexText.text = "Dex :" + Player.Instance.dex;
        conText.text = "Con :" + Player.Instance.con;

        statusPointText.text = "�ܿ� ���� ����Ʈ :" + Player.Instance.statusPoint;
    }

    public void UpStr()
    {
        Debug.Log("������");

        if (Player.Instance.statusPoint > 0)
        {
            Debug.Log("����������");
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

