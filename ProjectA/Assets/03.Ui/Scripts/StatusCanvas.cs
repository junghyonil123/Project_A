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

    public void OnOffStatusWindow()
    {
        profileWindow.SetActive(!profileWindow.activeSelf);
        SetStatusWindow();
    }

    public void SetStatusWindow()
    {
        Player.Instance.SetStatus(); //플레이어의 스탯수치를 계산해줌

        jobText.text = Player.Instance.job;
        atkText.text = "공격력 :"+Player.Instance.atk;
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

