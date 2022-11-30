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

    public GameObject battelPlayer;
    public GameObject battelEnemy;

    public GameObject battleCanvas;
    public GameObject finishBattleCanvas;

    public GameObject playerBattleHpBar;
    public GameObject EnemyBattleHpBar;

    public List<Slot> finishCanvasSlotList = new List<Slot>();

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
    }

    public void BattelStart(Enemy _enemy)
    {
        enemy = _enemy; //몬스터 정보 저장

        battleCanvas.SetActive(true);

        ProfilSetting();

        StartCoroutine("Battle");
    }

    private void ProfilSetting()
    {
        playerPortrait.sprite = player.portrait; //초상화 세팅
        enemyPortrait.sprite = enemy.portrait;

        battelPlayer.transform.GetChild(0).GetComponent<Image>().sprite = player.GetComponent<SpriteRenderer>().sprite; //움직이는 분신 세팅
        battelEnemy.transform.GetChild(0).GetComponent<Image>().sprite = enemy.GetComponent<SpriteRenderer>().sprite;
    }

    IEnumerator Battle()
    {
        while (true)
        {
            float count = 100;

            for (int i = 0; i < count; i++)
            {
                //Player와 Enemy를 앞으로 움직임
                battelPlayer.GetComponent<RectTransform>().Translate(new Vector2(300 / count, 0));
                battelEnemy.GetComponent<RectTransform>().Translate(new Vector2(-300 / count, 0));

                yield return null;
            }

            PlayerAttack(); //플레이어 공격

            if (CheckDie(enemy)) { yield break; } //몬스터가 죽었는지 확인

            EnemyAttack(); //몬스터 공격

            if (CheckDie(player)) { yield break; } //플레이어가 죽었는지 확인

            for (int i = 0; i < count; i++)
            {
                //Player와 Enemy를 뒤로 움직임
                battelPlayer.GetComponent<RectTransform>().Translate(new Vector2(-300 / count, 0));
                battelEnemy.GetComponent<RectTransform>().Translate(new Vector2(300 / count, 0));

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
                GiveDropItemToPlayer();
                battleCanvas.SetActive(false);
                yield break;
            }

            yield return null;
        }

    } //전투를 종료시킴

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
        foreach (var slot in finishCanvasSlotList)
        {
            if (slot.item != null)
            {
                Debug.Log(slot.item);
                Player.Instance.GetItem(slot.item);
            }
        }
    } //FinishCanvas의 아이템을 플레이어 인벤토리로 넣어줌
}
    