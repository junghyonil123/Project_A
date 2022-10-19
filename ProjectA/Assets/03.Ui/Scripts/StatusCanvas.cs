using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatusCanvas : MonoBehaviour
{
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

    public bool isOpenCanvas = false;


    public void WindowOnOff(string name)
    {
        for (int i = 0; i < uiWindowList.Count; i++)
        {
            if (uiWindowList[i].name == name)
            {
                if (uiWindowList[i].GetComponent<RectTransform>().position.x < 0)
                {
                    uiWindowList[i].GetComponent<RectTransform>().Translate(new Vector3(+350, 0, 0));
                }
            }
            else
            {
                if (uiWindowList[i].GetComponent<RectTransform>().position.x > 0)
                {
                    uiWindowList[i].GetComponent<RectTransform>().Translate(new Vector3(-350, 0, 0));
                }
            }
        }
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
        Debug.Log(statusWindow.GetComponent<RectTransform>().position.x);

        if (statusWindow.GetComponent<RectTransform>().position.x == 73.5)
        {
            //�����ִٸ�
            statusWindow.GetComponent<RectTransform>().Translate(new Vector3(-400, 0, 0));
        }
        else
        {
            statusWindow.GetComponent<RectTransform>().Translate(new Vector3(400, 0, 0));
        }
        SetStatusWindow();
    }

    public void SetStatusWindow()
    {
        Player.Instance.SetStatus(); //�÷��̾��� ���ȼ�ġ�� �������

        jobText.text = Player.Instance.job;
        atkText.text = "���ݷ� :"+Player.Instance.atk;
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
            Player.Instance.statusPoint -= 1;
            SetStatusWindow();
        }
    }

    public void UpDex()
    {
        if (Player.Instance.statusPoint > 0)
        {
            Player.Instance.dex += 1;
            Player.Instance.statusPoint -= 1;
            SetStatusWindow();
        }
    }

    public void UpCon()
    {
        if (Player.Instance.statusPoint > 0)
        {
            Player.Instance.con += 1;
            Player.Instance.statusPoint -= 1;
            SetStatusWindow();
        }
    }
}

