using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{

    #region Singleton
    private static Inventory instance;
    public static Inventory Instance
    {
        get
        {
            if (instance == null) //instance �� ���������ʴ´ٸ�
            {
                var obj = FindObjectOfType<Inventory>(); //Player Ÿ���� �����ϴ��� Ȯ��
                if (obj != null)
                {
                    instance = obj; //null�� �ƴ϶�� instance�� �־���
                }
                else
                {
                    var newPlayer = new GameObject("InventoryUi").AddComponent<Inventory>(); //null�̶�� ���θ������
                    instance = newPlayer;
                }
            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }

    private void Awake()
    {
        ////������ ���ÿ� ����Ǵ� Awake�� �̹� �����Ǿ��ִ� �̱��� ������Ʈ�� �ִ��� �˻��ϰ� �ִٸ� ���� ������ ������Ʈ�� �ı�

        var objs = FindObjectsOfType<Inventory>();
        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }

    }
    #endregion

    public List<GameObject> inventoryList = new List<GameObject>();

    public GameObject equipmentWindow;
    public GameObject explanationWindow;

    public GameObject dropItemButton;
    public GameObject equipItemButton;
    public GameObject unEpuipItemButton;
    public GameObject consumeItemButton;

    public Image itemImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI typeText;
    public TextMeshProUGUI abilityText;
    public TextMeshProUGUI explanationText;
    
    public GameObject StoreButton;
    
    public Slot GetItem(Item item)
    {
        Debug.Log("����");
        foreach (GameObject slot in inventoryList)
        {
            if (slot.GetComponent<Slot>().item == null)
            {
                slot.GetComponent<Slot>().SetItem(item);
                return slot.GetComponent<Slot>();
            }
            else if (item.itmeType == ItemType.ConsumableItme || item.itmeType == ItemType.Material)
            {
                if (slot.GetComponent<Slot>().item.itemNumber == item.itemNumber)// ���� ���Ȥ�� �Һ�������϶� ���� �������� �ִٸ� ��ħ
                {
                    slot.GetComponent<Slot>().item.itemAmount += item.itemAmount;
                    slot.GetComponent<Slot>().SetItemAmount();
                    return slot.GetComponent<Slot>();
                }
            }
        }

        return null;
    }

    public void DropItem()
    {
        GameManager.Instance.nowSelectSlot.DeleteItem();
        explanationWindow.SetActive(false);
    }

    public string ReturnItemType(ItemType itemtype)
    {
        switch (itemtype)
        {
            case ItemType.Weapon:
                return "Weapon";
            case ItemType.Head:
                return "Head";
            case ItemType.SubWeapon:
                return "SubWeapon";
            case ItemType.Armo:
                return "Armo";
            case ItemType.Shoes:
                return "Shoes";
            case ItemType.ConsumableItme:
                return "ConsumableItme";
            case ItemType.Material:
                return "Material";
            default:
                return "null";
        }
    }

    public void ItemExplanation(Item item, bool isEquipped)
    {
        if (item == null)
        {
            return;
        }
        if (isEquipped)
        {
            dropItemButton.SetActive(false);
            equipItemButton.SetActive(false);
            consumeItemButton.SetActive(true);
            unEpuipItemButton.SetActive(false);
        }
        else if (item.itmeType == ItemType.ConsumableItme)
        {
            consumeItemButton.SetActive(true);
            dropItemButton.SetActive(true);

            unEpuipItemButton.SetActive(false);
            equipItemButton.SetActive(false);
        }
        else
        {
            dropItemButton.SetActive(true);
            equipItemButton.SetActive(true);
            unEpuipItemButton.SetActive(false);
            unEpuipItemButton.SetActive(false);
        }

        explanationWindow.SetActive(!explanationWindow.activeSelf);
        itemImage.sprite = item.itemSprite;
        nameText.text = item.itmeName;
        typeText.text = ReturnItemType(item.itmeType);
        abilityText.text = "���ݷ�: " + item.itemStatus.itemAtk;
        explanationText.text = item.itmeExplanation;
    }

    //public void ItemExplanation_InBox(Item item)
    //{
    //    if (item == null)
    //    {
    //        return;
    //    }
    //    if (isEquipped)
    //    {
    //        dropItemButton.SetActive(false);
    //        equipItemButton.SetActive(false);
    //        consumeItemButton.SetActive(true);
    //        unEpuipItemButton.SetActive(false);
    //    }
    //    else if (item.itmeType == ItemType.ConsumableItme)
    //    {
    //        consumeItemButton.SetActive(true);
    //        dropItemButton.SetActive(true);

    //        unEpuipItemButton.SetActive(false);
    //        equipItemButton.SetActive(false);
    //    }
    //    else
    //    {
    //        dropItemButton.SetActive(true);
    //        equipItemButton.SetActive(true);
    //        unEpuipItemButton.SetActive(false);
    //        unEpuipItemButton.SetActive(false);
    //    }

    //    explanationWindow.SetActive(!explanationWindow.activeSelf);
    //    itemImage.sprite = item.itemSprite;
    //    nameText.text = item.itmeName;
    //    typeText.text = ReturnItemType(item.itmeType);
    //    abilityText.text = "���ݷ�: " + item.itemStatus.itemAtk;
    //    explanationText.text = item.itmeExplanation;
    //}

    public void SkillExplanation(Skill skill)
    {
        if (skill == null)
        {
            return;
        }

        dropItemButton.SetActive(false);
        equipItemButton.SetActive(false);
        unEpuipItemButton.SetActive(false);

        explanationWindow.SetActive(!explanationWindow.activeSelf);
        itemImage.sprite = skill.skillSprite;
        nameText.text = skill.skillName;
        abilityText.text = "";
        typeText.text = "";
        explanationText.text = skill.skillExplanation;
    }

    public Slot equipSlot;
    public Slot unEquipSlot;

    public void EquipItem()
    {
        //������ ����
        equipSlot = EquipmentWindow.Instance.EquipItem(GameManager.Instance.nowSelectSlot.item); //�������� ��������
        ItemExplanation(GameManager.Instance.nowSelectSlot.item, true); //������ ����
        GameManager.Instance.nowSelectSlot.DeleteItem(); //������ġ�� �������� ����
        GameManager.Instance.nowSelectSlot = equipSlot; //������ġ�� �����Ѱ����� ����
    }

    public void UnEquipItem()
    {
        GameManager.Instance.nowSelectSlot = GetItem(GameManager.Instance.nowSelectSlot.item); //�������� ����
        EquipmentWindow.Instance.UnEquipItem(GameManager.Instance.nowSelectSlot.item.itmeType);
        ItemExplanation(GameManager.Instance.nowSelectSlot.item, false); //������ ���� 
    }

    public void ConsumeItem()
    {
        GameManager.Instance.nowSelectSlot.item.Consume();

        if (GameManager.Instance.nowSelectSlot.item.itemAmount > 1)
        {
            GameManager.Instance.nowSelectSlot.item.itemAmount -= 1;
            GameManager.Instance.nowSelectSlot.SetItemAmount();
        }
        else
        {
            GameManager.Instance.nowSelectSlot.DeleteItem();
        }

        explanationWindow.SetActive(false);
    }
}