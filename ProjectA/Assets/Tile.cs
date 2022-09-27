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
            // ���� ���� �ٸ����� �ִٸ� �װ��� topMap�� ������
            topMap = hitInfo.transform.gameObject;
        }
        else
        {
            Debug.Log("���̾ȵſ�" + gameObject.name);
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
