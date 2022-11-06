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
    public int lv;

    public bool isDie = false;

    virtual public void Start()
    {
        nowHp = maxHp;
    }

    virtual public void GetDamage(float damage)
    {
        if(damage - def <= 1)
        {
            nowHp--;
        }
        else
        {
          nowHp = nowHp - (damage - def);
        }

        if(nowHp < 0)
        {
            nowHp = 0;
            Die();
<<<<<<< Updated upstream
=======
            PlayerBattle.Instance.isBattle = false;
>>>>>>> Stashed changes
        }
    }

    virtual public void Die()
    {
        if(nowHp <= 0)
        {
<<<<<<< Updated upstream
            BattleManager.Instance.FinishCanvasOn();
            Time.timeScale = 0;
=======
>>>>>>> Stashed changes
            isDie = true;
        }
    }
}
