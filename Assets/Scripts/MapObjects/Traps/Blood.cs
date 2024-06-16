using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : AOEType
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        WhenOnArea(collision);
    }

    public override void WhenOnArea(Collider2D player)
    {
        if (player.tag != "Player") return;
        player.GetComponent<Player>().Hit(damage, this.gameObject);
    }
}
