using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ItemEffect{

}

public class ItemManager : MonoBehaviour
{

    #region Singleton
    private static ItemManager instance;
    public static ItemManager Instance
    {
        get
        {
            if (instance == null) //instance �� ���������ʴ´ٸ�
            {
                var obj = FindObjectOfType<ItemManager>(); //Player Ÿ���� �����ϴ��� Ȯ��
                if (obj != null)
                {
                    instance = obj; //null�� �ƴ϶�� instance�� �־���
                }
                else
                {
                    var newPlayer = new GameObject("ItemManager").AddComponent<ItemManager>(); //null�̶�� ���θ������
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

        for (int i = 0; i < itemLibrary.Count; i++)
        {
            if (itemLibrary[i].itemNumber != i)
            {
                Debug.Log("�����۳ѹ� ����ġ");
            }
        }
    }
    #endregion

    public List<Item> itemLibrary = new List<Item>();

    #region WeaponEffectList

    public int WeaponEffect_0(ref int atk)
    {
        Debug.Log("WeaponEffect_1 �ߵ�");
        return atk += 2;
    }

    public int WeaponEffect_1(ref int atk)
    {
        Debug.Log("SkillEffect_2 �ߵ�");
        return atk + 3;
    }

    #endregion
}
