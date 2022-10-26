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

        //var objs = FindObjectsOfType<Player>();
        //if (objs.Length != 1)
        //{
        //    Destroy(gameObject);
        //    return;
        //}

    }
    #endregion

    public List<GameObject> inventoryList = new List<GameObject>();

    public GameObject equipmentWindow;
    public GameObject explanationWindow;
    public GameObject dropItemButton;
    public GameObject equipItemButton;
    public GameObject unEpuipItemButton;
    public GameObject explanationCanvas;

    public Image itemImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI typeText;
    public TextMeshProUGUI abilityText;
    public TextMeshProUGUI explanationText;

    public Slot nowSelectSlot;

    public void GetItem(Item item)
    {
        foreach (GameObject slot in inventoryList)
        {
            if (slot.GetComponent<Slot>().item == null)
            {
                slot.GetComponent<Slot>().SetItem(item);
                break;
            }
        }
    }

    public void DropItem()
    {
        nowSelectSlot.DeleteItem();
        explanationCanvas.SetActive(false);
    }

    public string ReturnItemType(ItemType itemtype)
    {
        switch (itemtype)
        {
            case ItemType.Weapon:
                return "Weapon";
            case ItemType.Head:
                return "Head";
            case ItemType.Glove:
                return "Glove";
            case ItemType.Armo:
                return "Armo";
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
            unEpuipItemButton.SetActive(true);
        }
        else
        {
            dropItemButton.SetActive(true);
            equipItemButton.SetActive(true);
            unEpuipItemButton.SetActive(false);
        }

        explanationWindow.SetActive(!explanationWindow.activeSelf);
        itemImage.sprite = item.itemSprite;
        nameText.text = item.itmeName;
        typeText.text = ReturnItemType(item.itmeType);
        abilityText.text = "���ݷ�: " + item.itemStatus;
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
        //������ ����
        equipmentWindow.SetActive(true);
        equipSlot = EquipmentWindow.Instance.EquipItem(nowSelectSlot.item);
        ItemExplanation(nowSelectSlot.item , true);
        explanationWindow.SetActive(true);
        nowSelectSlot.DeleteItem();
        nowSelectSlot = equipSlot;
        explanationCanvas.SetActive(false);
    }

    public void UnEquipItem()
    {
        GetItem(nowSelectSlot.item);
        ItemExplanation(nowSelectSlot.item, false);
        explanationWindow.SetActive(true);
        nowSelectSlot.DeleteItem();
        nowSelectSlot = unEquipSlot;
    }
}