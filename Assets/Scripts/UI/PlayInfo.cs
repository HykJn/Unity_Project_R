using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayInfo : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI deathByText;
    public TextMeshProUGUI slimeText;
    public TextMeshProUGUI zombieText;
    public TextMeshProUGUI skeletonText;
    public TextMeshProUGUI mimicText;
    public GameObject itemIcon;

    // Start is called before the first frame update
    void Start()
    {
        if (timeText != null)
        {
            timeText.text = $"Time: {GameManager.instance.curTime}";
        }
        if (scoreText != null)
        {
            scoreText.text = $"Score: {GameManager.score}";
        }
        if (deathByText != null)
        {
            deathByText.text = $"Death By: {GameManager.deathBy}";
        }
        if (slimeText != null)
        {
            slimeText.text = $"Slimes: {GameManager.e_Slime}";
        }
        if (zombieText != null)
        {
            zombieText.text = $"Zombies: {GameManager.e_Zombie}";
        }
        if (skeletonText != null)
        {
            skeletonText.text = $"Skeletons: {GameManager.e_Skeleton}";
        }
        if (mimicText != null)
        {
            mimicText.text = $"Mimics: {GameManager.e_Mimic}";
        }


        int idx = 0;
        foreach(Sprite s in GameManager.itemIcons)
        {
            GameObject itemCell = Instantiate(itemIcon, GameObject.Find("ItemsText").transform);
            itemCell.GetComponent<Image>().sprite = s;
            if (idx < 9) itemCell.GetComponent<RectTransform>().anchoredPosition = new Vector2(150 * idx, -80);
            else itemCell.GetComponent<RectTransform>().anchoredPosition = new Vector2(150 * (idx - 9), -180);
            itemCell.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = GameManager.counts[idx].ToString("00");
            idx++;
        }
    }
}
