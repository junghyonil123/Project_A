using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Unit : MonoBehaviour
{
    public string unitName;
    public float nowHp;
    public float maxHp;
    public float nowMp;
    public float maxMp;
    public float atk;
    public float def;
    public int lv;

    public bool isDie = false;
    virtual public void Start()
    {
        nowHp = maxHp;
        nowMp = maxMp;
    }

    virtual public void GetDamage(float damage)
    {
        if(damage - def <= 1)
        {
            nowHp--;
        }
        else
        {
          nowHp = nowHp - (damage - def);
        }
    }

    virtual public void Die()
    {
        if(nowHp <= 0)
        {
            Time.timeScale = 0;
            gameObject.SetActive(false);
            isDie = true;
            BattleManager.Instance.finishCanvas.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
