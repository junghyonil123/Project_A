using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    private bool isCanMove=true;
    
    public float speed;
    
    public float maxActivePoint = 10;
    public float nowActivePoint;

    public Animator playerAnimator;

    public void ResetActivePoint()
    {
        nowActivePoint = maxActivePoint;
    }

    public Transform target;
    
    //public Tile nowStrandingTile;


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

            if (hit.transform == target.GetComponent<Tile>().topMap.transform)
            {
                target = hit.transform;  //ray에 잡힌 물체를 target변수에 집어넣음
                playerAnimator.SetBool("Walk", true);
            }
            else if (hit.transform == target.GetComponent<Tile>().bottomMap.transform)
            {
                target = hit.transform;  
                playerAnimator.SetBool("Walk", true);
            }
            else if(hit.transform == target.GetComponent<Tile>().leftMap.transform)
            {
                target = hit.transform; 
                playerAnimator.SetBool("Walk", true);
            }
            else if(hit.transform == target.GetComponent<Tile>().rightMap.transform)
            {
                target = hit.transform;  
                playerAnimator.SetBool("Walk", true);
            }


            if (transform.position != target.transform.position)
            {   //Player가 서있는 땅을 눌러도 activePoint 소모 안하도록 막음
                nowActivePoint--;
            }

        }
        else if (target == null)
        {
            return;
        }
        else
        {
            if (transform.position == target.position)
                isCanMove = true;
        }

        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (transform.position == target.position)
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
        target = hit.transform;
    }
}
