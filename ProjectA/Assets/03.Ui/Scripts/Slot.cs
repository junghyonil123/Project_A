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
        //�������� ���Կ� �߰����ִ��Լ�
        item = Instantiate(_item, transform);
        itemImage.gameObject.SetActive(true);
        itemImage.sprite = _item.ItemSprite;
    }

    public void OnClick()
    {
        Debug.Log("���Ǿ");
        Inventory.Instance.ItemExplanation(item);
    }
}
