using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusCanvas : MonoBehaviour
{
    public GameObject profileWindow;

    public void OnOffStatusWindow()
    {
        profileWindow.SetActive(!profileWindow.activeSelf);
    }
}
