using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneController : MonoBehaviour
{
    private void OnDisable()
    {
        SoundManager.instance.SFXChannel.Stop();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player pLogic = collision.gameObject.GetComponent<Player>();
            pLogic.Hit(1, this.gameObject);
            this.gameObject.SetActive(false);
        }

        if (collision.gameObject.tag == "Wall")
        {
            this.gameObject.SetActive(false);
        }
    }
}
