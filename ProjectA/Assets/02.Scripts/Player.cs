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
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);    //마우스 위치로 ray발사
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);   //ray에 걸리는 물체 hit에 저장
            if (hit.collider == null)   //만약 ray가 땅이없는 곳을 눌렀을때 에러 발생을 막기위한 return
                return;
            Debug.Log(hit.collider.gameObject);
            target = hit.collider.transform;    //ray에 잡힌 물체를 target변수에 집어넣음
        }
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

}
