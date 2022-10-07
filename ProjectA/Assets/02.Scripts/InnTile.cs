using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnTile : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            Player.Instance.nowHp = Player.Instance.maxHp;
    }
}
