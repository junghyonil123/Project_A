using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCanvas : MonoBehaviour
{
    public GameObject playerBattleHp;
    public GameObject enemyBattleHp;
    private float playerBattleHpPer;
    private float enemyBattleHpPer;

    private void Update()
    {
        playerBattleHpPer = Player.Instance.nowHp / Player.Instance.maxHp;
        playerBattleHp.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(0, 300 * playerBattleHpPer);
        enemyBattleHpPer = BattleManager.Instance.enemy.GetComponent<Enemy>().nowHp / BattleManager.Instance.enemy.GetComponent<Enemy>().maxHp;
        enemyBattleHp.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(0, 300 * enemyBattleHpPer);
    }
}
