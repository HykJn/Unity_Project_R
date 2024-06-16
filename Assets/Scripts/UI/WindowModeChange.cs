using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WindowModeChange : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public TMP_Dropdown resDropdown;

    public void ChangeWindowMode()
    {
        switch(dropdown.value)
        {
            case 0:
                Screen.SetResolution(1920, 1080, FullScreenMode.ExclusiveFullScreen);
                resDropdown.gameObject.SetActive(false);
                break;
            case 1:
                Screen.SetResolution(1920, 1080, FullScreenMode.FullScreenWindow);
                resDropdown.gameObject.SetActive(false);
                break;
            case 2:
                Screen.fullScreenMode = FullScreenMode.Windowed;
                resDropdown.gameObject.SetActive(true);
                break;
        }
    }

    public void ChangeResolution()
    {
        switch(resDropdown.value)
        {
            case 0:
                Screen.SetResolution(2560, 1440, FullScreenMode.Windowed);
                break;
            case 1:
                Screen.SetResolution(1920, 1080, FullScreenMode.Windowed);
                break;
            case 2:
                Screen.SetResolution(1600, 900, FullScreenMode.Windowed);
                break;
            case 3:
                Screen.SetResolution(1280, 720, FullScreenMode.Windowed);
                break;
            case 4:
                Screen.SetResolution(960, 540, FullScreenMode.Windowed);
                break;
        }
    }
}
