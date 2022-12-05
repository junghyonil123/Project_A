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
            //Ÿ�Ͽ� ���Ͱ� ������
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


    private RaycastHit2D hitInfo;
    private Tile otherTile;
   
    private void CreatMap()
    {
        if (returnDeltaX() <= 15 && returnDeltaY() <= 10)
        {
            //�÷��̾�� ���� ��ġ ���̰� x,y ������ 20�̻� ���� �ʴ´ٸ� ���� ����
            CreatXMap(ref leftMap, -3, 0);
            CreatXMap(ref rightMap, 3, 0);
            CreatXMap(ref topMap, 0, 3);
            CreatXMap(ref bottomMap, 0, -3);
        }
    }

    public void CreatXMap(ref GameObject xMap , int xAdd , int yAdd) // xMap�� ���� ���� Ȥ�� ����
    {
        if (CheckTile(xAdd, yAdd))
        {
            // ���� ���� �ٸ����� �ִٸ� �װ��� topMap�� ������
            xMap = hitInfo.transform.gameObject;
        }
        //else if(xMap != null)
        //{   // ���� ��������� topMap ����� Ÿ���� �ִٸ� (����� �÷��̾ �־����� ���� ������ �ʱ⿡ ������� �ʴ� ���)
        //    xMap.SetActive(true);   
        //}
        else
        {
            xMap = MapManager.Instance.CreatMap(new Vector2(transform.position.x + xAdd, transform.position.y + yAdd), numberOfNextBlock, thisMap);
        }
    }

    public bool CheckTile(int xAdd, int yAdd) // Ÿ���� �ִٸ� true ���ٸ� false
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
