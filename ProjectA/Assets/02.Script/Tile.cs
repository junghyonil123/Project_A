using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tile : MonoBehaviour
{
    public GameObject topMap;
    public GameObject bottomMap;
    public GameObject leftMap;
    public GameObject rightMap;

    private void Awake()
    {
        CreatMap();
        MapManager.creatMapEvent += CreatMap;
    }
    
    private void CreatMap()
    {
        float playerXPos = GameManager.Instance.player.transform.position.x; //�÷��̾��� x�����ǰ� y�������� �����ϴ� ����
        float playerYPos = GameManager.Instance.player.transform.position.y;

        float deltaX = playerXPos - transform.position.x; //�÷��̾�� Ÿ���� �Ÿ����� �����ϴ� ����
        float deltaY = playerYPos - transform.position.y;
         
        deltaX = playerXPos - transform.position.x <= 0 ? deltaX * -1 : deltaX; //�Ÿ����� ���밪���� �ٲ��ִ� ����
        deltaY = playerYPos - transform.position.y <= 0 ? deltaY * -1 : deltaY;

        if (deltaX <= 20 && deltaY <= 20)
        {
            //�÷��̾�� ���� ��ġ ���̰� x,y ������ 20�̻� ���� �ʴ´ٸ� ���� ����
            CreatTopMap();
            CreatBottomMap();
            CreatRightMap();
            CreatLeftMap();
        }
        
    }

    private void CreatTopMap()
    {

        RaycastHit2D hitInfo;
        hitInfo = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 3), this.transform.up, 0.1f);

        if (hitInfo)
        {
            // ���� ���� �ٸ����� �ִٸ� �װ��� topMap�� ������
            topMap = hitInfo.transform.gameObject;
        }
        else
        {
            //Debug.Log("ž�ʸ������");
            topMap = MapManager.Instance.CreatMap(new Vector2(transform.position.x, transform.position.y + 3));
        }
    }

    private void CreatBottomMap()
    {

        RaycastHit2D hitInfo;
        hitInfo = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 3), this.transform.up, 0.1f);

        if (hitInfo)
        {
            // ���� ���� �ٸ����� �ִٸ� �װ��� topMap�� ������
            bottomMap = hitInfo.transform.gameObject;
        }
        else
        {
            //Debug.Log("���Ҹʸ������");
            bottomMap = MapManager.Instance.CreatMap(new Vector2(transform.position.x, transform.position.y - 3));
        }
    }

    private void CreatLeftMap()
    {
        RaycastHit2D hitInfo;
        hitInfo = Physics2D.Raycast(new Vector2(transform.position.x - 3, transform.position.y), this.transform.up, 0.1f);

        if (hitInfo)
        {
            // ���� ���� �ٸ����� �ִٸ� �װ��� topMap�� ������
            leftMap = hitInfo.transform.gameObject;
        }
        else
        {
            leftMap = MapManager.Instance.CreatMap(new Vector2(transform.position.x - 3, transform.position.y));
        }
    }
    
    private void CreatRightMap()
    {
        RaycastHit2D hitInfo;
        hitInfo = Physics2D.Raycast(new Vector2(transform.position.x + 3, transform.position.y), this.transform.up, 0.1f);

        if (hitInfo)
        {
            // ���� ���� �ٸ����� �ִٸ� �װ��� topMap�� ������
            rightMap = hitInfo.transform.gameObject;
        }
        else
        {
            rightMap = MapManager.Instance.CreatMap(new Vector2(transform.position.x + 3, transform.position.y));
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y + 3), this.transform.up * 0.3f, Color.green);
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y - 3), this.transform.up * 0.3f, Color.green);
    }
}
