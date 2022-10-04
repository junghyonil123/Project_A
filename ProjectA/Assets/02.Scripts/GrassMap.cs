using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassMap : MonoBehaviour
{
    public List<GameObject> environmentList = new List<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < environmentList.Count; i++)
        {
            if (Random.Range(0,9) < 1)
            {
                Instantiate(environmentList[i], transform.position + new Vector3(Random.Range(-1f,1f), Random.Range(-1f, 1f), 0), transform.rotation);
                break;
            }
        }
    }
}
