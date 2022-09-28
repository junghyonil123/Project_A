using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform target;
    public float speed;

    private void Update()
    {
        PlayerMove();
    }

    void PlayerMove()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);    //���콺 ��ġ�� ray�߻�
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);   //ray�� �ɸ��� ��ü hit�� ����
            if (hit.collider == null)   //���� ray�� ���̾��� ���� �������� ���� �߻��� �������� return
                return;
            Debug.Log(hit.collider.gameObject);
            target = hit.collider.transform;    //ray�� ���� ��ü�� target������ �������
        }
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

}
