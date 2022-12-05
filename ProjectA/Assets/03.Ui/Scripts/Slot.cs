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

    public void SetItem(Item _item)
    {
        //�������� ���Կ� �߰����ִ��Լ�
        item = Instantiate(_item, transform);
        itemImage.gameObject.SetActive(true);
        itemImage.sprite = _item.itemSprite;
    }

    public virtual void DeleteItem()
    {
        itemImage.sprite = null;
        itemImage.gameObject.SetActive(false);
        item = null;
        Destroy(this.transform.GetChild(1).gameObject);
    }

    public virtual void OnClick()
    {
        Inventory.Instance.nowSelectSlot = this;
        Inventory.Instance.ItemExplanation(item, false);
    }
}
