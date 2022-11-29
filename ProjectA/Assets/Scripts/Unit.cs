using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public abstract class Unit : MonoBehaviour
{
    public string unitName;
    public int nowHp;
    public int maxHp;
    public int atk;
    public int def;

    public Sprite portrait;

    abstract public void Die();

}
