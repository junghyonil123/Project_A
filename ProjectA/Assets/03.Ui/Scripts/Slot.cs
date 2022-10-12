using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Image itemImage = null;
    public Item item = null;

    private void Awake()
    {
        itemImage = transform.GetChild(0).GetComponent<Image>();
    }

    public void EquipItem(Item _item)
    {
        //아이템을 슬롯에 추가해주는함수
        item = Instantiate(_item, transform);
        itemImage.gameObject.SetActive(true);
        itemImage.sprite = _item.ItemSprite;
    }

    public virtual void DeleteItem()
    {
        Debug.Log("지워졌습니다");
        itemImage.sprite = null;
        itemImage.gameObject.SetActive(false);
        item = null;
        Destroy(this.transform.GetChild(1).gameObject);
    }

    public virtual void OnClick()
    {
        Inventory.Instance.nowSelectSlot = this;
        Inventory.Instance.ItemExplanation(item);
    }
}
