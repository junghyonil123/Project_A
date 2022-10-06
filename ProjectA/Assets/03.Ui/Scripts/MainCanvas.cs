using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvas : MonoBehaviour
{
    public GameObject playerActivePoint;
    private float playerActivePointPer;

    private void Update()
    {
        //�÷��̾��� �ൿ���� Ui���·� ������
        playerActivePointPer = Player.Instance.nowActivePoint / Player.Instance.maxActivePoint;
        playerActivePoint.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(0, 150 * playerActivePointPer); 
    }
}
