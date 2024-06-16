using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public GameObject opposite;
    Teleporter oppositeLogic;
    GameObject player;
    bool onPlayer;
    float teleportCool = 0;
    float teleportTick;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        oppositeLogic = opposite.GetComponent<Teleporter>();
        teleportTick = teleportCool;
    }
    private void Update()
    {
        teleportTick += Time.deltaTime;
        if (onPlayer && teleportTick >= teleportCool && Input.GetKeyDown(KeyCode.E))
        {
            player.transform.position = opposite.transform.position;
            SoundManager.instance.PlaySound("mageTeleport");
            teleportTick = 0;
            oppositeLogic.teleportTick = 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player") return;
        onPlayer = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player") return;
        onPlayer = false;
    }
}
