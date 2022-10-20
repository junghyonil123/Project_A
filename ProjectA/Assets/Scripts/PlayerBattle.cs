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
                transform.position = BattleManager.Instance.playerBattlePos.transform.position; //�÷��̾� ��ġ�� ��Ʋ������ �ű��
                transform.localScale = new Vector3(2, 2, 2);
                collision.gameObject.transform.position = BattleManager.Instance.enemyBattlePos.transform.position; // ������ ��ġ�� ��Ʋ������ �ű��
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
            rigid.velocity = Vector2.zero; //�ε������� ���� ���η�

            rigid.AddForce(Vector2.left * 30, ForceMode2D.Impulse); //�и��� �������� ������

            yield return new WaitForSeconds(0.3f); //0.3�ʵ��� �и���

            if (Player.Instance.isDie || enemy.GetComponent<Enemy>().isDie)
            {
                StopCoroutine("_PlayerKnockBack");
            }

            rigid.velocity = Vector2.zero; //�������η�

            rigid.AddForce(Vector2.right * 50, ForceMode2D.Impulse); //���������� ��������
            
        }
    }

    IEnumerator EnemyKnockBack(Rigidbody2D rigid)
    {
        if (!Player.Instance.isDie || !enemy.GetComponent<Enemy>().isDie)
        {
            rigid.velocity = Vector2.zero; //�ε������� ���� ���η�

            rigid.AddForce(Vector2.right * 30, ForceMode2D.Impulse); //�и��� �������� ������
            
            yield return new WaitForSeconds(0.3f); //0.3�ʵ��� �и���
            
            rigid.velocity = Vector2.zero; //�������η�
            
            rigid.AddForce(Vector2.left * 50, ForceMode2D.Impulse); //���������� ��������
        }
    }

    void StartFos()
    {
        playerRigid.AddForce(Vector2.right * 20, ForceMode2D.Impulse);
        enemy.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 20, ForceMode2D.Impulse);
    }

}
