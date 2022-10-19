using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FinishCanvas : MonoBehaviour
{
    public TextMeshProUGUI textMeshpro;
    public TextMeshProUGUI xpTextMesh;
    public TextMeshProUGUI goldTextMesh;

    private static FinishCanvas instance = null;
    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        textMeshpro = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        xpTextMesh = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        goldTextMesh = transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        BattleManager.Instance.finishCanvas = this.gameObject;
    }
    public static FinishCanvas Instance
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


    private void Update()
    {
        if(BattleManager.Instance.player.GetComponent<Player>().nowHp != 0)
        {
            textMeshpro.text = "½Â¸®";
        }
        else
        {
            textMeshpro.text = "ÆÐ¹è";
        }
    }
}
