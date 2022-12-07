using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

[Serializable]
public struct DropItemInfo 
{
    public Item dropItem;
    public float dropPer;
    public int dropAmountMin;
    public int dropAmountMax;
}


public class Enemy : Unit
{
    [HideInInspector] public SpriteRenderer spriteRenderer;

    public int dropExp;
    public int dropGold;
    public int number;

    public List<DropItemInfo> dropItemInfo = new List<DropItemInfo>();

    private void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void Drop()
    {

        foreach (var item in dropItemInfo)
        {
            if (item.dropPer >= UnityEngine.Random.Range(1, 101))
            {
                item.dropItem.itemAmount = UnityEngine.Random.Range(item.dropAmountMin, item.dropAmountMax + 1);
                BattleManager.Instance.ShowDropItemToFinishCanvas(item.dropItem);
            }
        }

        Player.Instance.GetGold(dropGold);
        Player.Instance.GetExp(dropExp);

    }

    public override void Die()
    {
        Debug.Log("적이 죽었습니다");
        Drop(); //아이템드롭
        Destroy(this.gameObject); //삭제
    }
}