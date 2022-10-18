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
    public List<Vector3> mapTransformList = new List<Vector3>();
    public List<int> mapTypeList = new List<int>();
}

public class DataManager : MonoBehaviour
{
    #region Singleton
    private static DataManager instance;
    public static DataManager Instance
    {
        get
        {
            if (instance == null) //instance 가 존재하지않는다면
            {
                var obj = FindObjectOfType<DataManager>(); //Player 타입이 존재하는지 확인
                if (obj != null)
                {
                    instance = obj; //null이 아니라면 instance에 넣어줌
                }
                else
                {
                    var newPlayer = new GameObject("DataManager").AddComponent<DataManager>(); //null이라면 새로만들어줌
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
        //생성과 동시에 실행되는 Awake는 이미 생성되어있는 싱글톤 오브젝트가 있는지 검사하고 있다면 지금 생성된 오브젝트를 파괴

        //var objs = FindObjectsOfType<Player>();
        //if (objs.Length != 1)
        //{
        //    Destroy(gameObject);
        //    return;
        //}

        DontDestroyOnLoad(gameObject); //씬을 전환할때 파괴되는것을 막음

        playerDataPath = Application.persistentDataPath + "/" + "PlayerData";
        mapDataPath = Application.persistentDataPath + "/" + "mapData";
    }
    #endregion


    void Start()
    {
        //LoadData();
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

        playerDataClass.playerTransform = Player.Instance.nowStandingTile.position;
    }

    public void SetPlayerData()
    {
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

    #endregion

    #region mapData

    MapData mapDataClass = new MapData();
    string mapDataPath;

    public void GetMapData()
    {
        for (int i = 0; i < MapManager.Instance.mapList.Count; i++)
        {
            mapDataClass.mapTransformList.Add(MapManager.Instance.mapList[i].transform.position);
            //mapDataClass.mapTypeList.Add(MapManager.Instance.mapList[i].GetComponent<Tile>); 맵타입추가하기
        }

    }

    #endregion

    public void SaveData()
    {
        GetPlayerData();
        string playerData = JsonUtility.ToJson(playerDataClass);
        File.WriteAllText(playerDataPath, playerData);
        Debug.Log(playerData);
    }

    public void LoadData()
    {
        if (File.Exists(playerDataPath))
        {
            string playerData = File.ReadAllText(playerDataPath);
            playerDataClass = JsonUtility.FromJson<PlayerData>(playerData);
            print(playerData);
            SetPlayerData();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
