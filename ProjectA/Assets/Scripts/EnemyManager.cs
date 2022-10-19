using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem
{
    public List<Item> dropItem = new List<Item>();
    public List<float> dropPer = new List<float>();
    public List<int> dropMin = new List<int>();
    public List<int> dropMax = new List<int>();

    public DropItem(List<Item> _dropitem, List<float> _dropPer, List<int> _dropMin, List<int> _dropMax)
    {
        dropItem = _dropitem;
        dropPer = _dropPer;
        dropMax = _dropMax;
        dropMin = _dropMin;
    }
}


public class EnemyManager : MonoBehaviour { 

    public List<DropItem> dropitem = new List<DropItem>();

    public List<Item> monster1DropItem = new List<Item>();
    public List<float> monster1DropPer = new List<float>();
    public List<int> monster1DropMin = new List<int>();
    public List<int> monster1DropMax = new List<int>();
    
    public List<Item> monster2DropItem = new List<Item>();
    public List<float> monster2DropPer = new List<float>();
    public List<int> monster2DropMin = new List<int>();
    public List<int> monster2DropMax = new List<int>();


    #region singleton
    private static EnemyManager instance = null;

    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public static EnemyManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    #endregion


    private void Start()
    {
        dropitem.Add(new DropItem(monster1DropItem, monster1DropPer, monster1DropMax, monster1DropMin));
    }



}
