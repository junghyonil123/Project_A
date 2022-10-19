using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBattle : MonoBehaviour
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
        StartCoroutine("KnockBack");
    }

    IEnumerator KnockBack()
    {
        rigid.velocity = Vector2.zero; //�ε������� ���� ���η�

        rigid.AddForce(Vector2.right * 30, ForceMode2D.Impulse); //�и��� �������� ������

        yield return new WaitForSeconds(0.3f); //0.3�ʵ��� �и���

        rigid.velocity = Vector2.zero; //�������η�

        rigid.AddForce(Vector2.left * 50, ForceMode2D.Impulse); //���������� ��������
    }
    void StartFos()
    {
        rigid.AddForce(Vector2.left * 20, ForceMode2D.Impulse);
    }
}
