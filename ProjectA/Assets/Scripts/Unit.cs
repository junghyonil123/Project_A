using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Unit : MonoBehaviour
{
    public string unitName;
    public float nowHp;
    public float maxHp;
    public float atk;
    public float def;

    [HideInInspector] public bool isDie = false;

    virtual public void Start()
    {
    }


}
