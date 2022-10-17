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
        item.AddStatus();
        gameObject.SetActive(false);
        return mainWeaponSlot;
    }

}
