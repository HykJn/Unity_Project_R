using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Slime, Zombie, Skeleton, Mimic
}

public abstract class SimpleType : MonoBehaviour
{
    public abstract void Activate(Collider2D player);
}

public abstract class AOEType : MonoBehaviour
{
    public int damage;
    public float slowness;
    public abstract void WhenOnArea(Collider2D player);
}

public abstract class SummonType : MonoBehaviour
{
    public float distance;
    public abstract void SetColliderSize();
    public abstract GameObject Summon(EnemyType type);
}

public abstract class ETCType : MonoBehaviour
{
    public abstract void Activate();
}