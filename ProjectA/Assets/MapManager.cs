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

    public void CreatMap(Vector2 mapPos) //���ο� ���� ������ִ� �Լ�
    {
        Debug.Log("mapPos");
        for (int i = 0; i < 1; i++)
        {
            if (Random.Range(1, 101) < mapSpawnPer[i])
            {
                Instantiate(mapList[i], mapPos, Quaternion.identity);
                //return Instantiate(mapList[i], mapPos, Quaternion.identity);
                Debug.Log("�ٸ������");
            }
        }
        //return gameObject;

    }
}