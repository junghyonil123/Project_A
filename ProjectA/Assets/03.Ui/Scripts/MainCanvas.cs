using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvas : MonoBehaviour
{
    public GameObject playerActivePoint;
    public GameObject playerHp;
    private float playerActivePointPer;
    private float playerHpPer;

    #region singleton
    private static MainCanvas instance = null;

    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
            //DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public static MainCanvas Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    #endregion

    private void Update()
    {
        //플레이어의 행동력을 Ui형태로 보여줌
        playerActivePointPer = (float)Player.Instance.nowActivePoint / Player.Instance.maxActivePoint;
        playerActivePoint.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(0, 150 * playerActivePointPer);
        playerHpPer = (float)Player.Instance.nowHp / Player.Instance.maxHp;
        playerHp.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(0, 150 * playerHpPer);
    }
}
