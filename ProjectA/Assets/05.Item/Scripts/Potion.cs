using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : Item
{
    [SerializeField]
    int recoveryAmount;

    public override void Consume()
    {
        if (Player.Instance.nowHp + recoveryAmount >= Player.Instance.maxHp)
        {
            Player.Instance.nowHp = Player.Instance.maxHp;
        }
        else
        {
            Player.Instance.nowHp += recoveryAmount;
        }
    }
}
