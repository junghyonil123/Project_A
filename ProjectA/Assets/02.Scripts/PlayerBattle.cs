using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattle : Unit
{
    Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Invoke("StartFos", 1f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<AudioSource>().Play();
        StartCoroutine("KnockBack");
        GetDamage(BattleManager.Instance.enemy.GetComponent<Enemy>().atk);
        Debug.Log(collision.gameObject.name);
        BattleManager.Instance.enemy.GetComponent<Enemy>().isBattle = true;
        BattleManager.Instance.player.GetComponent<Player>().isBattle = true;
    }

    IEnumerator KnockBack()
    {
        rigid.velocity = Vector2.zero; //부딪힌순간 힘을 제로로

        rigid.AddForce(Vector2.left * 30, ForceMode2D.Impulse); //밀리는 방향으로 힘을줌

        yield return new WaitForSeconds(0.3f); //0.3초동안 밀리고

        rigid.velocity = Vector2.zero; //힘을제로로

        rigid.AddForce(Vector2.right * 50, ForceMode2D.Impulse); //적방향으로 힘을받음
    }

    void StartFos()
    {
        rigid.AddForce(Vector2.right * 20, ForceMode2D.Impulse);
    }
}
