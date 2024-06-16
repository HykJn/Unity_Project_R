using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    public TextMeshProUGUI classText;
    public TextMeshProUGUI maxHpText;
    public TextMeshProUGUI powerText;
    public TextMeshProUGUI attackSpeedText;
    public TextMeshProUGUI criticalChanceText;
    public TextMeshProUGUI criticalDamageText;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI coolText;
    public GameObject itemImage;
    public List<BuffItemTypes> items;
    public List<int> counts;
    public List<GameObject> itemCells;

    Player pLogic
    {
        get
        {
            return GameObject.FindWithTag("Player").GetComponent<Player>();
        }
    }

    public static PlayerStatus instance;

    // Update is called once per frame
    void Update()
    {
        int c_watch;
        if (items.Contains(BuffItemTypes.Watch)) c_watch = counts[items.IndexOf(BuffItemTypes.Watch)];
        else c_watch = 0;
        classText.text = $"Class: {pLogic.name}";
        maxHpText.text = $"MaxHp: {pLogic.maxHp}";
        powerText.text = $"Power: {pLogic.basePower} (+ {pLogic.powerWeight})";
        attackSpeedText.text = $"Attack Speed: {pLogic.baseAttackSpeed} (- {pLogic.attackSpeedWeight})";
        criticalChanceText.text = $"Critical Chance: {pLogic.criticalChance * 100}%";
        criticalDamageText.text = $"Critical Damage: {pLogic.criticalDamage * 100}%";
        speedText.text = $"Speed: {pLogic.baseSpeed} (+ {pLogic.speedWeight})";
        coolText.text = $"Cooltime Dim: {c_watch * 5}%";
    }

    public void GetItem(GameObject item)
    {
        BuffItemTypes type = item.GetComponent<BuffItems>().type;
        if (!items.Contains(type))
        {
            GameObject itemCell = Instantiate(itemImage, this.transform);
            items.Add(type);
            itemCells.Add(itemCell);
            counts.Add(1);
            int idx = items.IndexOf(type);
            TextMeshProUGUI count = itemCell.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            count.text = counts[idx].ToString();
            Sprite itemSprite = item.GetComponent<SpriteRenderer>().sprite;
            itemCell.GetComponent<Image>().sprite = itemSprite;
            itemCell.GetComponent<RectTransform>().anchoredPosition = new Vector2(50 + 100 * idx, -180);
        }
        else
        {
            int idx = items.IndexOf(type);
            counts[idx]++;
            itemCells[idx].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = counts[idx].ToString();
        }
    }

    public PlayerStatus()
    {
        instance = this;
    }
}
