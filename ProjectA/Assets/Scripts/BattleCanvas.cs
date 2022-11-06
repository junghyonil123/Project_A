using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class BattleCanvas : MonoBehaviour
{
    public GameObject playerBattleHp;
    public GameObject enemyBattleHp;
    private float playerBattleHpPer;
    private float enemyBattleHpPer;

    //SpriteRenderer spriteRender;
    public TextMeshProUGUI resultTextMesh;
    public TextMeshProUGUI xpTextMesh;
    public TextMeshProUGUI goldTextMesh;

    #region singleton
    private static BattleCanvas instance = null;

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
    }
    public static BattleCanvas Instance
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

    private void Update()
    {
        if (PlayerBattle.Instance.isBattle)
        {
            playerBattleHpPer = Player.Instance.nowHp / Player.Instance.maxHp;
            playerBattleHp.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(0, 300 * playerBattleHpPer);

            enemyBattleHpPer = PlayerBattle.Instance.enemy.GetComponent<Enemy>().nowHp 
                / PlayerBattle.Instance.enemy.GetComponent<Enemy>().maxHp;
            enemyBattleHp.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(0, 300 * enemyBattleHpPer);
        }
    }

    //public void ReturnScene()
    //{
    //    Player.Instance.transform.position = Player.Instance.nowStandingTile.transform.position;
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        gameObject.SetActive(false);
    //        Player.Instance.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    //        PlayerBattle.Instance.battleCanvas.SetActive(false);
    //        MainCanvas.Instance.gameObject.SetActive(true);
    //    }
    //}

    public void MonsterItemDrop(Item item)
    {

    }
}
