using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatusCanvas : MonoBehaviour
{
    public GameObject profileWindow;

    public TextMeshProUGUI jobText;
    public TextMeshProUGUI atkText;
    public TextMeshProUGUI defText;
    public TextMeshProUGUI hpText;
    
    public TextMeshProUGUI strText;
    public TextMeshProUGUI dexText;
    public TextMeshProUGUI conText;

    public TextMeshProUGUI statusPointText;

    public List<GameObject> uiWindowList = new List<GameObject>();

    public void ProfilWindowOnOff()
    {
        for (int i = 0; i < uiWindowList.Count; i++)
        {
            if (uiWindowList[i].name == "ProfileWindow")
            {
                uiWindowList[i].SetActive(true);
            }
            else
            {
                uiWindowList[i].SetActive(false);
            }
        }
    }

    public void EquipmentWindowOnOff()
    {
        for (int i = 0; i < uiWindowList.Count; i++)
        {
            if (uiWindowList[i].name == "EquipmentWindow")
            {
                uiWindowList[i].SetActive(true);
            }
            else
            {
                uiWindowList[i].SetActive(false);
            }
        }
    }

    public void InventoryWindowOnOff()
    {
        for (int i = 0; i < uiWindowList.Count; i++)
        {
            if (uiWindowList[i].name == "InventoryWindow")
            {
                uiWindowList[i].SetActive(true);
            }
            else
            {
                uiWindowList[i].SetActive(false);
            }
        }
    }

    public void OnOffStatusWindow()
    {
        profileWindow.SetActive(!profileWindow.activeSelf);
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

