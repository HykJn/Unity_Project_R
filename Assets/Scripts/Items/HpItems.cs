using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HpType
{
    Full, Half, Emtpy
}

public class HpItems : MonoBehaviour
{
    public HpType hpType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;

        Player pLogic = collision.GetComponent<Player>();

        switch(hpType)
        {
            case HpType.Full:
                if(pLogic.hp <= pLogic.maxHp - 2)
                {
                    pLogic.hp += 2;
                    this.gameObject.SetActive(false);
                }
                else if(pLogic.hp <= pLogic.maxHp - 1)
                {
                    pLogic.hp += 1;
                    this.gameObject.SetActive(false);
                }
                else this.gameObject.SetActive(false);
                break;
            case HpType.Half:
                if (pLogic.hp <= pLogic.maxHp - 1)
                {
                    pLogic.hp += 1;
                    this.gameObject.SetActive(false);
                }
                else this.gameObject.SetActive(false);
                break;
            case HpType.Emtpy:
                if (pLogic.maxHp < 20)
                {
                    pLogic.maxHp += 2;
                    this.gameObject.SetActive(false);
                }
                else this.gameObject.SetActive(false);
                break;
        }
        SoundManager.instance.PlaySound("getItem");
        GameManager.GetItem(this.gameObject);
    }
}
