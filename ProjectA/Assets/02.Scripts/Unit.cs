using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public float nowHp;
    public float maxHp;
    public float nowMp;
    public float maxMp;
    public float atk;
    public float def;
    public float lv;

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
}
