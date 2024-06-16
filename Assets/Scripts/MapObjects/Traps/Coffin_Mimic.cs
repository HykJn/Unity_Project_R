using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffin_Mimic : SummonType
{
    Player pLogic;
    Collider2D playerCol;
    CircleCollider2D sensor;
    public EnemyType type;

    private void Awake()
    {
        SetColliderSize();
        playerCol = GameObject.FindWithTag("Player").GetComponent<Collider2D>();
        sensor = this.transform.Find("Sensor").GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        if(sensor.IsTouching(playerCol))
        {
            Summon(type);
            if (type == EnemyType.Skeleton) SoundManager.instance.PlaySound("woodBreak");
            else if (type == EnemyType.Mimic) SoundManager.instance.PlaySound("mimicAwake");
            this.transform.parent.parent.parent.GetComponent<NodeMap>().GetNodeMap();
            Destroy(this.gameObject);
        }
    }

    public override void SetColliderSize()
    {
        sensor = this.transform.Find("Sensor").GetComponent<CircleCollider2D>();
        sensor.radius = distance;
    }

    public override GameObject Summon(EnemyType type)
    {
        GameObject enemy = ObjectManager.instance.Activate(type.ToString().ToLower());
        enemy.transform.position = this.transform.position;
        enemy.transform.parent = this.transform.parent.parent.parent;
        return enemy;
    }
}
