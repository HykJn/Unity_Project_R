using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum DebuffTypes
{
    Slow, Bleed, Poisoned, Bind
}

public enum BuffTypes
{
    Bless, Shield
}

[Serializable]
public struct Debuff
{
    public DebuffTypes debuffType;
    public Sprite icon;
    public float duration;
    public float curDuration;

    public Debuff(DebuffTypes type, float duration)
    {
        debuffType = type;
        this.duration = duration;
        this.curDuration = duration;
        switch(type)
        {
            case DebuffTypes.Slow:
                icon = UIManager.instance.slow;
                break;
            case DebuffTypes.Bleed:
                icon = UIManager.instance.bleed;
                break;
            case DebuffTypes.Poisoned:
                icon = UIManager.instance.poisoned;
                break;
            case DebuffTypes.Bind:
                icon = UIManager.instance.bind;
                break;
            default:
                icon = null;
                break;
        }
    }
    public IEnumerator CoolDown(GameObject obj)
    {
        while (true)
        {
            curDuration -= Time.deltaTime;
            obj.transform.GetChild(0).GetComponent<Image>().fillAmount = curDuration / duration;
            if (curDuration <= 0)
            {
                GameObject.Destroy(obj);
                yield break;
            }
            yield return new WaitForEndOfFrame();
        }
    }
}

[Serializable]
public struct Buff
{
    public BuffTypes buffType;
    public Sprite icon;
    public float duration;
    public float curDuration;

    public Buff(BuffTypes type, float duration)
    {
        buffType = type;
        this.duration = duration;
        this.curDuration = duration;
        switch (type)
        {
            case BuffTypes.Bless:
                icon = UIManager.instance.bless;
                break;
            case BuffTypes.Shield:
                icon = UIManager.instance.shield;
                break;
            default:
                icon = null;
                break;
        }
    }

    public IEnumerator CoolDown(GameObject obj)
    {
        while (true)
        {
            curDuration -= Time.deltaTime;
            obj.transform.GetChild(0).GetComponent<Image>().fillAmount = curDuration / duration;
            if (curDuration <= 0)
            {
                GameObject.Destroy(obj);
                yield break;
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
public class UIManager : MonoBehaviour
{
    [Header("- Texts")]
    public TextMeshProUGUI timer;
    [HideInInspector] public string time = "00:00";
    public TextMeshProUGUI score;
    [HideInInspector] public int curScore;
    [Header("- Lifes")]
    public GameObject hp;
    public Sprite fullHp, halfHp, emptyHp;
    [Header("- Effection")]
    public GameObject effection;
    [Header("- Buffs Icon")]
    public Sprite bless;
    public Sprite shield;
    [Header("- Debuffs Icon")]
    public Sprite slow;
    public Sprite bleed;
    public Sprite poisoned;
    public Sprite bind;
    int curDebuffs = 0;
    int curBuffs = 0;
    [Header("- Skills")]
    public Image Skill1;
    public TextMeshProUGUI Skill1Time;
    public Image Skill2;
    public TextMeshProUGUI Skill2Time;

    public static UIManager instance;

    Player pLogic;
    Transform lifesPanel;
    Transform effectionPanel;
    List<GameObject> hpCells;
    int maxHp = 0;
    int curHp;

    void Awake()
    {
        pLogic = GameObject.FindWithTag("Player").GetComponent<Player>();
        lifesPanel = GameObject.Find("LifesPanel").transform;
        effectionPanel = GameObject.Find("EffectionPanel").transform;
        hpCells = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        DisplayHP();
        UpdateTexts();
        SkillCoolDown();
    }

    void DisplayHP()
    {
        //Set Max Hp
        if(maxHp < pLogic.maxHp)
        {
            GameObject hpCell = Instantiate(hp, lifesPanel);
            hpCells.Add(hpCell);
            Image hpImage = hpCell.GetComponent<Image>();
            if (maxHp < 10) hpCell.transform.localPosition = Vector2.right * (14 + 42.5f * maxHp) + Vector2.down * 5;
            else hpCell.transform.localPosition = new Vector2((14 + 42.5f * (maxHp - 10)), -85);
            hpImage.sprite = emptyHp;
            maxHp += 2;
        }

        //Set Current Hp
        curHp = pLogic.hp;
        if (curHp > maxHp) return;
        int fullHps = curHp / 2;
        int curHps = curHp % 2;
        for(int i = 0; i < fullHps; i++)
        {
            Image hpImage = hpCells[i].GetComponent<Image>();
            hpImage.sprite = fullHp;
        }
        for(int i = fullHps; i < hpCells.Count; i++)
        {
            Image hpImage = hpCells[i].GetComponent<Image>();
            hpImage.sprite = emptyHp;
        }
        if (curHps == 1)
        {
            Image hpImage = hpCells[fullHps].GetComponent<Image>();
            hpImage.sprite = halfHp;
        }

    }

    void UpdateTexts()
    {
        timer.text = time;
        score.text = curScore.ToString();
    }

    void SkillCoolDown()
    {
        if(pLogic is Mage)
        {
            Mage mLogic = (Mage)pLogic;
            float skill1CoolDown = mLogic.curTeleportCool / mLogic.teleportCool;
            float skill2CoolDown = mLogic.curBlessCool / mLogic.blessCool;

            //Skill1
            if (mLogic.curTeleportCool < mLogic.teleportCool)
            {
                Skill1.fillAmount = skill1CoolDown;
                Skill1Time.text = (mLogic.teleportCool - mLogic.curTeleportCool).ToString(".");
            }
            else
            {
                Skill1.fillAmount = 1;
                Skill1Time.text = "";
            }
            //Skill2
            if (mLogic.curBlessCool < mLogic.blessCool)
            {
                Skill2.fillAmount = skill2CoolDown;
                Skill2Time.text = (mLogic.blessCool - mLogic.curBlessCool).ToString(".");
            }
            else
            {
                Skill2.fillAmount = 1;
                Skill2Time.text = "";
            }
        }
    }

    public void Effection(Buff buff)
    {
        GameObject effectionObj = Instantiate(effection, effectionPanel);
        effectionObj.GetComponent<Image>().sprite = buff.icon;
        effectionObj.transform.GetChild(0).GetComponent<Image>().sprite = buff.icon;
        effectionObj.transform.localPosition = new Vector2(10 + 74 * curBuffs, -15);
        StartCoroutine(buff.CoolDown(effectionObj));
    }

    public void Effection(Debuff debuff)
    {
        GameObject effectionObj = Instantiate(effection, effectionPanel);
        Image bg = effectionObj.GetComponent<Image>();
        Image mask = effectionObj.transform.GetChild(0).GetComponent<Image>();
        bg.sprite = debuff.icon;
        mask.sprite = debuff.icon;
        effectionObj.GetComponent<RectTransform>().position = new Vector2(10 + 74 * curDebuffs, -95);
        StartCoroutine(debuff.CoolDown(effectionObj));
    }

    public UIManager()
    {
        instance = this;
    }
}
