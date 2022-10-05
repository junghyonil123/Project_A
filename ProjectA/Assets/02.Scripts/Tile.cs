using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject topMap;
    public GameObject bottomMap;
    public GameObject leftMap;
    public GameObject rightMap;

    public GameObject thisMap;

    public int numberOfNextBlock;


    private void Awake()
    {
        CreatMap();
        thisMap = gameObject;
    }

    private void Update()
    {
        SetMapActive();
        CreatMap();
    }

    public void SetMapActive()
    {
        if (returnDeltaX() >= 15 || returnDeltaY() >= 10)
        {
            gameObject.SetActive(false);
        }
    }

    public float returnDeltaX()
    {
        //�÷��̾���� ��ġ���̸� ���밪���� ��ȯ�ϴ� �Լ�
        float playerXPos;
        float deltaX;

        playerXPos = Player.Instance.transform.position.x; //�÷��̾��� x�����ǰ� y�������� �����ϴ� ����
        deltaX = playerXPos - transform.position.x; //�÷��̾�� Ÿ���� �Ÿ����� �����ϴ� ����
        deltaX = playerXPos - transform.position.x <= 0 ? deltaX * -1 : deltaX; //�Ÿ����� ���밪���� �ٲ��ִ� ����

        return deltaX;
    }

    public float returnDeltaY()
    {
        float playerYPos;
        float deltaY;

        playerYPos = Player.Instance.transform.position.y;
        deltaY = playerYPos - transform.position.y;
        deltaY = playerYPos - transform.position.y <= 0 ? deltaY * -1 : deltaY;

        return deltaY;
    }

    private void CreatMap()
    {
        if (returnDeltaX() <= 10 && returnDeltaY() <= 5)
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
        else if(topMap != null)
        {
            // ���� ��������� topMap ����� Ÿ���� �ִٸ�
            topMap.SetActive(true);
        }
        else
        {
            //���� �����
            topMap = MapManager.Instance.CreatMap(new Vector2(transform.position.x, transform.position.y + 3), numberOfNextBlock, thisMap);
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
        else if (bottomMap != null)
        {
            // ���� ��������� topMap ����� Ÿ���� �ִٸ�
            bottomMap.SetActive(true);
        }
        else
        {
            //Debug.Log("���Ҹʸ������");
            bottomMap = MapManager.Instance.CreatMap(new Vector2(transform.position.x, transform.position.y - 3), numberOfNextBlock, thisMap);
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
        else if (leftMap != null)
        {
            // ���� ��������� topMap ����� Ÿ���� �ִٸ�
            leftMap.SetActive(true);
        }
        else
        {
            leftMap = MapManager.Instance.CreatMap(new Vector2(transform.position.x - 3, transform.position.y), numberOfNextBlock, thisMap);
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
        else if (rightMap != null)
        {
            // ���� ��������� topMap ����� Ÿ���� �ִٸ�
            rightMap.SetActive(true);
        }
        else
        {
            rightMap = MapManager.Instance.CreatMap(new Vector2(transform.position.x + 3, transform.position.y), numberOfNextBlock, thisMap);
        }
    }

}
