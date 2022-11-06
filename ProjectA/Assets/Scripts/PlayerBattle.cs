using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattle : MonoBehaviour
{
    public GameObject enemy;
    Rigidbody2D enemyRigid;

    public GameObject player;
    Rigidbody2D playerRigid;

    public bool isBattle = false;
    public Transform playerBattlePos;
    public Transform enemyBattlePos;

    public GameObject battleCanvas;
    public GameObject finishCanvas;

    #region singleton
    private static PlayerBattle instance = null;

    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }

        playerRigid = player.GetComponent<Rigidbody2D>();
    }
    public static PlayerBattle Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (!isBattle)
            {
                enemy = collision.gameObject; //enemy�� �΋H�� ���ͷ� ����
                enemyRigid = enemy.GetComponent<Rigidbody2D>();

                isBattle = true; // ������ �����ϴ� ���� isBattle�� true�θ���

                transform.position = playerBattlePos.position; //�÷��̾� ��ġ�� ��Ʋ������ �ű��
                battleCanvas.SetActive(true); //��Ʋ���� Ų��
                MainCanvas.Instance.gameObject.SetActive(false); //���� ���Ӽ��� UI�� ����
                transform.localScale = new Vector3(2, 2, 2);
                collision.gameObject.transform.position = enemyBattlePos.transform.position; // ������ ��ġ�� ��Ʋ������ �ű��
                
                Invoke("StartFos", 1f);
            }
            else
            {
                GetComponent<AudioSource>().Play();

                Player.Instance.GetDamage(enemy.GetComponent<Enemy>().atk);
                enemy.GetComponent<Enemy>().GetDamage(Player.Instance.atk);

                if (enemy.GetComponent<Enemy>().nowHp <= 0)
                {
                    //enemy.GetComponent<Enemy>().Die();
                    StopCoroutine("KnockBack");
                    playerRigid.velocity = Vector2.zero;
                    for (int i = 0; i < 1000000; i++)
                    {
                        transform.position = player.GetComponent<Player>().nowStandingTile.position;
                    }
                    Debug.Log(playerRigid.velocity);
                }
                else
                {
                    StartCoroutine("KnockBack");
                }
            }
        }
    }

    IEnumerator KnockBack()
    {
        if (enemy != null)
        {
            playerRigid.velocity = Vector2.zero; //�ε������� ���� ���η�
            enemyRigid.velocity = Vector2.zero;
            Debug.Log("1");
        }
        else
        {
            yield break;
        }
            
        if (enemy != null)
        {
            playerRigid.AddForce(Vector2.left * 30, ForceMode2D.Impulse); //�и��� �������� ������
            enemyRigid.AddForce(Vector2.right * 30, ForceMode2D.Impulse);
            Debug.Log("2");
        }
        else
        {
            playerRigid.velocity = Vector2.zero; //�������η�
            enemyRigid.velocity = Vector2.zero;
            yield break;
        }

        for (int i = 0; i < 30; i++)
        {
            if (enemy != null)
            {
                yield return new WaitForSeconds(0.01f);
            }
            else
            {
                playerRigid.velocity = Vector2.zero;
                enemyRigid.velocity = Vector2.zero;
                yield break;
            }
        } //0.3�ʵ��� �и���

        if (enemy != null)
        {
            playerRigid.velocity = Vector2.zero; //�������η�
            enemyRigid.velocity = Vector2.zero;
            Debug.Log("3");
        }
        else
        {
<<<<<<< Updated upstream
            rigid.velocity = Vector2.zero; //�ε������� ���� ���η�

            rigid.AddForce(Vector2.right * 30, ForceMode2D.Impulse); //�и��� �������� ������
            
            yield return new WaitForSeconds(0.3f); //0.3�ʵ��� �и���
            
            rigid.velocity = Vector2.zero; //�������η�
            
            rigid.AddForce(Vector2.left * 50, ForceMode2D.Impulse); //���������� ��������
=======
            yield break;
        }

        if (enemy != null)
        {
            playerRigid.AddForce(Vector2.right * 50, ForceMode2D.Impulse); //���������� ��������
            enemyRigid.AddForce(Vector2.left * 50, ForceMode2D.Impulse);
            Debug.Log("4");
        }
        else
        {
            playerRigid.velocity = Vector2.zero; //�������η�
            enemyRigid.velocity = Vector2.zero;
            yield break;
>>>>>>> Stashed changes
        }
    }

    void StartFos()
    {
        playerRigid.AddForce(Vector2.right * 20, ForceMode2D.Impulse);
        enemyRigid.AddForce(Vector2.left * 20, ForceMode2D.Impulse);
    }

    public void FinishCanvasOpen()
    {
        finishCanvas.SetActive(true);
    }
}
