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

    public delegate void CreateMapDelegate();
    public CreateMapDelegate createMapDelegete;

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

    public int maxExp = 10;
    public int curExp = 0;
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
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            BattleManager.Instance.BattelStart(collision.gameObject.GetComponent<Enemy>());
        }
    }
    
    RaycastHit2D[] hitObjects;
    List<string> hitObjectsList = new List<string>();
    [SerializeField]
    Tile hitTile;

    void PlayerMove()
    {
        if (GameManager.Instance.openedUiCount <= 0 && Input.GetMouseButtonDown(0) && isCanMove && nowActivePoint != 0)
        {
            Debug.Log(Input.GetMouseButtonDown(0));

            isCanSave = true;
            isCanMove = false;

            if (!CheckClick())
            {
                return;
            }

            PlayerFlip();

            Debug.Log("��������Ӿ��");

            if (hitTile.transform == nowStandingTile.GetComponent<Tile>().topMap.transform)
            {
                SetMoveTile();
            }
            else if (hitTile.transform == nowStandingTile.GetComponent<Tile>().bottomMap.transform)
            {
                SetMoveTile();
            }
            else if(hitTile.transform == nowStandingTile.GetComponent<Tile>().leftMap.transform)
            {
                SetMoveTile();
            }
            else if(hitTile.transform == nowStandingTile.GetComponent<Tile>().rightMap.transform)
            {
                SetMoveTile();
            }
            else
            {
                return;
            }

            StartCoroutine("Move");

        }
        else
        {
            if (transform.position == nowStandingTile.position) //�÷��̾ Ŭ���� Ÿ�Ͽ� �����ϱ����� �ٸ�Ÿ�Ϸ� �̵��ϴ°��� �������� isCanMove;
                isCanMove = true;
        }


    } //�÷��̾� �����̱�

    IEnumerator Move()
    {
        while (transform.position != nowStandingTile.position)
        {
            //if (GameManager.Instance.openedUiCount > 0)
            //{
            //    yield return new WaitForSeconds(Time.deltaTime);
            //    continue;
            //}

            transform.position = Vector2.MoveTowards(transform.position, hitTile.transform.position, speed * Time.deltaTime);
            yield return new WaitForSeconds(Time.deltaTime);
        }

        MoveFinish();
    }

    void MoveFinish()
    {
        playerAnimator.SetBool("Walk", false);
        GameManager.Instance.getInfo(InfoType.moveCount , 1);
        GameManager.Instance.CheckPlayerActivePoint();

        AfterMoveFinish();
        DataManager.Instance.SaveData();
    }

    void AfterMoveFinish()
    {
        createMapDelegete();
    }

    bool CheckClick()
    {
        hitObjects = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);   //ray�� �ɸ��� ��ü hit�� ����
        //Debug.Log(hitObjects.transform);

        hitObjectsList.Clear();
        for (int i = 0; i < hitObjects.Length; i++)
        {
            hitObjectsList.Add(hitObjects[i].transform.tag);
        }

        foreach (var item in hitObjects)
        {
            if (item.transform.CompareTag("Tile"))
            {
                hitTile = item.transform.GetComponent<Tile>();
            }
            else if (item.transform.CompareTag("Box") && hitTile.transform == nowStandingTile)
            {
                item.transform.GetComponent<Box>().OpenCloseBox();
            }
        }

        if (!hitObjectsList.Contains("Box"))
        {
            Box.lastBox.CloseBox();
        }
        

        if (hitObjects.Length <= 0 || hitTile.transform.position == nowStandingTile.position)   //���� ray�� ���̾��� ���� �������� ���� �߻��� �������� return
            return false;

        return true;
    }

    void PlayerFlip()
    {
        if (hitTile.transform.position.x < transform.position.x)
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
    }

    void SetMoveTile()
    {
        Debug.Log("�����Դϴ�");
        nowStandingTile = hitTile.transform;  //ray�� ���� ��ü�� target������ �������
        playerAnimator.SetBool("Walk", true);
        nowActivePoint -= hitTile.requiredActivePoint;
    }

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
        
        if (AttackSkillDelegate != null)
        {
            AttackSkillDelegate(ref battelAtk); //battleAtk�� ��� ��ų�� ���� ��Ų�� ������
        }

        return battelAtk;
    }

    
}
