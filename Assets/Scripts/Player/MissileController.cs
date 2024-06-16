using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    //Player's Damage
    [HideInInspector]
    public float damage;
    public bool isCrit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Hitting Event on Enemy
        if (collision.tag == "Enemy")
        {
            Enemy eLogic = collision.GetComponent<Enemy>();
            if (eLogic != null) eLogic.Hit(damage);
            this.gameObject.SetActive(false);
            if (!isCrit) SoundManager.instance.PlaySound("mageAttackNormalHit");
            else SoundManager.instance.PlaySound("mageAttackCriticalHit");

        }
        //Bordering
        if (collision.gameObject.tag == "Wall")
        {
            this.gameObject.SetActive(false);
            if (!isCrit) SoundManager.instance.PlaySound("mageAttackNormalHit");
            else SoundManager.instance.PlaySound("mageAttackCriticalHit");
        }
    }
}