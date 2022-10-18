using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FinishCanvas : MonoBehaviour
{
    public TextMeshProUGUI textMeshpro;

    private void Awake()
    {
        textMeshpro = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        BattleManager.Instance.finishCanvas = this.gameObject;
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
