using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    Weapon,
    Head,
    Glove,
    Armo,
    ConsumableItme,
    Material
}

public class Item : MonoBehaviour
{
    public string ItmeName;
    public Sprite ItemSprite;
    public ItemType itmeType;

    public int itemAt;
    public string itmeExplanation;

    private void Awake()
    {
        ItemSprite = GetComponent<SpriteRenderer>().sprite;
    }
}
