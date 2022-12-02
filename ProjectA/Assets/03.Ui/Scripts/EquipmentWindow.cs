using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquipmentWindow : MonoBehaviour
{
    #region Singleton
    private static EquipmentWindow instance;
    public static EquipmentWindow Instance
    {
        get
        {
            if (instance == null) //instance 가 존재하지않는다면
            {
                var obj = FindObjectOfType<EquipmentWindow>(); //Player 타입이 존재하는지 확인
                if (obj != null)
                {
                    instance = obj; //null이 아니라면 instance에 넣어줌
                }
                else
                {
                    var newPlayer = new GameObject("EquipmentWindow").AddComponent<EquipmentWindow>(); //null이라면 새로만들어줌
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

    public List<GameObject> equipmentSlotList = new List<GameObject>();

    public EquipSlot headSlot;
    public EquipSlot mainWeaponSlot;
    public EquipSlot ArmoSlot;
    public EquipSlot SubWeaponSlot;
    public EquipSlot ShoesSlot;

    public EquipSlot EquipItem(Item item)
    {
        //아이템을 장착해줌
        mainWeaponSlot.SetItem(item);
        mainWeaponSlot.itemImage.color = new Color(255, 255, 255, 255);
        mainWeaponSlot.isEquipped = true;
        item.EquiptItem();
        return mainWeaponSlot;
    }

    public void UnEquipItem(ItemType itemType)
    {

        switch (itemType)
        {
            case ItemType.Weapon:
                mainWeaponSlot.item.UnEquiptItem();
                mainWeaponSlot.DeleteItem();
                break;
            case ItemType.Head:
                SubWeaponSlot.item.UnEquiptItem();
                SubWeaponSlot.DeleteItem();
                break;
            case ItemType.SubWeapon:
                mainWeaponSlot.item.UnEquiptItem();
                mainWeaponSlot.DeleteItem();
                break;
            case ItemType.Armo:
                ArmoSlot.item.UnEquiptItem();
                ArmoSlot.DeleteItem();
                break;
            case ItemType.Shoes:
                ShoesSlot.item.UnEquiptItem();
                ShoesSlot.DeleteItem();
                break;
            default:
                break;
        }

    }

}
