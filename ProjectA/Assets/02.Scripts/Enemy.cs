using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : Unit
{
    public SpriteRenderer spriteRenderer;

    public bool isBattle = false;

    private void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        BattleGetDamage();
        Die();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            BattleManager.Instance.enemy = gameObject;
            DontDestroyOnLoad(this);
            spriteRenderer.sortingOrder = -2;
            BattleManager.Instance.player.GetComponent<SpriteRenderer>().sortingOrder = -2;
            SceneManager.LoadScene("BattleScene");
        }
    }

    public void BattleGetDamage()
    {
        if (isBattle)
        {
            Debug.Log("Battle2");
            GetDamage(BattleManager.Instance.player.GetComponent<Player>().atk);
            isBattle = false;
        }
    }

    public override void Die()
    {
        if(nowHp <= 0)
        {
            GameObject.Find("EnemyBattle").SetActive(false);
        }
        base.Die();
    }
}
