using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : AOEType
{
    bool isSlowed;
    Player pLogic;
    private void OnTriggerStay2D(Collider2D collision)
    {
        WhenOnArea(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        OutOfArea(collision);
    }

    public override void WhenOnArea(Collider2D player)
    {
        if (player.tag != "Player") return;
        pLogic = player.GetComponent<Player>();
        pLogic.Hit(damage, this.gameObject);
        if(!isSlowed)
        {
            pLogic.speedWeight -= pLogic.baseSpeed * slowness;
            isSlowed = true;
        }
    }

    void OutOfArea(Collider2D player)
    {
        if (player.tag != "Player") return;
        if (!isSlowed) return;
        isSlowed = false;
        pLogic.speedWeight += pLogic.baseSpeed * slowness;
    }
}
