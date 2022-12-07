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
        //�������� ���Կ� �߰����ִ��Լ�

        item = Instantiate(_item, transform); //�������� ���Կ� ������
        itemImage.gameObject.SetActive(true); //�̹����� ����
        itemImage.sprite = _item.itemSprite; //�̹����� ���������� ��ü����

        SetItemAmount();
    }

    public void SetItemAmount()
    {
        if (item.itemAmount > 1) //���� ������ 1���� ���ٸ� ����
        {
            itmeAmountText.gameObject.SetActive(true); //�������� ������ ���̰��ϱ�
            itmeAmountText.text = item.itemAmount.ToString(); //�������� ������ ǥ������
        }
    }

    public virtual void DeleteItem()
    {
        itemImage.sprite = null;
        itemImage.gameObject.SetActive(false); //�̹��� �Ⱥ��̰��ϱ�
        itmeAmountText.gameObject.SetActive(false); //������ ���� �Ⱥ��̰��ϱ�
        item = null;

        if (!transform.GetComponentInChildren<Item>())
        {
            Debug.Log("���Կ� �������� ���µ� �����Ϸ� �߽��ϴ�");
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
