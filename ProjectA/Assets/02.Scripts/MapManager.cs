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


    public List<GameObject> mapList = new List<GameObject>();
    public List<GameObject> canMakeMapList = new List<GameObject>(); //�����Ǵ� ���� ������ �����ϴ� ����Ʈ
    GameObject newBlock;

    public GameObject CreatMap(Vector2 mapPos , int numberOfNextBlock , GameObject thisMap) //���ο� ���� ������ִ� �Լ�
    {
        Debug.Log("���� ����ϴ�.");

        if (numberOfNextBlock != 0)
        {
            //���� ������������ �ִٸ� �Ȱ��� ���� ������
            numberOfNextBlock -= 1;
            newBlock = Instantiate(thisMap, mapPos, Quaternion.identity, transform);
            newBlock.GetComponent<Tile>().numberOfNextBlock = numberOfNextBlock;
            //mapList.Add(newBlock); //tile�� �����ɶ����� �ʸ���Ʈ�� ���������
            return newBlock;
        }

        for (int i = 0; i < canMakeMapList.Count; i++)
        {
            if (Random.Range(1, 101) < canMakeMapList[i].GetComponent<Tile>().tileCreatPercent)
            {
                //����Ȯ���� �����Ǹ� Ư������ ������

                newBlock = Instantiate(canMakeMapList[i], mapPos, Quaternion.identity, transform);
                mapList.Add(newBlock); //tile�� �����ɶ����� �ʸ���Ʈ�� ���������

                if (canMakeMapList[i].GetComponent<Tile>().specialTile)
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

        //�ƹ����� �������� �ʾҴٸ� �������� ����
        newBlock = Instantiate(canMakeMapList[0], mapPos, Quaternion.identity, transform);
        mapList.Add(newBlock); //tile�� �����ɶ����� �ʸ���Ʈ�� ���������

        return newBlock;
    }
}