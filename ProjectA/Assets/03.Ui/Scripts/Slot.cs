using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField]
    private Image itemImage = null;
    public Item item = null;


    public void SetItem(Item _item)
    {
        //아이템을 슬롯에 추가해주는함수
        item = Instantiate(_item, transform);
        itemImage.gameObject.SetActive(true);
        itemImage.sprite = _item.ItemSprite;
    }

    public void OnClick()
    {
        Debug.Log("눌렷어염");
        Inventory.Instance.ItemExplanation(item);
    }
}
