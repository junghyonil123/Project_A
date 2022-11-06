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
    }

    public override void Die()
    {
        base.Die();
        Drop();
        Destroy(gameObject);
    }

    public void Drop()
    {
        BattleCanvas.Instance.xpTextMesh.text = ""+xp;
        BattleCanvas.Instance.goldTextMesh.text = ""+gold;

        for (int i = 0; i < dropItem.Count; i++)
        {
            if (dropPer[i] >= Random.Range(1, 101))
            {
                BattleCanvas.Instance.MonsterItemDrop(dropItem[i]);
                Inventory.Instance.GetItem(dropItem[i]);
            }
        }
    }

}
