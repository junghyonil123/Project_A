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
        //플레이어와의 위치차이를 절대값으로 반환하는 함수
        float playerXPos;
        float deltaX;

        playerXPos = Player.Instance.transform.position.x; //플레이어의 x포지션과 y포지션을 저장하는 변수
        deltaX = playerXPos - transform.position.x; //플레이어와 타일의 거리차를 저장하는 변수
        deltaX = playerXPos - transform.position.x <= 0 ? deltaX * -1 : deltaX; //거리차를 절대값으로 바꿔주는 역할

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
            //플레이어와 맵의 위치 차이가 x,y 축으로 20이상 나지 않는다면 맵을 생성
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
            // 맵의 위에 다른맵이 있다면 그것을 topMap에 저장함
            topMap = hitInfo.transform.gameObject;
        }
        else if(topMap != null)
        {
            // 맵은 비어있지만 topMap 저장된 타일이 있다면
            topMap.SetActive(true);
        }
        else
        {
            //맵을 만든다
            topMap = MapManager.Instance.CreatMap(new Vector2(transform.position.x, transform.position.y + 3), numberOfNextBlock, thisMap);
        }
    }

    private void CreatBottomMap()
    {

        RaycastHit2D hitInfo;
        hitInfo = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 3), this.transform.up, 0.1f);

        if (hitInfo)
        {
            // 맵의 위에 다른맵이 있다면 그것을 topMap에 저장함
            bottomMap = hitInfo.transform.gameObject;
        }
        else if (bottomMap != null)
        {
            // 맵은 비어있지만 topMap 저장된 타일이 있다면
            bottomMap.SetActive(true);
        }
        else
        {
            //Debug.Log("바텀맵만들어짐");
            bottomMap = MapManager.Instance.CreatMap(new Vector2(transform.position.x, transform.position.y - 3), numberOfNextBlock, thisMap);
        }
    }

    private void CreatLeftMap()
    {
        RaycastHit2D hitInfo;
        hitInfo = Physics2D.Raycast(new Vector2(transform.position.x - 3, transform.position.y), this.transform.up, 0.1f);

        if (hitInfo)
        {
            // 맵의 위에 다른맵이 있다면 그것을 topMap에 저장함
            leftMap = hitInfo.transform.gameObject;
        }
        else if (leftMap != null)
        {
            // 맵은 비어있지만 topMap 저장된 타일이 있다면
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
            // 맵의 위에 다른맵이 있다면 그것을 topMap에 저장함
            rightMap = hitInfo.transform.gameObject;
        }
        else if (rightMap != null)
        {
            // 맵은 비어있지만 topMap 저장된 타일이 있다면
            rightMap.SetActive(true);
        }
        else
        {
            rightMap = MapManager.Instance.CreatMap(new Vector2(transform.position.x + 3, transform.position.y), numberOfNextBlock, thisMap);
        }
    }

}
