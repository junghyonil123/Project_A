using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class FinishCanvas : MonoBehaviour
{
    SpriteRenderer spriteRender;
    public TextMeshProUGUI textMeshpro;
    public TextMeshProUGUI xpTextMesh;
    public TextMeshProUGUI goldTextMesh;

    private static FinishCanvas instance = null;
    private void Awake()
    {
        spriteRender = GetComponent<SpriteRenderer>();
        if (null == instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        textMeshpro = GameObject.Find("result").GetComponent<TextMeshProUGUI>();
        xpTextMesh = GameObject.Find("XpText").GetComponent<TextMeshProUGUI>();
        goldTextMesh = GameObject.Find("MoneyText").GetComponent<TextMeshProUGUI>();
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
        ReturnScene();
    }

    public void ReturnScene()
    {
        if (Input.GetMouseButtonDown(0))
        {
            BattleManager.Instance.isBattle = false;
            gameObject.SetActive(false);
            Time.timeScale = 1;
            Player.Instance.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}
