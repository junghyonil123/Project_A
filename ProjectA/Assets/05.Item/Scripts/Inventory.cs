using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void ExplanationWindow()
    {

    }
}