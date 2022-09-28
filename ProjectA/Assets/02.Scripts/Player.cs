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
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);    //���콺 ��ġ�� ray�߻�
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);   //ray�� �ɸ��� ��ü hit�� ����
            if (hit.collider == null)   //���� ray�� ���̾��� ���� �������� ���� �߻��� �������� return
                return;
            if(hit.collider.transform.position == transform.position + new Vector3(-3,-3) || hit.collider.transform.position == transform.position + new Vector3(-3,3) || hit.collider.transform.position == transform.position + new Vector3(3, 3) || hit.collider.transform.position == transform.position + new Vector3(3, -3))
            {   //�밢�� �̵��� ���
                Debug.Log(hit.collider.transform.position);
                return;
            }
            Debug.Log(hit.collider.gameObject);
            target = hit.collider.transform;    //ray�� ���� ��ü�� target������ �������
            if(transform.position != target.transform.position)     //Player�� ���ִ� ���� ������ activePoint �Ҹ� ���ϵ��� ����
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
