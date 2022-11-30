using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : Unit
{
    #region Singleton
    private static Player instance;
    public static Player Instance
    {
        get
        {
            if (instance == null) //instance �� ���������ʴ´ٸ�
            {
                var obj = FindObjectOfType<Player>(); //Player Ÿ���� �����ϴ��� Ȯ��
                if (obj != null)
                {
                    instance = obj; //null�� �ƴ϶�� instance�� �־���
                }
                else
                {
                    var newPlayer = new GameObject("Player").AddComponent<Player>(); //null�̶�� ���θ������
                    instance = newPlayer;
                }
            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }

    private void Awake()
    {
        ////������ ���ÿ� ����Ǵ� Awake�� �̹� �����Ǿ��ִ� �̱��� ������Ʈ�� �ִ��� �˻��ϰ� �ִٸ� ���� ������ ������Ʈ�� �ı�

        var objs = FindObjectsOfType<Player>();
        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }

    }
    #endregion

    public delegate int AttackSkill(ref int atk);
    public AttackSkill AttackSkillDelegate;
    
    
    public StatusCanvas statusCanvs;

    public bool isCanMove=true;
    public bool isFinishBattle = false;
    public bool isFight = false;

    private bool isCanSave = false;//���̺갡 �������� ǥ���ϴ� �÷���
    
    public float speed;

    public int maxActivePoint = 10;
    public int nowActivePoint;

    public int str;
    public int dex;
    public int con;
    public int statusPoint;
    public int lv;

    private int maxExp = 10;
    private int curExp;
    private int gold;

    public Animator playerAnimator;

    public string job;

    public Transform nowStandingTile;

    public void GetItem(Item item) //�������� ��� �Լ�
    {
        Inventory.Instance.GetItem(item);
    }

    public void SetStatus()
    {
        atk = str * 2;
        def = dex * 1;
        maxHp = con * 5;
    } //������ ������� �ɷ�ġ�� ������ִ� �Լ�

    public void LvUp()
    {
        lv += 1;
        statusPoint += 5;
        curExp = lv * 10;

    } //������

    public void GetGold(int _gold) //��带 ��� �Լ�
    {
        gold += _gold; 
    }

    public void GetExp(int _exp)
    {
        curExp += _exp;

        if (curExp >= maxExp)
        {
            curExp = (curExp - maxExp);
            LvUp();
        }
    }


    public void ResetHp()
    {
        this.nowHp = this.maxHp;
    } //ü�� �ʱ�ȭ

    public void ResetActivePoint()
    {
        nowActivePoint = maxActivePoint;
    } //�ൿ�� �ʱ�ȭ

    public void Start()
    {
        SetStatus();
        ResetHp();
        PlayerPosionRay();
        ResetActivePoint();
    }

    private void Update()
    {
        PlayerMove();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            GetItem(collision.GetComponent<Item>());
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("�������ϴ�");
            BattleManager.Instance.BattelStart(collision.gameObject.GetComponent<Enemy>());
        }
    }
        
    void PlayerMove()
    {
        if (Input.GetMouseButtonUp(0) && isCanMove && nowActivePoint != 0 && statusCanvs.isOpenCanvas)
        {
            isCanSave = true;
            isCanMove = false;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);    //���콺 ��ġ�� ray�߻�
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);   //ray�� �ɸ��� ��ü hit�� ����
            Tile hitTile = hit.transform.GetComponent<Tile>();
            if (!hit)   //���� ray�� ���̾��� ���� �������� ���� �߻��� �������� return
                return;

            if (hit.transform.position.x < transform.position.x)
            {
                //�÷��̾ �������� ���ٸ� �÷��̾ ������ �ٶ󺸵��� ��������
                if (transform.localScale.x > 0)
                {
                    transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                }
            }
            else
            {
                if (transform.localScale.x < 0)
                {
                    transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                }
            }

            if (hit.transform == nowStandingTile.GetComponent<Tile>().topMap.transform)
            {
                nowStandingTile = hit.transform;  //ray�� ���� ��ü�� target������ �������
                playerAnimator.SetBool("Walk", true);
                nowActivePoint -= hitTile.requiredActivePoint;
            }
            else if (hit.transform == nowStandingTile.GetComponent<Tile>().bottomMap.transform)
            {
                nowStandingTile = hit.transform;  
                playerAnimator.SetBool("Walk", true);
                nowActivePoint -= hitTile.requiredActivePoint;
            }
            else if(hit.transform == nowStandingTile.GetComponent<Tile>().leftMap.transform)
            {
                nowStandingTile = hit.transform; 
                playerAnimator.SetBool("Walk", true);
                nowActivePoint -= hitTile.requiredActivePoint;
            }
            else if(hit.transform == nowStandingTile.GetComponent<Tile>().rightMap.transform)
            {
                nowStandingTile = hit.transform;  
                playerAnimator.SetBool("Walk", true);
                nowActivePoint -= hitTile.requiredActivePoint;
            }

        }
        else if (nowStandingTile == null)
        {
            return;
        }
        else
        {
            if (transform.position == nowStandingTile.position) //�÷��̾ Ŭ���� Ÿ�Ͽ� �����ϱ����� �ٸ�Ÿ�Ϸ� �̵��ϴ°��� �������� isCanMove;
                isCanMove = true;
        }

            transform.position = Vector2.MoveTowards(transform.position, nowStandingTile.position, speed * Time.deltaTime);

        if (transform.position == nowStandingTile.position)
        { //�̵��̳�����
            playerAnimator.SetBool("Walk", false);

            if (isCanSave)
            {
                //�ѹ� �̵��� �� ���� �����͸� ��������
                DataManager.Instance.SaveData();

                isCanSave = false;
            }
        }
    } //�÷��̾� �����̱�

    public void PlayerPosionRay()
    {
        RaycastHit2D hitInfo;
        hitInfo = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 3), this.transform.up, 0.1f);

        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(1f,0f,0f), transform.forward,0.1f);   //ray�� �ɸ��� ��ü hit�� ����
        nowStandingTile = hit.transform;
    } //�÷��̾��� ������ġ üũ

    public override void Die()
    {
        Debug.Log("�ְͽ��ϴ�.");
        GameManager.Instance.GameOver();
    } //Die

    public int battelAtk;

    public int GetPlayerAtk()
    {
        battelAtk = atk; 
        Debug.Log(AttackSkillDelegate);
        
        if (AttackSkillDelegate != null)
        {
            AttackSkillDelegate(ref battelAtk); //battleAtk�� ��� ��ų�� ���� ��Ų�� ������
        }

        Debug.Log(battelAtk);

        return battelAtk;
    }
}
