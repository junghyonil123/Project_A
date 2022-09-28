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

    public GameObject CreatMap(Vector2 mapPos) //새로운 맵을 만들어주는 함수
    {
        for (int i = 0; i < mapList.Count; i++)
        {
            if (Random.Range(1, 101) < mapSpawnPer[i])
            {
                //Instantiate(mapList[i], mapPos, Quaternion.identity);
                return Instantiate(mapList[i], mapPos, Quaternion.identity);
            }
        }

        return null;
    }
}