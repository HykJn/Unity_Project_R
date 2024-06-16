using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuffItemTypes
{
    Glove, Shoes, PowerPotion, TeleportBook, BlessBook, Skull, BarrierBook, Watch, Shield
}
public class BuffItems : MonoBehaviour
{
    public BuffItemTypes type;
    Player pLogic;
    Mage mageLogic;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;
        pLogic = collision.GetComponent<Player>();
        switch (type)
        {
            case BuffItemTypes.Shoes:
                pLogic.speedWeight += pLogic.baseSpeed * 0.2f;
                this.gameObject.SetActive(false);
                break;
            case BuffItemTypes.Watch:
                mageLogic = collision.GetComponent<Mage>();
                if(mageLogic != null)
                {
                    if (mageLogic.BlessCoolDim < mageLogic.baseBlessCool * 0.3f) mageLogic.BlessCoolDim += mageLogic.baseBlessCool * 0.05f;
                    if (mageLogic.teleportCoolDim < mageLogic.baseTeleportCool * 0.3f) mageLogic.teleportCoolDim += mageLogic.baseTeleportCool * 0.05f;
                }
                this.gameObject.SetActive(false);
                break;
            case BuffItemTypes.TeleportBook:
                mageLogic = collision.GetComponent<Mage>();
                if (mageLogic != null) mageLogic.teleportDistanceWeight += 0.5f;
                this.gameObject.SetActive(false);
                break;
            case BuffItemTypes.BlessBook:
                mageLogic = collision.gameObject.GetComponent<Mage>();
                if (mageLogic != null)
                {
                    mageLogic.blessDurationWeight += 1f;
                    mageLogic.blessDamage += 1f;
                }
                this.gameObject.SetActive(false);
                break;
            case BuffItemTypes.PowerPotion:
                pLogic.powerWeight += 1;
                this.gameObject.SetActive(false);
                break;
            case BuffItemTypes.Glove:
                if (pLogic.criticalChance < 1) pLogic.criticalChance += 0.2f;
                this.gameObject.SetActive(false);
                break;
            case BuffItemTypes.Skull:
                pLogic.criticalDamage += 0.2f;
                this.gameObject.SetActive(false);
                break;
            case BuffItemTypes.Shield:
                if (!pLogic.buffItems["Shield"]) pLogic.buffItems["Shield"] = true;
                this.gameObject.SetActive(false);
                break;
        }
        SoundManager.instance.PlaySound("getItem");
        GameManager.GetItem(this.gameObject);
        if (type != BuffItemTypes.Shield) PlayerStatus.instance.GetItem(this.gameObject);
    }
}
