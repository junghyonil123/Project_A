using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCamera : MonoBehaviour
{
    public Camera camera;

    private void Awake()
    {
        camera = GetComponent<Camera>();
    }

    void Update()
    {
        if (BattleManager.Instance.isBattle)
        {
            camera.depth = 2;
        }
        else
        {
            camera.depth = -1;
        }
    }
}
