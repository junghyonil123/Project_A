using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum InfoType
{
    moveCount,
    monsterKillCount,
}

public class GameManager : MonoBehaviour
{
    #region singleton
    private static GameManager instance = null;

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
    public static GameManager Instance
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

    public bool isNight = false;

    private void Start()
    {
        StartCoroutine("CheckPlayerActivePoint");
    }

    [SerializeField]
    private int _openedUiCount = 0;

    public int openedUiCount
    {
        get { return _openedUiCount; }
    }

    public void AddUiCount(int amount)
    {
        _openedUiCount += amount;

        //if (_openedUiCount > 0)
        //{
        //    Time.timeScale = 0;
        //}
        //else
        //{
        //    Time.timeScale = 1;
        //}
    }

    [SerializeField]
    private GameObject gameOverCanvas;

    public void GameOver()
    {
        gameOverCanvas.SetActive(true);
    }

    #region SetDay

    [SerializeField]
    private SpriteRenderer nightSprite;

    public void CheckPlayerActivePoint()
    {
        if (Player.Instance.nowActivePoint <= 0)
        {
            if (isNight)
            {
                Be_A_Morning();
            }
            else if (!isNight)
            {
                Be_A_Night();
            }
        }

    }

    Tile[] maps;

    private void SpawnNightMonster()
    {
        maps = MapManager.Instance.transform.GetComponentsInChildren<Tile>();

        for (int i = 0; i < maps.Length; i++)
        {
            maps[i].SendMessage("NightMonsterSpawn");
        }
    }

    public void Be_A_Night()
    {
        isNight = true;
        Player.Instance.nowActivePoint = Player.Instance.maxActivePoint;
        StartCoroutine(ItsNight());
    }

    public void Be_A_Morning()
    {
        isNight = false;
        Player.Instance.nowActivePoint = Player.Instance.maxActivePoint;
        StartCoroutine(ItsMorning());
    }

    WaitForSeconds nightTransferTime = new WaitForSeconds(0.01f);

    IEnumerator ItsNight()
    {
        for (int i = 0; i <= 200; i++)
        {
            nightSprite.color = new Color(nightSprite.color.r, nightSprite.color.g, nightSprite.color.b, i / 200f);
            yield return nightTransferTime;
        }
        SpawnNightMonster();
    }// sprite의 알파값을 조정해 2초에 걸쳐 밤으로 만들어 주는 코루틴

    IEnumerator ItsMorning()
    {
        for (int i = 200; i >= 0; i--)
        {
            nightSprite.color = new Color(nightSprite.color.r, nightSprite.color.g, nightSprite.color.b, i / 200f);
            yield return nightTransferTime;
        }
    }// sprite의 알파값을 조정해 2초에 걸쳐 낮으로 만들어 주는 코루틴

    #endregion

    #region PlayerInfo

    public void getInfo(InfoType infoType, int value)
    {
        switch (infoType)
        {
            case InfoType.moveCount:
                _moveCount += value;
                break;
            case InfoType.monsterKillCount:
                _monsterKillCount += value;
                break;
            default:
                break;
        }

        SkillManager.Instance.CheckSkillUnlock(); //정보 저장이 끝나면 스킬이 개방 되었나확인
    }

    private int _moveCount;
    public int moveCount { get { return _moveCount; } }

    private int _monsterKillCount;
    public int monsterKillCount { get { return _monsterKillCount; } }
    
    #endregion
}
