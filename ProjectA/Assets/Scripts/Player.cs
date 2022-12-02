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
            if (instance == null) //instance 가 존재하지않는다면
            {
                var obj = FindObjectOfType<Player>(); //Player 타입이 존재하는지 확인
                if (obj != null)
                {
                    instance = obj; //null이 아니라면 instance에 넣어줌
                }
                else
                {
                    var newPlayer = new GameObject("Player").AddComponent<Player>(); //null이라면 새로만들어줌
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
        ////생성과 동시에 실행되는 Awake는 이미 생성되어있는 싱글톤 오브젝트가 있는지 검사하고 있다면 지금 생성된 오브젝트를 파괴

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

    private bool isCanSave = false;//세이브가 가능함을 표시하는 플래그
    
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

    public void GetItem(Item item) //아이템을 얻는 함수
    {
        Inventory.Instance.GetItem(item);
    }

    public void SetStatus()
    {
        atk = str * 2;
        def = dex * 1;
        maxHp = con * 5;
    } //스텟을 기반으로 능력치를 계산해주는 함수

    public void LvUp()
    {
        lv += 1;
        statusPoint += 5;
        curExp = lv * 10;

    } //레벨업

    public void GetGold(int _gold) //골드를 얻는 함수
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
    } //체력 초기화

    public void ResetActivePoint()
    {
        nowActivePoint = maxActivePoint;
    } //행동력 초기화

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
            Debug.Log("실행댓습니다");
            BattleManager.Instance.BattelStart(collision.gameObject.GetComponent<Enemy>());
        }
    }

    RaycastHit2D hitObject;
    Tile hitTile;

    void PlayerMove()
    {
        if (GameManager.Instance.openedUiCount == 0 && Input.GetMouseButtonDown(0) && isCanMove && nowActivePoint != 0)
        {
            isCanSave = true;
            isCanMove = false;

            if (!ClickTile())
            {
                return;
            }

            PlayerFlip();

            if (hitObject.transform == nowStandingTile.GetComponent<Tile>().topMap.transform)
            {
                SetMoveTile();
            }
            else if (hitObject.transform == nowStandingTile.GetComponent<Tile>().bottomMap.transform)
            {
                SetMoveTile();
            }
            else if(hitObject.transform == nowStandingTile.GetComponent<Tile>().leftMap.transform)
            {
                SetMoveTile();
            }
            else if(hitObject.transform == nowStandingTile.GetComponent<Tile>().rightMap.transform)
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
            if (transform.position == nowStandingTile.position) //플레이어가 클릭한 타일에 도착하기전에 다른타일로 이동하는것을 막기위한 isCanMove;
                isCanMove = true;
        }


    } //플레이어 움직이기

    IEnumerator Move()
    {
        while (transform.position != nowStandingTile.position)
        {
            transform.position = Vector2.MoveTowards(transform.position, hitObject.transform.position, speed * Time.deltaTime);
            yield return new WaitForSeconds(Time.deltaTime);
        }

        MoveFinish();
    }

    void MoveFinish()
    {
        playerAnimator.SetBool("Walk", false);
        GameManager.Instance.getInfo(InfoType.moveCount , 1);
        DataManager.Instance.SaveData();
    }

    bool ClickTile()
    {
        hitObject = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition), Mathf.Infinity);   //ray에 걸리는 물체 hit에 저장
        hitTile = hitObject.transform.GetComponent<Tile>();


        if (!hitObject || hitTile.transform.position == nowStandingTile.position)   //만약 ray가 땅이없는 곳을 눌렀을때 에러 발생을 막기위한 return
            return false;

        return true;
    }

    void PlayerFlip()
    {
        if (hitObject.transform.position.x < transform.position.x)
        {
            //플레이어가 왼쪽으로 간다면 플레이어를 왼쪽을 바라보도록 뒤집어줌
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
        nowStandingTile = hitObject.transform;  //ray에 잡힌 물체를 target변수에 집어넣음
        playerAnimator.SetBool("Walk", true);
        nowActivePoint -= hitTile.requiredActivePoint;
    }

    public void PlayerPosionRay()
    {
        RaycastHit2D hitInfo;
        hitInfo = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 3), this.transform.up, 0.1f);

        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(1f,0f,0f), transform.forward,0.1f);   //ray에 걸리는 물체 hit에 저장
        nowStandingTile = hit.transform;
    } //플레이어의 현재위치 체크



    public override void Die()
    {
        Debug.Log("주것습니다.");
        GameManager.Instance.GameOver();
    } //Die

    public int battelAtk;

    public int GetPlayerAtk()
    {
        battelAtk = atk; 
        Debug.Log(AttackSkillDelegate);
        
        if (AttackSkillDelegate != null)
        {
            AttackSkillDelegate(ref battelAtk); //battleAtk에 모든 스킬을 적용 시킨후 리턴함
        }

        Debug.Log(battelAtk);

        return battelAtk;
    }
}
