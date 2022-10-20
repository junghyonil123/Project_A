using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerData
{
    public string name;
    public string job;
    
    public int lv;
    
    public float nowHp;
    public float maxHp;
    public float atk;
    public float def;
    public float maxActivePoint;
    public float nowActivePoint;
    public float str;
    public float dex;
    public float con;
    public float statusPoint;
    
    public Vector3 playerTransform;

}

public class MapData
{
    public List<Vector3> tileTransformList = new List<Vector3>();
    public List<int> tileTypeList = new List<int>();
}

public class DataManager : MonoBehaviour
{
    #region Singleton
    private static DataManager instance;
    public static DataManager Instance
    {
        get
        {
            if (instance == null) //instance �� ���������ʴ´ٸ�
            {
                var obj = FindObjectOfType<DataManager>(); //Player Ÿ���� �����ϴ��� Ȯ��
                if (obj != null)
                {
                    instance = obj; //null�� �ƴ϶�� instance�� �־���
                }
                else
                {
                    var newPlayer = new GameObject("DataManager").AddComponent<DataManager>(); //null�̶�� ���θ������
                    instance = newPlayer;
                }
            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }

    private void Awake()
    {
        //������ ���ÿ� ����Ǵ� Awake�� �̹� �����Ǿ��ִ� �̱��� ������Ʈ�� �ִ��� �˻��ϰ� �ִٸ� ���� ������ ������Ʈ�� �ı�

        var objs = FindObjectsOfType<Player>();
        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject); //���� ��ȯ�Ҷ� �ı��Ǵ°��� ����

        playerDataPath = Application.persistentDataPath + "/" + "PlayerData";
        mapDataPath = Application.persistentDataPath + "/" + "mapData";
    }
    #endregion

    public bool isLoadFinish = false; //�ε��� ������ true���Ǵ� �÷���

    void Start()
    {
        isLoadFinish = false;
        LoadData();
    }

    #region playerData

    PlayerData playerDataClass = new PlayerData();
    string playerDataPath;

    public void GetPlayerData()
    {
        playerDataClass.name = Player.Instance.unitName;
        playerDataClass.job = Player.Instance.job;

        playerDataClass.lv = Player.Instance.lv;

        playerDataClass.nowHp = Player.Instance.nowHp;
        playerDataClass.maxHp = Player.Instance.maxHp;
        playerDataClass.atk = Player.Instance.atk;
        playerDataClass.def = Player.Instance.def;
        playerDataClass.maxActivePoint = Player.Instance.maxActivePoint;
        playerDataClass.nowActivePoint = Player.Instance.nowActivePoint;
        playerDataClass.str = Player.Instance.str;
        playerDataClass.dex = Player.Instance.dex;
        playerDataClass.con = Player.Instance.con;
        playerDataClass.statusPoint = Player.Instance.statusPoint;

        playerDataClass.playerTransform = Player.Instance.transform.position;

        string playerData = JsonUtility.ToJson(playerDataClass);
        File.WriteAllText(playerDataPath, playerData);
    }

    public void SetPlayerData()
    {
        if (File.Exists(playerDataPath))
        {
            string playerData = File.ReadAllText(playerDataPath);
            playerDataClass = JsonUtility.FromJson<PlayerData>(playerData);

            Player.Instance.unitName = playerDataClass.name;
            Player.Instance.job = playerDataClass.job;

            Player.Instance.lv = playerDataClass.lv;

            Player.Instance.nowHp = playerDataClass.nowHp;
            Player.Instance.maxHp = playerDataClass.maxHp;
            Player.Instance.atk = playerDataClass.atk;
            Player.Instance.def = playerDataClass.def;
            Player.Instance.maxActivePoint = playerDataClass.maxActivePoint;
            Player.Instance.nowActivePoint = playerDataClass.nowActivePoint;
            Player.Instance.str = playerDataClass.str;
            Player.Instance.dex = playerDataClass.dex;
            Player.Instance.con = playerDataClass.con;
            Player.Instance.statusPoint = playerDataClass.statusPoint;

            Player.Instance.transform.Translate(playerDataClass.playerTransform);

            Player.Instance.PlayerPosionRay();
        }
    }

    #endregion

    #region mapData

    MapData mapDataClass = new MapData();
    string mapDataPath;

    public void GetMapData()
    {
        for (int i = 0; i < MapManager.Instance.mapList.Count; i++)
        {
            mapDataClass.tileTransformList.Add(MapManager.Instance.mapList[i].transform.position);
            mapDataClass.tileTypeList.Add(MapManager.Instance.mapList[i].GetComponent<Tile>().tileType); 
        }

        string mapData = JsonUtility.ToJson(mapDataClass); //�� �����͸� ���̽������� ���ڿ��� ��ȯ
        File.WriteAllText(mapDataPath, mapData); //��ȯ�� ���ڿ��� mapDataPath��ο� ����
    }

    public void SetMapData()
    {
        if (File.Exists(mapDataPath))
        {
            string mapData = File.ReadAllText(mapDataPath);
            mapDataClass = JsonUtility.FromJson<MapData>(mapData);

            for (int i = 0; i < mapDataClass.tileTransformList.Count; i++)
            {
                MapManager.Instance.mapList.Add(Instantiate(MapManager.Instance.canMakeMapList[mapDataClass.tileTypeList[i]], mapDataClass.tileTransformList[i], Quaternion.identity, MapManager.Instance.transform));  
                //���� �����Ѵ� (������ �� Ÿ��, ���� ���������ִ� ��ġ, ���� ȸ��, MapManager�� �ڽ����� �־���)
            }
        }
    }

    #endregion

    public void SaveData()
    {
        GetMapData();
        GetPlayerData();
        DataClear();
    }

    public void LoadData()
    {
        SetMapData();
        SetPlayerData();
        DataClear();
        isLoadFinish = true;
    }

    public void DataClear()
    {
        playerDataClass = new PlayerData();
        mapDataClass = new MapData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
