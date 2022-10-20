using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    private void Update()
    {
        if (!Player.Instance.isFight)
        {
            transform.position = Player.Instance.transform.position + new Vector3(0, 0, -1);
        }
        else
        {
            transform.position = new Vector3(0, 0, 30);
        }
    }

}
