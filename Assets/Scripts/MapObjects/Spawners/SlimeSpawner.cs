
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSpawner : MonoBehaviour
{
    public float hp = 14;
    float _hp;
    public float spawnTime = 20f;
    float curSpawnTime;

    private void OnEnable()
    {
        _hp = hp;
        curSpawnTime = 0;
    }

    private void Update()
    {
        curSpawnTime += Time.deltaTime;
        if (curSpawnTime >= spawnTime)
        {
            curSpawnTime = 0;
            SpawnUnit();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Hit event by player's missile
        if (collision.tag == "PlayerMissile")
        {
            _hp -= collision.GetComponent<MissileController>().damage;
            Debug.Log(_hp);
            if (_hp <= 0)
            {
                SoundManager.instance.PlaySound("slimeSpawnerBreak");
                this.transform.parent.parent.parent.GetComponent<NodeMap>().GetNodeMap();
                this.gameObject.SetActive(false);
            }
        }
    }

    void SpawnUnit()
    {
        Vector2 spawnPosition = Vector2.zero;

        // 스포너 프리팹의 이름을 기준으로 위치를 결정
        string spawnerName = gameObject.name;

        // SpawnerDown이라면 y 좌표를 -1로 설정
        if (spawnerName == "SpawnerDown")
        {
            spawnPosition = Vector2.down;
        }
        // SpawnerUp이라면 y 좌표를 1로 설정
        else if (spawnerName == "SpawnerUp")
        {
            spawnPosition = Vector2.up;
        }
        // SpawnerRight이라면 x 좌표를 1로 설정
        else if (spawnerName == "SpawnerRight")
        {
            spawnPosition = Vector2.right;
        }
        // SpawnerLeft이라면 x 좌표를 -1로 설정
        else if (spawnerName == "SpawnerLeft")
        {
            spawnPosition = Vector2.left;
        }
        GameObject slime = ObjectManager.instance.Activate("slime");
        SoundManager.instance.PlaySound("slimeSpawn");
        slime.transform.parent = this.transform.parent.parent.parent;
        slime.transform.localPosition = this.transform.localPosition + (Vector3)spawnPosition;
    }
}
