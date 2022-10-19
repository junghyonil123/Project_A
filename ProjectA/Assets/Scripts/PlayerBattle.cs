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
        rigid.velocity = Vector2.zero; //�ε������� ���� ���η�

        rigid.AddForce(Vector2.left * 30, ForceMode2D.Impulse); //�и��� �������� ������

        yield return new WaitForSeconds(0.3f); //0.3�ʵ��� �и���

        rigid.velocity = Vector2.zero; //�������η�

        rigid.AddForce(Vector2.right * 50, ForceMode2D.Impulse); //���������� ��������
    }

    void StartFos()
    {
        rigid.AddForce(Vector2.right * 20, ForceMode2D.Impulse);
    }
}
