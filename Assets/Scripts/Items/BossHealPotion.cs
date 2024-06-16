using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealPotion : MonoBehaviour
{
    SkeletonMage BossLogic;
    void Start()
    {
        BossLogic = GameObject.Find("SkeletonMage").GetComponent<SkeletonMage>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SoundManager.instance.PlaySound("getItem");
            BossLogic.potionCount--;
            this.gameObject.SetActive(false);            
        }
    }
}
