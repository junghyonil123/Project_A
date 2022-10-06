using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvas : MonoBehaviour
{
    public GameObject playerActivePoint;
    private float playerActivePointPer;

    private void Update()
    {
        //플레이어의 행동력을 Ui형태로 보여줌
        playerActivePointPer = Player.Instance.nowActivePoint / Player.Instance.maxActivePoint;
        playerActivePoint.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(0, 150 * playerActivePointPer); 
    }
}
