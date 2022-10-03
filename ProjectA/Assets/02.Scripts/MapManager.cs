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
    public List<bool> specialTile = new List<bool>();
    GameObject newBlock;

    public GameObject CreatMap(Vector2 mapPos , int numberOfNextBlock , GameObject thisMap) //���ο� ���� ������ִ� �Լ�
    {
        if (numberOfNextBlock != 0)
        {
            //���� ������������ �ִٸ� �Ȱ��� ���� ������
            numberOfNextBlock -= 1;
            newBlock = Instantiate(thisMap, mapPos, Quaternion.identity);
            newBlock.GetComponent<Tile>().numberOfNextBlock = numberOfNextBlock;
            return newBlock;
        }

        for (int i = 0; i < mapList.Count; i++)
        {
            if (Random.Range(1, 101) < mapSpawnPer[i])
            {
                //����Ȯ���� �����Ǹ� Ư������ ������

                newBlock = Instantiate(mapList[i], mapPos, Quaternion.identity);
                if (specialTile[i])
                {
                    //���� ���� ���ӵǴ� ����Ⱥ��̶�� numberOfNextBlock�� �Է����ݴϴ�.
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