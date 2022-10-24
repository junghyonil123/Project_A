using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    Weapon,
    Head,
    Glove,
    Armo,
    ConsumableItme,
    Material
}

public class Item : MonoBehaviour
{
    public string itmeName;
    public Sprite itemSprite;
    public ItemType itmeType;
    public int itemNumber;

    public int itemStatus;
    public string itmeExplanation;

    private void Awake()
    {
        itemSprite = GetComponent<SpriteRenderer>().sprite;
    }

    public void AddStatus()
    {
        //�÷��̾��� ���ݷ¸� ������
        Player.Instance.atk += itemStatus;
    }
}

