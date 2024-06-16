using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Web : AOEType
{
    static int touchedWebs = 0;
    static bool onWeb = false;
    Player pLogic;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        WhenOnArea(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        OutOfArea(collision);
    }

    public override void WhenOnArea(Collider2D player)
    {
        if (player.tag != "Player" || onWeb) return;
        onWeb = true;
        touchedWebs++;
        pLogic = player.GetComponent<Player>();
        pLogic.speedWeight -= pLogic.baseSpeed * slowness / touchedWebs;
        Debug.Log(touchedWebs);
    }

    void OutOfArea(Collider2D player)
    {
        if (player.tag != "Player" || !onWeb) return;
        onWeb = false;
        pLogic = player.GetComponent<Player>();
        pLogic.speedWeight += pLogic.baseSpeed * slowness / touchedWebs;
        touchedWebs--;
    }
}
