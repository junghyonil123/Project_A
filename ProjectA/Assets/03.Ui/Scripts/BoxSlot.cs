using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSlot : Slot
{
    public override void OnClick()
    {
        GameManager.Instance.nowSelectSlot = this;
        //Inventory.Instance.ItemExplanation_InBox(item);
    }
}
