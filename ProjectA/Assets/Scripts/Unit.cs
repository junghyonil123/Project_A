using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Unit : MonoBehaviour
{
    public string unitName;
    public float nowHp;
    public float maxHp;
    public float atk;
    public float def;
    public int lv;

    virtual public void Start()
    {
        nowHp = maxHp;
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

        if(nowHp < 0)
        {
            nowHp = 0;
        }
    }

    virtual public void Die()
    {
        if(nowHp <= 0)
        {
            Time.timeScale = 0;
        }
    }
}
