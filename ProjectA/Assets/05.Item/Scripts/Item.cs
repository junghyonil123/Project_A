using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public enum ItemType
{
    Weapon,
    Head,
    SubWeapon,
    Armo,
    Shoes,
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

    public int itemAmount;

    public ItemStatus itemStatus;
    public string itmeExplanation;
     
    public void EquiptItem()
    {
        //플레이어의 공격력만 더해줌
        Player.Instance.atk += itemStatus.itemAtk;
        Player.Instance.def += itemStatus.itemDef;
        Player.Instance.maxHp += itemStatus.itemHp;
        Player.Instance.str += itemStatus.itemStr;
        Player.Instance.def += itemStatus.itemDef;
        Player.Instance.con += itemStatus.itemCon;
        StatusCanvas.Instance.SetStatus();
    }

    public void UnEquiptItem()
    {
        Player.Instance.atk -= itemStatus.itemAtk;
        Player.Instance.def -= itemStatus.itemDef;
        Player.Instance.maxHp -= itemStatus.itemHp;
        Player.Instance.str -= itemStatus.itemStr;
        Player.Instance.def -= itemStatus.itemDef;
        Player.Instance.con -= itemStatus.itemCon;
        StatusCanvas.Instance.SetStatus();
    }

    public virtual void Consume(){}
}

