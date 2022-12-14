using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public Image playerPortrait;
    public Image enemyPortrait;

    private Player player;
    private Enemy enemy;

    public GameObject battlePlayer;
    public Vector3 battlePlayerStartPos;
    public GameObject battleEnemy;
    public Vector3 battleEnemyStartPos;

    public GameObject battleCanvas;
    public GameObject finishBattleCanvas;

    public GameObject playerBattleHpBar;
    public GameObject EnemyBattleHpBar;

    public DamageText damageText;

    public List<Slot> finishCanvasSlotList = new List<Slot>();

    public bool isBattle;

    #region Singleton
    private static BattleManager instance;
    public static BattleManager Instance
    {
        get
        {
            if (instance == null) //instance 가 존재하지않는다면
            {
                var obj = FindObjectOfType<BattleManager>(); //Player 타입이 존재하는지 확인
                if (obj != null)
                {
                    instance = obj; //null이 아니라면 instance에 넣어줌
                }
                else
                {
                    var newBattleManager = new GameObject("BattleManager").AddComponent<BattleManager>(); //null이라면 새로만들어줌
                    instance = newBattleManager;
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
        ////생성과 동시에 실행되는 Awake는 이미 생성되어있는 싱글톤 오브젝트가 있는지 검사하고 있다면 지금 생성된 오브젝트를 파괴

        var objs = FindObjectsOfType<BattleManager>();

        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }

    }
    #endregion
   
    private void Start()
    {
        player = Player.Instance;
        battlePlayerStartPos = battlePlayer.GetComponent<RectTransform>().position;
        battleEnemyStartPos = battleEnemy.GetComponent<RectTransform>().position;

    }

    public void BattelStart(Enemy _enemy)
    {
        isBattle = true;

        enemy = _enemy; //몬스터 정보 저장

        battleCanvas.SetActive(true);

        ProfilSetting();

        GameManager.Instance.AddUiCount(1);

        StartCoroutine("Battle");
    }

    private void ProfilSetting()
    {
        playerPortrait.sprite = player.portrait; //초상화 세팅
        enemyPortrait.sprite = enemy.portrait;

        battlePlayer.transform.GetChild(0).GetComponent<Image>().sprite = player.GetComponent<SpriteRenderer>().sprite; //움직이는 분신 세팅
        battleEnemy.transform.GetChild(0).GetComponent<Image>().sprite = enemy.GetComponent<SpriteRenderer>().sprite;
        battleEnemy.transform.GetChild(0).GetComponent<Image>().color = enemy.GetComponent<SpriteRenderer>().color;
    }

    IEnumerator Battle()
    {
        while (true)
        {
            float count = 100;

            for (int i = 0; i < count; i++)
            {
                //Player와 Enemy를 앞으로 움직임
                battlePlayer.GetComponent<RectTransform>().Translate(new Vector2(300 / count, 0));
                battleEnemy.GetComponent<RectTransform>().Translate(new Vector2(-300 / count, 0));

                yield return null;
            }

            PlayerAttack(); //플레이어 공격

            if (CheckDie(enemy)) { yield break; } //몬스터가 죽었는지 확인

            EnemyAttack(); //몬스터 공격

            if (CheckDie(player)) { yield break; } //플레이어가 죽었는지 확인

            for (int i = 0; i < count; i++)
            {
                //Player와 Enemy를 뒤로 움직임
                battlePlayer.GetComponent<RectTransform>().Translate(new Vector2(-300 / count, 0));
                battleEnemy.GetComponent<RectTransform>().Translate(new Vector2(300 / count, 0));

                yield return null;
            }
        }
    }

    private void PlayerAttack()
    {
        int damage = player.GetPlayerAtk() - enemy.def;


        if (damage <= 0)
        {
            damage = 1;
        }

        enemy.nowHp -= damage;

        damageText.PlayerDamageTextFallDown(damage);

        SetBattleHpBar();

    } //플레이어의 공격

    private void EnemyAttack()
    {
        int damage = enemy.atk - player.def;

        if (damage <= 0)
        {
            damage = 1;
        }

        player.nowHp -= damage;

        damageText.EnemyDamageTextFallDown(damage);

        SetBattleHpBar();

    } //몬스터의 공격

    private bool CheckDie(Unit _unit)
    {
        if (_unit.nowHp <= 0)
        {
            _unit.nowHp = 0;
            _unit.Die();
            StartCoroutine(BattleFinish());
            return true;
        }
        return false;
    } //_unit이 죽었는지 확인

    IEnumerator BattleFinish()
    {
        finishBattleCanvas.SetActive(true);

        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isBattle = false;
                GameManager.Instance.getInfo(InfoType.monsterKillCount , 1);

                GiveDropItemToPlayer();
                ResetBattleCanvas();
                battleCanvas.SetActive(false);
                GameManager.Instance.AddUiCount(-1);
                yield break;
            }

            yield return null;
        }

    } //전투를 종료시킴

    void ResetBattleCanvas()
    {
        finishBattleCanvas.SetActive(false);
        battlePlayer.GetComponent<RectTransform>().position = battlePlayerStartPos;
        battleEnemy.GetComponent<RectTransform>().position = battleEnemyStartPos;
    }

    private float battlePlayerHpPer;
    private float battleEnemyHpPer;

    private void SetBattleHpBar()
    {
        battlePlayerHpPer = (float)player.nowHp / player.maxHp;
        playerBattleHpBar.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(0, 300 * battlePlayerHpPer);
        battleEnemyHpPer = (float)enemy.nowHp / enemy.maxHp;
        EnemyBattleHpBar.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(0, 300 * battleEnemyHpPer);
    } //전투중 플레이어와 몬스터의 hp바를 업데이트해줌

    public void ShowDropItemToFinishCanvas(Item _item) //드롭된 아이템을 FinishCanvas에 보여줌
    {
        foreach (Slot slot in finishCanvasSlotList)
        {
            if (slot.item == null)
            {
                slot.SetItem(_item);
                break;
            }
        }
    }
    
    public void GiveDropItemToPlayer()
    {
        foreach (var slot in finishCanvasSlotList) //인벤토리에 모든 아이템을 집어넣음
        {
            if (slot.item != null)
            {
                Debug.Log(slot.item);
                Player.Instance.GetItem(slot.item);
            }
        }

        foreach (var slot in finishCanvasSlotList) //BattelCanvas의 Slot에 있는 아이템들은 모두 삭제함
        {
            if (slot.item != null)
            {
                slot.DeleteItem();
            }
        }

    } //FinishCanvas의 아이템을 플레이어 인벤토리로 넣어줌
}
    