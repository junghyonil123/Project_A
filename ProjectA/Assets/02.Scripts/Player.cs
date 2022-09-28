using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    private bool isCanMove=true;
    public Transform target;
    public float speed;
    [SerializeField] private int activePoint = 10;

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
    }

    private void Update()
    {
        PlayerMove();
    }

    void PlayerMove()
    {
        if (Input.GetMouseButtonUp(0) && isCanMove && activePoint!=0)
        {
            isCanMove = false;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);    //마우스 위치로 ray발사
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);   //ray에 걸리는 물체 hit에 저장
            if (hit.collider == null)   //만약 ray가 땅이없는 곳을 눌렀을때 에러 발생을 막기위한 return
                return;
            if(hit.collider.transform.position == transform.position + new Vector3(-3,-3) || hit.collider.transform.position == transform.position + new Vector3(-3,3) || hit.collider.transform.position == transform.position + new Vector3(3, 3) || hit.collider.transform.position == transform.position + new Vector3(3, -3))
            {   //대각선 이동을 잠금
                Debug.Log(hit.collider.transform.position);
                return;
            }
            Debug.Log(hit.collider.gameObject);
            target = hit.collider.transform;    //ray에 잡힌 물체를 target변수에 집어넣음
            if(transform.position != target.transform.position)     //Player가 서있는 땅을 눌러도 activePoint 소모 안하도록 막음
                activePoint--;
        }
        else if(target == null)
        {
            return;
        }
        else
        {
                if (transform.position == target.position)
                    isCanMove = true;
        }
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    public override void GetDamage(float damage)
    {
        base.GetDamage(damage);
    }
}
