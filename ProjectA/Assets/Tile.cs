using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject topMap;
    public GameObject bottomMap;
    public GameObject leftMap;
    public GameObject rightMap;

    private void Awake()
    {
        RaycastHit2D hitInfo;
        hitInfo = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 3), this.transform.up, 0.3f);

        if (hitInfo)
        {
            // 맵의 위에 다른맵이 있다면 그것을 topMap에 저장함
            topMap = hitInfo.transform.gameObject;
        }
        else
        {
            Debug.Log("레이안돼염" + gameObject.name);
            Debug.Log(new Vector2(transform.position.x, transform.position.y + 3));

            MapManager.Instance.CreatMap(new Vector2(transform.position.x, transform.position.y + 3));
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y + 3), this.transform.up * 0.3f, Color.green);
    }
}
