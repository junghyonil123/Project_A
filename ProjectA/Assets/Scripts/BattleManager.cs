using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleManager : MonoBehaviour
{
    public GameObject enemy;
    public GameObject player;
    public GameObject finishCanvas;
    public GameObject playerBattlePos;
    public GameObject enemyBattlePos;
    public GameObject battleCanvas;

    public TextMeshProUGUI textMeshpro;

    public List<Slot> dropItemSlot = new List<Slot>();

    public bool isBattle = false;

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
        ResultText();
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

    void ResultText()
    {
        if (BattleManager.Instance.player.GetComponent<Player>().nowHp != 0)
        {
            textMeshpro.text = "½Â¸®";
        }
        else
        {
            textMeshpro.text = "ÆÐ¹è";
        }
    }
}
