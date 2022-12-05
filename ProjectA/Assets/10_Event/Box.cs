using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Box : MonoBehaviour
{
    public Sprite openSprite;

    public void OpenBox()
    {
        GetComponent<SpriteRenderer>().sprite = openSprite;
    }
}
