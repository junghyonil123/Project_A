using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    private bool isCanMove=true;
    
    public float speed;
    
    public float maxActivePoint = 10;
    public float nowActivePoint;

    public float str;
    public float dex;
    public float con;
    public float statusPoint;

    public Animator playerAnimator;

    public string job;

    public Transform nowStandingTile;

    public void SetStatus()
    {
        atk = str * 2;
        def = dex * 1;
        maxHp = con * 5;
    }

    public void LvUp()
    {
        lv += 1;
        statusPoint += 5;
    }

    public void ResetHp()
    {
        this.nowHp = this.maxHp;
    }

    public void ResetActivePoint()
    {
        nowActivePoint = maxActivePoint;
    }

    #region singleton
    private static Player instance = null;

    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public static Player Instance
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

    public override void Start()
    {
        base.Start();
        PlayerPosionRay();
        ResetActivePoint();
    }

    private void Update()
    {
        PlayerMove();
    }

    void PlayerMove()
    {

        if (Input.GetMouseButtonUp(0) && isCanMove && nowActivePoint != 0)
        {
            isCanMove = false;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);    //마우스 위치로 ray발사
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);   //ray에 걸리는 물체 hit에 저장

            if (!hit)   //만약 ray가 땅이없는 곳을 눌렀을때 에러 발생을 막기위한 return
                return;

            if (hit.transform.position.x < transform.position.x)
            {
                //플레이어가 왼쪽으로 간다면 플레이어를 왼쪽을 바라보도록 뒤집어줌
                if (transform.localScale.x >= 0)
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
                nowStandingTile = hit.transform;  //ray에 잡힌 물체를 target변수에 집어넣음
                playerAnimator.SetBool("Walk", true);
            }
            else if (hit.transform == nowStandingTile.GetComponent<Tile>().bottomMap.transform)
            {
                nowStandingTile = hit.transform;  
                playerAnimator.SetBool("Walk", true);
            }
            else if(hit.transform == nowStandingTile.GetComponent<Tile>().leftMap.transform)
            {
                nowStandingTile = hit.transform; 
                playerAnimator.SetBool("Walk", true);
            }
            else if(hit.transform == nowStandingTile.GetComponent<Tile>().rightMap.transform)
            {
                nowStandingTile = hit.transform;  
                playerAnimator.SetBool("Walk", true);
            }

            if (transform.position != hit.transform.position)
            {   //Player가 서있는 땅을 눌러도 activePoint 소모 안하도록 막음
                nowActivePoint -= hit.transform.GetComponent<Tile>().requiredActivePoint;
            }

        }
        else if (nowStandingTile == null)
        {
            return;
        }
        else
        {
            if (transform.position == nowStandingTile.position)
                isCanMove = true;
        }

        transform.position = Vector2.MoveTowards(transform.position, nowStandingTile.position, speed * Time.deltaTime);

        if (transform.position == nowStandingTile.position)
        { //이동이끝났음
            playerAnimator.SetBool("Walk", false);
        }
    }

    public override void GetDamage(float damage)
    {
        base.GetDamage(damage);
    }

    public void PlayerPosionRay()
    {
        RaycastHit2D hitInfo;
        hitInfo = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 3), this.transform.up, 0.1f);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.forward,0.1f);   //ray에 걸리는 물체 hit에 저장
        //nowStrandingTile = hit.transform.GetComponent<Tile>();
        nowStandingTile = hit.transform;
    }
}
