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


    public List<GameObject> mapList = new List<GameObject>(); //�����Ǵ� ���� ������ �����ϴ� ����Ʈ
    public List<float> mapSpawnPer = new List<float>(); //���� ������Ȯ���� �����ϴ� ����Ʈ

    public GameObject CreatMap(Vector2 mapPos) //���ο� ���� ������ִ� �Լ�
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