using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : Unit
{
    public Sprite sprite;

    private void Awake()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("col");
            BattleManager.Instance.enemy = gameObject;
            Debug.Log("Com");
            DontDestroyOnLoad(this);
            gameObject.SetActive(false);
            BattleManager.Instance.player.SetActive(false);
            SceneManager.LoadScene("BattleScene");
        }
    }
}
