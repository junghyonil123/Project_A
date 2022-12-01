using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{ 
    #region singleton
    private static GameManager instance = null;

    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public static GameManager Instance
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

    public bool isNight = false;

    private void Start()
    {
        StartCoroutine("CheckPlayerActivePoint");
    }

    [SerializeField]
    private SpriteRenderer nightSprite;
    

    [SerializeField]
    private GameObject gameOverCanvas;

    public void GameOver()
    {
        gameOverCanvas.SetActive(true);
    }

    IEnumerator CheckPlayerActivePoint()
    {
        while (true)
        {
            if (Player.Instance.nowActivePoint <= 0)
            {
                if (isNight)
                {
                    Be_A_Morning();
                }
                else if (!isNight)
                {
                    Be_A_Night();
                }
            }
            yield return null;
        }
        
    }

    public void Be_A_Night()
    {
        isNight = true;
        Player.Instance.nowActivePoint = Player.Instance.maxActivePoint;
        StartCoroutine(ItsNight());
    }

    public void Be_A_Morning()
    {
        isNight = false;
        Player.Instance.nowActivePoint = Player.Instance.maxActivePoint;
        StartCoroutine(ItsMorning());
    }

    WaitForSeconds nightTransferTime = new WaitForSeconds(0.01f);

    IEnumerator ItsNight()
    {
        for (int i = 0; i <= 200; i++)
        {
            Debug.Log("itsNoght!");
            nightSprite.color = new Color(nightSprite.color.r, nightSprite.color.g, nightSprite.color.b, i/200f);
            yield return nightTransferTime;
        }
    }// sprite의 알파값을 조정해 2초에 걸쳐 밤으로 만들어 주는 코루틴

    IEnumerator ItsMorning()
    {
        for (int i = 200; i >= 0; i--)
        {
            Debug.Log("itsMorning!" + i);
            nightSprite.color = new Color(nightSprite.color.r, nightSprite.color.g, nightSprite.color.b, i/200f);
            yield return nightTransferTime;
        }
    }// sprite의 알파값을 조정해 2초에 걸쳐 낮으로 만들어 주는 코루틴

}
