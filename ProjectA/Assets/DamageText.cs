using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DamageText : MonoBehaviour
{
    public TextMeshProUGUI playerDamageText;
    public TextMeshProUGUI enemyDamageText;

    public void PlayerDamageTextFallDown(int damage)
    {
        playerDamageText.gameObject.SetActive(true);
        playerDamageText.text = damage.ToString();
        playerDamageText.GetComponent<Animator>().SetTrigger("DamageFall");
    }

    public void EnemyDamageTextFallDown(int damage)
    {
        enemyDamageText.gameObject.SetActive(true);
        enemyDamageText.text = damage.ToString();
        enemyDamageText.GetComponent<Animator>().SetTrigger("DamageFall");
    }

}
