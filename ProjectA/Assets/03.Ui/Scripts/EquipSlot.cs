using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipSlot : Slot
{
    public bool isEquipped = false;
    public Sprite defaultSprite;

    public override void DeleteItem()
    {
        Debug.Log(itemImage);
        itemImage.sprite = defaultSprite;
        itemImage.color = new Color(255 / 225f, 255 / 225f, 255 / 225f, 38 / 225f);

        item = null;
    }

    public override void OnClick()
    {
        Inventory.Instance.nowSelectSlot = this;
        Inventory.Instance.ItemExplanation(item, isEquipped);
    }
}
