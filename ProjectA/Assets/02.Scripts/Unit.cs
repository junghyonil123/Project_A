using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public float hp;
    public float maxHp;
    public float mp;
    public float maxMp;
    public float atk;
    public float def;

    virtual public void Start()
    {
        hp = maxHp;
        mp = maxMp;
    }

    virtual public void GetDamage(float damage)
    {
        if(damage - def <= 1)
        {
            hp--;
        }
        else
        {
          hp = hp - (damage - def);
        }
    }
}
