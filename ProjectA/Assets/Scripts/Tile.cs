using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct SpawnMonsterInfo
{
    public GameObject spawnMonster;
    public float spawnPer;
}

[Serializable]
public struct NightMonsterInfo
{
    public GameObject spawnMonster;
    public float spawnPer;
}

public class Tile : MonoBehaviour
{
    public GameObject topMap;
    public GameObject bottomMap;
    public GameObject leftMap;
    public GameObject rightMap;
    [HideInInspector] public GameObject thisMap;

    public int tileType;

    public float tileCreatPercent;

    public bool specialTile;

    public int numberOfNextBlock;

    public int requiredActivePoint;

    public bool canNotSpawnMonster;

    public List<SpawnMonsterInfo> spawnMonsterInfoList = new List<SpawnMonsterInfo>();
    public List<NightMonsterInfo> nightMonsterInfoList = new List<NightMonsterInfo>();

    private void Awake()
    {
        thisMap = gameObject;
        MonsterSpawn();
        //TileLock();
        CreatMap();
    }

    private void Update()
    {
        //TileUnLock();
        //CreatMap();
    }

    private SpriteRenderer[] spriteRenderer;

    //public void TileLock()
    //{
    //    spriteRenderer = gameObject.GetComponentsInChildren<SpriteRenderer>();

    //    for (int i = 0; i < spriteRenderer.Length; i++)
    //    {
    //        spriteRenderer[i].color = new Color(0f, 0f, 0f);
    //    }
    //}

    //public void TileUnLock()
    //{
    //    if (returnDeltaX() <= 5 && returnDeltaY() <= 5)
    //    {
    //        for (int i = 0; i < spriteRenderer.Length; i++)
    //        {
    //            spriteRenderer[i].color = new Color(255f, 255f, 255f);
    //        }
    //    }
    //}

    public void MonsterSpawn()
    {
        if (canNotSpawnMonster)
        {
            return;
        }

        for (int i = 0; i < spawnMonsterInfoList.Count; i++)
        { 
            if (spawnMonsterInfoList[i].spawnPer >= UnityEngine.Random.Range(1 , 101))
            {
                Instantiate(spawnMonsterInfoList[i].spawnMonster, this.transform);  
            }
        }
    }

    public void NightMonsterSpawn()
    {

        if (gameObject.GetComponentInChildren<Enemy>() != null)
        {
            Debug.Log(gameObject.GetComponentInChildren<Enemy>());
            //타일에 몬스터가 존재함
            return;
        }

        if (canNotSpawnMonster)
        {
            return;
        }

        for (int i = 0; i < nightMonsterInfoList.Count; i++)
        {
            if (nightMonsterInfoList[i].spawnPer >= UnityEngine.Random.Range(1, 101))
            {
                Instantiate(nightMonsterInfoList[i].spawnMonster, this.transform);
            }
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


    private RaycastHit2D hitInfo;
    private Tile otherTile;
   
    private void CreatMap()
    {
        if (returnDeltaX() <= 15 && returnDeltaY() <= 10)
        {
            //플레이어와 맵의 위치 차이가 x,y 축으로 20이상 나지 않는다면 맵을 생성
            CreatXMap(ref leftMap, -3, 0);
            CreatXMap(ref rightMap, 3, 0);
            CreatXMap(ref topMap, 0, 3);
            CreatXMap(ref bottomMap, 0, -3);
        }
    }

    public void CreatXMap(ref GameObject xMap , int xAdd , int yAdd) // xMap에 맵을 저장 혹은 생성
    {
        if (CheckTile(xAdd, yAdd))
        {
            // 맵의 위에 다른맵이 있다면 그것을 topMap에 저장함
            xMap = hitInfo.transform.gameObject;
        }
        //else if(xMap != null)
        //{   // 맵은 비어있지만 topMap 저장된 타일이 있다면 (현재는 플레이어가 멀어져도 맵을 지우지 않기에 사용하지 않는 기능)
        //    xMap.SetActive(true);   
        //}
        else
        {
            xMap = MapManager.Instance.CreatMap(new Vector2(transform.position.x + xAdd, transform.position.y + yAdd), numberOfNextBlock, thisMap);
        }
    }

    public bool CheckTile(int xAdd, int yAdd) // 타일이 있다면 true 없다면 false
    {
        hitInfo = Physics2D.Raycast(new Vector2(transform.position.x + xAdd, transform.position.y + yAdd), this.transform.up, 0.1f);

        if (!hitInfo) return false;

        if (!hitInfo.transform.CompareTag("Tile"))
        {
            Debug.Log(hitInfo.transform.parent);
            otherTile = hitInfo.transform.parent.GetComponent<Tile>();
        }
        else
        {
            otherTile = hitInfo.transform.GetComponent<Tile>();
        }

        return true;
    }
}
