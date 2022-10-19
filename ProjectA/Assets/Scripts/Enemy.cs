using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : Unit
{
    public SpriteRenderer spriteRenderer;

    public int xp;
    public int gold;
    public int number;

    public List<Item> dropItem = new List<Item>();
    public List<float> dropPer = new List<float>();
    public List<int> dropMin = new List<int>();
    public List<int> dropMax = new List<int>();

    public bool isBattle = false;

    private void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        BattleGetDamage();
        Die();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            BattleManager.Instance.enemy = gameObject;
            DontDestroyOnLoad(this);
            spriteRenderer.sortingOrder = -2;
            BattleManager.Instance.player.GetComponent<SpriteRenderer>().sortingOrder = -2;
            SceneManager.LoadScene("BattleScene");
        }
    }

    public void BattleGetDamage()
    {
        if (isBattle)
        {
            Debug.Log("Battle2");
            GetDamage(BattleManager.Instance.player.GetComponent<Player>().atk);
            isBattle = false;
        }
    }

    public override void Die()
    {
        base.Die();
        if(nowHp <= 0)
        {
            Drop();
            GameObject.Find("EnemyBattle").SetActive(false);
        }
    }

    public void Drop()
    {
        Debug.Log("!");
        FinishCanvas.Instance.xpTextMesh.text = " "+xp;
        FinishCanvas.Instance.goldTextMesh.text = " "+gold;

        for (int i = 0; i < dropItem.Count; i++)
        {
            Debug.Log("아이템을 드롭합니다.");
            if (dropPer[i] < Random.Range(1,101))
            {
                BattleManager.Instance.monsterItemDrop(dropItem[i]);
                Inventory.Instance.GetItem(dropItem[i]);
            }
        }
    }
}
