using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public Image playerPortrait;
    public Image enemyPortrait;
    public GameObject player;
    public GameObject enemy;
    public void BattelStart(GameObject _enemy)
    {
        player = Player.Instance.gameObject;
        enemy = _enemy;

        playerPortrait.sprite = player.GetComponent<SpriteRenderer>().sprite;
        enemyPortrait.sprite = enemy.GetComponent<SpriteRenderer>().sprite;
    }
}
