using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishBattleCanvas_Par : MonoBehaviour
{
    private void Start()
    {
        BattleManager.Instance.finishCanvas = this.gameObject;
    }
}
