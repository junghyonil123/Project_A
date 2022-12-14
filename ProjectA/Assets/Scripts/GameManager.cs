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

    public bool isOpenBox = false;

    private Slot _nowSelectSlot;

    public Slot nowSelectSlot
    {
        get { return _nowSelectSlot; }

        set { 
                _lastSelectSlot = _nowSelectSlot;
                _nowSelectSlot = value;
            } 
    }

    private Slot _lastSelectSlot;

    public Slot lastSelectSlot
    {
        get
        {
            if (_lastSelectSlot == null)
            {
                return new Slot();
            }
            else
            {
                return _lastSelectSlot;
            } 
        }

        set
        {
            _lastSelectSlot = value;
        }
    }

    private Box _lastOpendBox;

    public Box lastOpendBox
    {
        //get { }
        set { _lastOpendBox = value; }
    }

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
            if (BattleManager.Instance.isBattle && _openedUiCount <= 0)
            {
                yield return nightTransferTime;
                i--;
                continue;
            }
            nightSprite.color = new Color(nightSprite.color.r, nightSprite.color.g, nightSprite.color.b, i / 200f);
            yield return nightTransferTime;
        }
        SpawnNightMonster();
    }// sprite?? ???????? ?????? 2???? ???? ?????? ?????? ???? ??????

    IEnumerator ItsMorning()
    {
        for (int i = 200; i >= 0; i--)
        {
            if (BattleManager.Instance.isBattle && _openedUiCount <= 0)
            {
                yield return nightTransferTime;
                i++;
                continue;
            }
            nightSprite.color = new Color(nightSprite.color.r, nightSprite.color.g, nightSprite.color.b, i / 200f);
            yield return nightTransferTime;
        }
    }// sprite?? ???????? ?????? 2???? ???? ?????? ?????? ???? ??????

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

        if (!BattleManager.Instance.isBattle)
        {
           SkillManager.Instance.CheckSkillUnlock(); //???? ?????? ?????? ?????? ???? ??????????
        }

    }

    private int _moveCount;
    public int moveCount { get { return _moveCount; } }

    private int _monsterKillCount;
    public int monsterKillCount { get { return _monsterKillCount; } }
    
    #endregion
}
