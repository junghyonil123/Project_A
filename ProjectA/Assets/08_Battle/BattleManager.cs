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
            if (instance == null) //instance �� ���������ʴ´ٸ�
            {
                var obj = FindObjectOfType<BattleManager>(); //Player Ÿ���� �����ϴ��� Ȯ��
                if (obj != null)
                {
                    instance = obj; //null�� �ƴ϶�� instance�� �־���
                }
                else
                {
                    var newBattleManager = new GameObject("BattleManager").AddComponent<BattleManager>(); //null�̶�� ���θ������
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
        ////������ ���ÿ� ����Ǵ� Awake�� �̹� �����Ǿ��ִ� �̱��� ������Ʈ�� �ִ��� �˻��ϰ� �ִٸ� ���� ������ ������Ʈ�� �ı�

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

        enemy = _enemy; //���� ���� ����

        battleCanvas.SetActive(true);

        ProfilSetting();

        GameManager.Instance.AddUiCount(1);

        StartCoroutine("Battle");
    }

    private void ProfilSetting()
    {
        playerPortrait.sprite = player.portrait; //�ʻ�ȭ ����
        enemyPortrait.sprite = enemy.portrait;

        battlePlayer.transform.GetChild(0).GetComponent<Image>().sprite = player.GetComponent<SpriteRenderer>().sprite; //�����̴� �н� ����
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
                //Player�� Enemy�� ������ ������
                battlePlayer.GetComponent<RectTransform>().Translate(new Vector2(300 / count, 0));
                battleEnemy.GetComponent<RectTransform>().Translate(new Vector2(-300 / count, 0));

                yield return null;
            }

            PlayerAttack(); //�÷��̾� ����

            if (CheckDie(enemy)) { yield break; } //���Ͱ� �׾����� Ȯ��

            EnemyAttack(); //���� ����

            if (CheckDie(player)) { yield break; } //�÷��̾ �׾����� Ȯ��

            for (int i = 0; i < count; i++)
            {
                //Player�� Enemy�� �ڷ� ������
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

    } //�÷��̾��� ����

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

    } //������ ����

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
    } //_unit�� �׾����� Ȯ��

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

    } //������ �����Ŵ

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
    } //������ �÷��̾�� ������ hp�ٸ� ������Ʈ����

    public void ShowDropItemToFinishCanvas(Item _item) //��ӵ� �������� FinishCanvas�� ������
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
        foreach (var slot in finishCanvasSlotList) //�κ��丮�� ��� �������� �������
        {
            if (slot.item != null)
            {
                Debug.Log(slot.item);
                Player.Instance.GetItem(slot.item);
            }
        }

        foreach (var slot in finishCanvasSlotList) //BattelCanvas�� Slot�� �ִ� �����۵��� ��� ������
        {
            if (slot.item != null)
            {
                slot.DeleteItem();
            }
        }

    } //FinishCanvas�� �������� �÷��̾� �κ��丮�� �־���
}
    