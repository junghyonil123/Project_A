using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    private bool isCanMove=true;
    
    public float speed;
    
    [SerializeField] private int activePoint = 10;
    
    public Transform target;
    
    public Tile nowStrandingTile;


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
    }

    private void Update()
    {
        PlayerMove();
    }

    void PlayerMove()
    {

        if (Input.GetMouseButtonUp(0) && isCanMove && activePoint != 0)
        {
                        
            isCanMove = false;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);    //���콺 ��ġ�� ray�߻�
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);   //ray�� �ɸ��� ��ü hit�� ����

            if (!hit)   //���� ray�� ���̾��� ���� �������� ���� �߻��� �������� return
                return;

            Debug.Log(hit.collider.gameObject);

            if (hit.transform == nowStrandingTile.topMap.transform)
            {
                target = hit.transform;  //ray�� ���� ��ü�� target������ �������
                nowStrandingTile = target.GetComponent<Tile>();
            }
            else if (hit.transform == nowStrandingTile.bottomMap.transform)
            {
                target = hit.transform;  //ray�� ���� ��ü�� target������ �������
                nowStrandingTile = target.GetComponent<Tile>();
            }
            else if(hit.transform == nowStrandingTile.leftMap.transform)
            {
                target = hit.transform;  //ray�� ���� ��ü�� target������ �������
                nowStrandingTile = target.GetComponent<Tile>();
            }
            else if(hit.transform == nowStrandingTile.rightMap.transform)
            {
                target = hit.transform;  //ray�� ���� ��ü�� target������ �������
                nowStrandingTile = target.GetComponent<Tile>();
            }


            if (transform.position != target.transform.position)
            {   //Player�� ���ִ� ���� ������ activePoint �Ҹ� ���ϵ��� ����
                activePoint--;
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
        nowStrandingTile = hit.transform.GetComponent<Tile>();
        target = nowStrandingTile.transform;
    }
}
