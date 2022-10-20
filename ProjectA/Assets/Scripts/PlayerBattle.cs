using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattle : MonoBehaviour
{
    public GameObject enemy;
    Rigidbody2D playerRigid;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (!BattleManager.Instance.isBattle)
            {
                enemy = collision.gameObject;
                BattleManager.Instance.enemy = collision.gameObject;
                BattleManager.Instance.isBattle = true;
                transform.position = BattleManager.Instance.playerBattlePos.transform.position; //플레이어 위치를 배틀신으로 옮긴다
                transform.localScale = new Vector3(2, 2, 2);
                collision.gameObject.transform.position = BattleManager.Instance.enemyBattlePos.transform.position; // 몬스터의 위치를 배틀신으로 옮긴다
                Invoke("StartFos", 1f);
            }
            else
            {
                GetComponent<AudioSource>().Play();
                Player.Instance.GetDamage(enemy.GetComponent<Enemy>().atk);
                enemy.GetComponent<Enemy>().GetDamage(Player.Instance.atk);

                StartCoroutine(_PlayerKnockBack(playerRigid));
                StartCoroutine(EnemyKnockBack(enemy.GetComponent<Rigidbody2D>()));


            }
        }
    }
    private void Awake()
    {
        playerRigid = GetComponent<Rigidbody2D>();
    }

    IEnumerator _PlayerKnockBack(Rigidbody2D rigid)
    {
        if (!Player.Instance.isDie || !enemy.GetComponent<Enemy>().isDie)
        {
            rigid.velocity = Vector2.zero; //부딪힌순간 힘을 제로로

            rigid.AddForce(Vector2.left * 30, ForceMode2D.Impulse); //밀리는 방향으로 힘을줌

            yield return new WaitForSeconds(0.3f); //0.3초동안 밀리고

            if (Player.Instance.isDie || enemy.GetComponent<Enemy>().isDie)
            {
                StopCoroutine("_PlayerKnockBack");
            }

            rigid.velocity = Vector2.zero; //힘을제로로

            rigid.AddForce(Vector2.right * 50, ForceMode2D.Impulse); //적방향으로 힘을받음
            
        }
    }

    IEnumerator EnemyKnockBack(Rigidbody2D rigid)
    {
        if (!Player.Instance.isDie || !enemy.GetComponent<Enemy>().isDie)
        {
            rigid.velocity = Vector2.zero; //부딪힌순간 힘을 제로로

            rigid.AddForce(Vector2.right * 30, ForceMode2D.Impulse); //밀리는 방향으로 힘을줌
            
            yield return new WaitForSeconds(0.3f); //0.3초동안 밀리고
            
            rigid.velocity = Vector2.zero; //힘을제로로
            
            rigid.AddForce(Vector2.left * 50, ForceMode2D.Impulse); //적방향으로 힘을받음
        }
    }

    void StartFos()
    {
        playerRigid.AddForce(Vector2.right * 20, ForceMode2D.Impulse);
        enemy.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 20, ForceMode2D.Impulse);
    }

}
