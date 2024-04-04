using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //General Status
    public float hp;
    public float speed;
    public float distance;
    public bool isAttack = false;
    public float attackSpeed;
    protected float attackTick;
    protected bool onHit = false;

    //Components
    protected SpriteRenderer render;

    protected virtual void Chase() { }

    protected void hitOut()
    {
        onHit = false;
        render.color = Color.white;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Hit event by player's missile
        if (collision.gameObject.tag == "PlayerMissile")
        {
            onHit = true;
            render.color = Color.white * 0.7f + new Color(0, 0, 0, 1);
            Debug.Log(hp);
            if (hp <= 0)
            {
                Destroy(this.gameObject);
            }
            Invoke("hitOut", 0.1f);
        }
    }
}