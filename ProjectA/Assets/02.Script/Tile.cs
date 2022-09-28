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
        float playerXPos = GameManager.Instance.player.transform.position.x; //플레이어의 x포지션과 y포지션을 저장하는 변수
        float playerYPos = GameManager.Instance.player.transform.position.y;

        float deltaX = playerXPos - transform.position.x; //플레이어와 타일의 거리차를 저장하는 변수
        float deltaY = playerYPos - transform.position.y;
         
        deltaX = playerXPos - transform.position.x <= 0 ? deltaX * -1 : deltaX; //거리차를 절대값으로 바꿔주는 역할
        deltaY = playerYPos - transform.position.y <= 0 ? deltaY * -1 : deltaY;

        if (deltaX <= 20 && deltaY <= 20)
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
        else
        {
            //Debug.Log("탑맵만들어짐");
            topMap = MapManager.Instance.CreatMap(new Vector2(transform.position.x, transform.position.y + 3));
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
        else
        {
            //Debug.Log("바텀맵만들어짐");
            bottomMap = MapManager.Instance.CreatMap(new Vector2(transform.position.x, transform.position.y - 3));
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
            // 맵의 위에 다른맵이 있다면 그것을 topMap에 저장함
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
