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
            if (instance == null) //instance 가 존재하지않는다면
            {
                var obj = FindObjectOfType<Inventory>(); //Player 타입이 존재하는지 확인
                if (obj != null)
                {
                    instance = obj; //null이 아니라면 instance에 넣어줌
                }
                else
                {
                    var newPlayer = new GameObject("InventoryUi").AddComponent<Inventory>(); //null이라면 새로만들어줌
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
        ////생성과 동시에 실행되는 Awake는 이미 생성되어있는 싱글톤 오브젝트가 있는지 검사하고 있다면 지금 생성된 오브젝트를 파괴

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
    
    public Slot nowSelectSlot;
     
    public Slot GetItem(Item item)
    {
        foreach (GameObject slot in inventoryList)
        {
            if (slot.GetComponent<Slot>().item == null)
            {
                slot.GetComponent<Slot>().SetItem(item);
                return slot.GetComponent<Slot>();
            }
        }

        return null;
    }

    public void DropItem()
    {
        nowSelectSlot.DeleteItem();
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
        abilityText.text = "공격력: " + item.itemStatus.itemAtk;
        explanationText.text = item.itmeExplanation;
    }

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
        //아이템 장착
        equipSlot = EquipmentWindow.Instance.EquipItem(nowSelectSlot.item); //아이템을 장착해줌
        ItemExplanation(nowSelectSlot.item, true); //아이템 설명
        nowSelectSlot.DeleteItem(); //현재위치의 아이템을 지움
        nowSelectSlot = equipSlot; //현재위치를 장착한곳으로 변경
    }

    public void UnEquipItem()
    {
        nowSelectSlot = GetItem(nowSelectSlot.item); //아이템을 얻음
        EquipmentWindow.Instance.UnEquipItem(nowSelectSlot.item.itmeType);
        ItemExplanation(nowSelectSlot.item, false); //아이템 설명 
    }

    public void ConsumeItem()
    {
        nowSelectSlot.item.Consume();
        nowSelectSlot.DeleteItem();
        explanationWindow.SetActive(false);
    }
}