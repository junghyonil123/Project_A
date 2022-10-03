using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    #region singleton
    private static MapManager instance = null;

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
    public static MapManager Instance
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


    public List<GameObject> mapList = new List<GameObject>(); //생성되는 맵의 종류를 저장하는 리스트
    public List<float> mapSpawnPer = new List<float>(); //맵이 생성될확률을 저장하는 리스트
    public List<bool> specialTile = new List<bool>();
    GameObject newBlock;

    public GameObject CreatMap(Vector2 mapPos , int numberOfNextBlock , GameObject thisMap) //새로운 맵을 만들어주는 함수
    {
        if (numberOfNextBlock != 0)
        {
            //만약 다음블럭갯수가 있다면 똑같은 블럭을 생성함
            numberOfNextBlock -= 1;
            newBlock = Instantiate(thisMap, mapPos, Quaternion.identity);
            newBlock.GetComponent<Tile>().numberOfNextBlock = numberOfNextBlock;
            return newBlock;
        }

        for (int i = 0; i < mapList.Count; i++)
        {
            if (Random.Range(1, 101) < mapSpawnPer[i])
            {
                //생성확률이 만족되면 특정블럭을 생성함

                newBlock = Instantiate(mapList[i], mapPos, Quaternion.identity);
                if (specialTile[i])
                {
                    //만약 맵이 연속되는 스페셜블럭이라면 numberOfNextBlock를 입력해줍니다.
                    newBlock.GetComponent<Tile>().numberOfNextBlock = Random.Range(4, 5);
                }
                else
                {
                    newBlock.GetComponent<Tile>().numberOfNextBlock = 0;
                }
                return newBlock;
            }
        }

        return null;
    }
}