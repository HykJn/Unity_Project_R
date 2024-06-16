using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTrap : SimpleType
{
    public float duration;
    Player pLogic;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Activate(collision);
    }

    public override void Activate(Collider2D player)
    {
        if (player.tag != "Player") return;
        pLogic = player.GetComponent<Player>();
        pLogic.isBind = true;
        SoundManager.instance.PlaySound("bearTrap");
        this.GetComponent<SpriteRenderer>().enabled = false;
        Invoke("Deactivate", duration);
    }

    public void Deactivate()
    {
        pLogic.isBind = false;
        Destroy(this.gameObject);
    }
}
