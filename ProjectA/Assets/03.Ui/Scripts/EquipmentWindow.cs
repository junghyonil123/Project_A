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
            if (instance == null) //instance �� ���������ʴ´ٸ�
            {
                var obj = FindObjectOfType<EquipmentWindow>(); //Player Ÿ���� �����ϴ��� Ȯ��
                if (obj != null)
                {
                    instance = obj; //null�� �ƴ϶�� instance�� �־���
                }
                else
                {
                    var newPlayer = new GameObject("EquipmentWindow").AddComponent<EquipmentWindow>(); //null�̶�� ���θ������
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

    public List<GameObject> equipmentSlotList = new List<GameObject>();

    public EquipSlot headSlot;
    public EquipSlot mainWeaponSlot;
    public EquipSlot ArmoSlot;
    public EquipSlot SubWeaponSlot;
    public EquipSlot ShoesSlot;

    public EquipSlot EquipItem(Item item)
    {
        //�������� ��������
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
