using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour
{
    public Image itemImage = null;
    public Item item = null;
    public TextMeshProUGUI itmeAmountText;

    private void Awake()
    {
        itemImage = transform.GetChild(0).GetComponent<Image>();
    }

    public void SetItem(Item _item)
    {
        //아이템을 슬롯에 추가해주는함수

        item = Instantiate(_item, transform); //아이템을 슬롯에 복사함
        itemImage.gameObject.SetActive(true); //이미지를 켜줌
        itemImage.sprite = _item.itemSprite; //이미지를 아이템으로 교체해줌

        SetItemAmount();
    }

    public void SetItemAmount()
    {
        if (item.itemAmount > 1) //만약 수량이 1보다 많다면 실행
        {
            itmeAmountText.gameObject.SetActive(true); //아이템의 수량을 보이게하기
            itmeAmountText.text = item.itemAmount.ToString(); //아이템의 수량을 표시해줌
        }
    }

    public virtual void DeleteItem()
    {
        itemImage.sprite = null;
        itemImage.gameObject.SetActive(false); //이미지 안보이게하기
        itmeAmountText.gameObject.SetActive(false); //아이템 수량 안보이게하기
        item = null;

        if (!transform.GetComponentInChildren<Item>())
        {
            Debug.Log("슬롯에 아이템이 없는데 삭제하려 했습니다");
            return;
        }

        Destroy(transform.GetComponentInChildren<Item>().gameObject);
    }

    public virtual void OnClick()
    {
        GameManager.Instance.nowSelectSlot = this;
        Inventory.Instance.ItemExplanation(item, false);

        if (GameManager.Instance.isOpenBox)
        {
            if (GameManager.Instance.nowSelectSlot.item != null)
                return;

            if (GameManager.Instance.lastSelectSlot == GameManager.Instance.nowSelectSlot && Inventory.Instance.StoreButton.activeSelf)
            {
                Inventory.Instance.StoreButton.SetActive(false);
            }
            else
            {
                Inventory.Instance.StoreButton.SetActive(true);
            }
            Inventory.Instance.StoreButton.GetComponent<RectTransform>().position = transform.position;
        }
    }
}
