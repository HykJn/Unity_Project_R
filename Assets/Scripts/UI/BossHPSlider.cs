using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossHPSlider : MonoBehaviour
{
    public Canvas canvas;
    public GameObject skeletonMage;
    public Image hpBar;
    public TextMeshProUGUI hpText;

    private void OnEnable()
    {
        canvas.worldCamera = Camera.main;
    }

    private void Update()
    {
        SkeletonMage bossLogic = skeletonMage.GetComponent<SkeletonMage>();
        hpText.text = $"{bossLogic._hp}/{bossLogic.hp}";
        hpBar.fillAmount = bossLogic._hp / bossLogic.hp;
    }
}
