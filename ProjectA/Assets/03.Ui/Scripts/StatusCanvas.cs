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
                Debug.Log(uiWindowList[i].name + "µé¿È");
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
            Debug.Log(uiWindowList[i].name);
            if (uiWindowList[i].name == "EquipmentWindow")
            {
                Debug.Log(uiWindowList[i].name + "µé¿È");
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
        Player.Instance.SetStatus(); //ÇÃ·¹ÀÌ¾îÀÇ ½ºÅÈ¼öÄ¡¸¦ °è»êÇØÁÜ

        jobText.text = Player.Instance.job;
        atkText.text = "°ø°Ý·Â :"+Player.Instance.atk;
        defText.text = "¹æ¾î·Â :" + Player.Instance.def;
        hpText.text = "Ã¼·Â :" + Player.Instance.nowHp+ "/" + Player.Instance.maxHp;

        strText.text = "Str : " + Player.Instance.str;
        dexText.text = "Dex :" + Player.Instance.dex;
        conText.text = "Con :" + Player.Instance.con;

        statusPointText.text = "ÀÜ¿© ½ºÅÈ Æ÷ÀÎÆ® :" + Player.Instance.statusPoint;
    }

    public void UpStr()
    {
        Debug.Log("´­·ÈÀ½");

        if (Player.Instance.statusPoint > 0)
        {
            Debug.Log("ÀÌÇÁ¹®µé¾î¿È");
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

