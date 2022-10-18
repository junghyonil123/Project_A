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

        path = Application.persistentDataPath + "/";
    }
    #endregion

    PlayerData playerData = new PlayerData();

    string path;
    string filename = "save";

    void Start()
    {
        //LoadData();
    }

    public void GetPlayerData()
    {
        playerData.name = Player.Instance.unitName;
        playerData.job = Player.Instance.job;

        playerData.lv = Player.Instance.lv;

        playerData.nowHp = Player.Instance.nowHp;
        playerData.maxHp = Player.Instance.maxHp;
        playerData.atk = Player.Instance.atk;
        playerData.def = Player.Instance.def;
        playerData.maxActivePoint = Player.Instance.maxActivePoint;
        playerData.nowActivePoint = Player.Instance.nowActivePoint;
        playerData.str = Player.Instance.str;
        playerData.dex = Player.Instance.dex;
        playerData.con = Player.Instance.con;
        playerData.statusPoint = Player.Instance.statusPoint;

        playerData.playerTransform = Player.Instance.nowStandingTile.position;
    }

    public void SetPlayerData()
    {
        Player.Instance.unitName = playerData.name;
        Player.Instance.job = playerData.job;

        Player.Instance.lv = playerData.lv;

        Player.Instance.nowHp = playerData.nowHp;
        Player.Instance.maxHp = playerData.maxHp;
        Player.Instance.atk = playerData.atk;
        Player.Instance.def = playerData.def;
        Player.Instance.maxActivePoint = playerData.maxActivePoint;
        Player.Instance.nowActivePoint = playerData.nowActivePoint;
        Player.Instance.str = playerData.str;
        Player.Instance.dex = playerData.dex;
        Player.Instance.con = playerData.con;
        Player.Instance.statusPoint = playerData.statusPoint;

        Player.Instance.transform.Translate(playerData.playerTransform);

        Player.Instance.PlayerPosionRay();
    }

    public void SaveData()
    {
        GetPlayerData();
        string data = JsonUtility.ToJson(playerData);
        File.WriteAllText(path + filename, data);
        Debug.Log(data);
    }

    public void LoadData()
    {
        if (File.Exists(path + filename))
        {
            string data = File.ReadAllText(path + filename);
            playerData = JsonUtility.FromJson<PlayerData>(data);
            print(data);
            SetPlayerData();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
