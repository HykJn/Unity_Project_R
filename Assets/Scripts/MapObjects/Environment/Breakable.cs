using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{ 
    public float hp;
    public float _hp;
    SpriteRenderer render;
    public Sprite fine;
    public Sprite cracked;
    public int threshold;
    public bool isJar;

    // Start is called before the first frame update
    private void Awake()
    {
        render = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        _hp = hp;
    }

    private void Update()
    {
        if(_hp > threshold) render.sprite = fine;
        else render.sprite = cracked;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Hit event by player's missile
        if (collision.gameObject.tag == "PlayerMissile")
        {
            _hp -= collision.GetComponent<MissileController>().damage;
            Debug.Log(_hp);
            if (_hp <= 0)
            {
                this.transform.parent.parent.parent.GetComponent<NodeMap>().GetNodeMap();
                this.gameObject.SetActive(false);
                if (!isJar) SoundManager.instance.PlaySound("woodBreak");
                else SoundManager.instance.PlaySound("jarBreak");
                this.transform.parent.parent.parent.GetComponent<NodeMap>().GetNodeMap();
            }
        }
    }
}
