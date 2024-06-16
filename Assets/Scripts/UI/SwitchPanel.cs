using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPanel : MonoBehaviour
{
    public GameObject panel;
    public bool pauseWhenClick = false;

    public void SwitchingPanel()
    {
        panel.SetActive(!panel.activeSelf);
        if (pauseWhenClick)
        {
            if (Time.timeScale != 1) Time.timeScale = 1;
            else Time.timeScale = 0;
        }
    }
}
