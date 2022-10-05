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

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);    //���콺 ��ġ�� ray�߻�
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);   //ray�� �ɸ��� ��ü hit�� ����

            if (!hit)   //���� ray�� ���̾��� ���� �������� ���� �߻��� �������� return
                return;

            if (hit.transform == target.GetComponent<Tile>().topMap.transform)
            {
                target = hit.transform;  //ray�� ���� ��ü�� target������ �������
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
            {   //Player�� ���ִ� ���� ������ activePoint �Ҹ� ���ϵ��� ����
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
        { //�̵��̳�����
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

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.forward,0.1f);   //ray�� �ɸ��� ��ü hit�� ����
        //nowStrandingTile = hit.transform.GetComponent<Tile>();
        target = hit.transform;
    }
}
