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

    private void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Die();
    }
    public override void Die()
    {
        base.Die();
        if(nowHp <= 0)
        {
            Debug.Log("die" + gameObject);
            Drop();
            Destroy(gameObject);
        }

    }

    public void Drop()
    {
        FinishCanvas.Instance.xpTextMesh.text = ""+xp;
        FinishCanvas.Instance.goldTextMesh.text = ""+gold;

        for (int i = 0; i < dropItem.Count; i++)
        {
            if (dropPer[i] >= Random.Range(1, 101))
            {
                Debug.Log("µÎ¹ø ½ÇÇàµÊ" + dropItem[i]);
                BattleManager.Instance.monsterItemDrop(dropItem[i]);
                Inventory.Instance.GetItem(dropItem[i]);
            }
        }
    }


}
