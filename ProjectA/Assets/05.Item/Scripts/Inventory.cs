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

        //DontDestroyOnLoad(gameObject); //���� ��ȯ�Ҷ� �ı��Ǵ°��� ����
    }
    #endregion

    public List<GameObject> inventory = new List<GameObject>();

    public GameObject explanationWindow;
    public Image itemImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI typeText;
    public TextMeshProUGUI abilityText;
    public TextMeshProUGUI explanationText;

    public Slot nowSelectSlot;

    public void GetItem(Item item)
    {
        foreach (GameObject slot in inventory)
        {
            if (slot.GetComponent<Slot>().item == null)
            {
                slot.GetComponent<Slot>().SetItem(item);
                break;
            }
        }
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

    public void ItemExplanation(Item item)
    {
        if (item == null)
        {
            return;
        }
        explanationWindow.SetActive(!explanationWindow.activeSelf);
        itemImage.sprite = item.ItemSprite;
        nameText.text = item.ItmeName;
        typeText.text = ReturnItemType(item.itmeType);
        abilityText.text = "���ݷ�: "+item.itemStatus;
        explanationText.text = item.itmeExplanation;
    }

    public void EquipItem()
    {
        EquipmentWindow.Instance.EquipItem(nowSelectSlot.item);
    }
}