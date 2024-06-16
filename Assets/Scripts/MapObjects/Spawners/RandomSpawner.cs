using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public float hp = 20;
    float _hp;

    public float spawnTime;
    float curSpawnTime;
    public List<string> monsters;

    private void OnEnable()
    {
        _hp = hp;
        curSpawnTime = 0;
    }

    private void Update()
    {
        curSpawnTime += Time.deltaTime;
        if(curSpawnTime >= spawnTime)
        {
            curSpawnTime = 0;
            SpawnUnit();
        }
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
                Destroy(this.gameObject);
            }
        }
    }

    void SpawnUnit()
    {
        int randIdx = Random.Range(0, monsters.Count);
        Vector3 spawnPosition = transform.localPosition + Vector3.down;
        GameObject randMonster = ObjectManager.instance.Activate(monsters[randIdx]);
        SoundManager.instance.PlaySound("randomSpawner");
        randMonster.transform.parent = this.transform.parent.parent.parent;
        randMonster.transform.localPosition = spawnPosition;
    }
}