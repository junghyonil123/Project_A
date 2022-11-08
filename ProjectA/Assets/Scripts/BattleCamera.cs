using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCamera : MonoBehaviour
{
    public Camera _camera;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    void Update()
    {
        if (BattleManager.Instance.isBattle)
        {
            _camera.depth = 2;
        }
        else
        {
            _camera.depth = -1;
        }
    }
}
