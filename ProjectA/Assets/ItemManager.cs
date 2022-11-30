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
            if (instance == null) //instance 가 존재하지않는다면
            {
                var obj = FindObjectOfType<ItemManager>(); //Player 타입이 존재하는지 확인
                if (obj != null)
                {
                    instance = obj; //null이 아니라면 instance에 넣어줌
                }
                else
                {
                    var newPlayer = new GameObject("ItemManager").AddComponent<ItemManager>(); //null이라면 새로만들어줌
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

        for (int i = 0; i < itemLibrary.Count; i++)
        {
            if (itemLibrary[i].itemNumber != i)
            {
                Debug.Log("아이템넘버 불일치");
            }
        }
    }
    #endregion

    public List<Item> itemLibrary = new List<Item>();

    #region WeaponEffectList

    public int WeaponEffect_0(ref int atk)
    {
        Debug.Log("WeaponEffect_1 발동");
        return atk += 2;
    }

    public int WeaponEffect_1(ref int atk)
    {
        Debug.Log("SkillEffect_2 발동");
        return atk + 3;
    }

    #endregion
}
