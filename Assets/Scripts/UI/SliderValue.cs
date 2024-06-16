using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderValue : MonoBehaviour
{
    public enum SliderType
    {
        MainVolume, BGMVolume, SFXVolume, Normal
    }
    public Slider slider;
    public TextMeshProUGUI text;
    public SliderType type;

    public void Awake()
    {
        InitVolumeSlider();
    }

    public void InitVolumeSlider()
    {
        if (type == SliderType.Normal) return;
        switch (type)
        {
            case SliderType.MainVolume:
                slider.value = SoundManager.instance.mainVolume;
                break;
            case SliderType.BGMVolume:
                slider.value = SoundManager.instance.bgmVolume;
                break;
            case SliderType.SFXVolume:
                slider.value = SoundManager.instance.sfxVolume;
                break;
        }
    }
    public void UpdateValue()
    {
        text.text = ((int)(slider.value * 100)).ToString() + "%";
    }

    public void SetMainVolume()
    {
        SoundManager.instance.mainVolume = slider.value;
    }

    public void SetBGMVolume()
    {
        SoundManager.instance.bgmVolume = slider.value;
    }

    public void SetSFXVolume()
    {
        SoundManager.instance.sfxVolume = slider.value;
    }
}
