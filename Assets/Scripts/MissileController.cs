using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    //Player's Damage
    public float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Hitting Event on Enemy
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy eLogic = collision.gameObject.GetComponent<Enemy>();
            eLogic.hp -= damage;
            Destroy(this.gameObject);
        }

        //Bordering
        if (collision.gameObject.tag == "Wall")
        {
            Destroy(this.gameObject);
        }
    }
}