using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornTrap : SimpleType
{
    SpriteRenderer render;
    [SerializeField] Sprite up;
    [SerializeField] Sprite down;

    private void Awake()
    {
        render = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Activate(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Invoke("Deactivate", 0.5f);
        }
    }

    public override void Activate(Collider2D player)
    {
        if (player.tag != "Player") return;
        render.sprite = up;
        SoundManager.instance.PlaySound("thornTrap");
        player.GetComponent<Player>().Hit(1, this.gameObject);
    }

    void Deactivate()
    {
        render.sprite = down;
    }
}
