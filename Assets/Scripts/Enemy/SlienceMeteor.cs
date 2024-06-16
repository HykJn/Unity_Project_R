using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlienceMeteor : MonoBehaviour
{
    Collider2D col;

    void Start()
    {
        col = GetComponent<Collider2D>();
        col.enabled = false;
        Invoke("Meteor", 2f);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player plogic = GameObject.FindWithTag("Player").GetComponent<Player>();
            plogic.isLock = true;
        }
        col.enabled = false;
    }
    void Meteor()
    {
        col.enabled = true;
    }
}