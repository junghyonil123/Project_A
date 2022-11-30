using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public enum ItemType
{
    Weapon,
    Head,
    Glove,
    Armo,
    ConsumableItme,
    Material
}

[Serializable]
public struct ItemStatus
{
    public int itemAtk;
    public int itemDef;
    public int itemHp;
    public int itemStr;
    public int itemDex;
    public int itemCon;
}

public class Item : MonoBehaviour
{
    public string itmeName;
    public Sprite itemSprite;
    public ItemType itmeType;
    public int itemNumber;

    public ItemStatus itemStatus;
    public string itmeExplanation;
     
    

    public void AddStatus()
    {
        //플레이어의 공격력만 더해줌
        Player.Instance.atk += itemStatus.itemAtk;
        Player.Instance.def += itemStatus.itemDef;
        Player.Instance.maxHp += itemStatus.itemHp;
        Player.Instance.str += itemStatus.itemStr;
        Player.Instance.def += itemStatus.itemDef;
        Player.Instance.con += itemStatus.itemCon;
    }

    public void SubtractStatus()
    {
        Player.Instance.atk -= itemStatus.itemAtk;
        Player.Instance.def -= itemStatus.itemDef;
        Player.Instance.maxHp -= itemStatus.itemHp;
        Player.Instance.str -= itemStatus.itemStr;
        Player.Instance.def -= itemStatus.itemDef;
        Player.Instance.con -= itemStatus.itemCon;
    }
}

