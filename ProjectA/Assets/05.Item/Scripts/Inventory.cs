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

        //var objs = FindObjectsOfType<Player>();
        //if (objs.Length != 1)
        //{
        //    Destroy(gameObject);
        //    return;
        //}

        //DontDestroyOnLoad(gameObject); //씬을 전환할때 파괴되는것을 막음
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
        abilityText.text = "공격력: "+item.itemStatus;
        explanationText.text = item.itmeExplanation;
    }

    public void EquipItem()
    {
        EquipmentWindow.Instance.EquipItem(nowSelectSlot.item);
    }
}