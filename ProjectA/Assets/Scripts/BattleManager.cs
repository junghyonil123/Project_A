using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public GameObject enemy;
    public GameObject player;
    public GameObject finishCanvas;

    public List<Slot> dropItemSlot = new List<Slot>();

    public void monsterItemDrop(Item item)
    {
        foreach (Slot slot in dropItemSlot)
        {
            if (slot.item == null)
            {
                slot.SetItem(item);
                break;
            }
        }
    }

    public void FinishCanvasOn()
    {
        finishCanvas.SetActive(true);
    }

    public void FinishCanvasOff()
    {
        finishCanvas.SetActive(false);
    }

    #region singleton
    private static BattleManager instance = null;

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
    public static BattleManager Instance
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
}
