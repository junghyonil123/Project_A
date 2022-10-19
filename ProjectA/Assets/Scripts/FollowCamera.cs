using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    private void Update()
    {
        transform.position = Player.Instance.transform.position + new Vector3(0, 0, -1);
    }

}
