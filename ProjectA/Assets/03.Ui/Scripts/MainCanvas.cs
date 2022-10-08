using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvas : MonoBehaviour
{
    public GameObject playerActivePoint;
    public GameObject playerHp;
    private float playerActivePointPer;
    private float playerHpPer;

    private void Update()
    {
        //�÷��̾��� �ൿ���� Ui���·� ������
        playerActivePointPer = Player.Instance.nowActivePoint / Player.Instance.maxActivePoint;
        playerActivePoint.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(0, 150 * playerActivePointPer);
        playerHpPer = Player.Instance.nowHp / Player.Instance.maxHp;
        playerHp.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(0, 150 * playerHpPer);
    }
}
