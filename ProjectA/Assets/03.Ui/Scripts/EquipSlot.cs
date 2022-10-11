using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipSlot : Slot
{
    public Sprite defaultSprite;
    public bool isEquipped = false;

    public override void DeleteItem()
    {
        itemImage = null;
        item = null;
        base.itemImage.sprite = defaultSprite;
        base.itemImage.color = new Color(255, 255, 255, 38);
    }

    public override void OnClick()
    {
        Inventory.Instance.nowSelectSlot = this;

        if (isEquipped)
        {
            Inventory.Instance.EquippedItemExplanation(item);
        }
        else
        {
            Inventory.Instance.ItemExplanation(item);
        }
    }
}
