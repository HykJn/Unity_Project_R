using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("- Common Status")]
    //General Status
    public float hp;
    public float _hp;
    public float speed;
    [HideInInspector] public float speedWeight = 1;
    protected float finalSpeed
    {
        get
        {
            return speed * speedWeight;
        }
    }
    [Space(15)]
    public float power;
    public float attackSpeed;
    public float distance;

    //Properties of Attack
    protected bool isAttack = false;
    protected float attackTick;

    //Properties of Move
    protected Vector2 moveDir;

    //Components
    protected SpriteRenderer render;
    protected PathFinder pathFinder;

    private void OnEnable()
    {
        pathFinder = GetComponent<PathFinder>();
        _hp = hp;
    }

    protected virtual void Chase() 
    {
        pathFinder.PathFinding();

        if (pathFinder.pathList.Count > 0)
        {
            moveDir = new Vector2(pathFinder.pathList[0].x, pathFinder.pathList[0].y) - (Vector2)this.transform.position;
        }
        else
        {
            moveDir = pathFinder.targetObj.transform.position - this.transform.position;
        }

        this.transform.Translate(moveDir.normalized * finalSpeed * Time.deltaTime);
    }

    public void Hit(float damage)
    {
        this._hp -= damage;
        render.color = Color.white * 0.7f + new Color(0, 0, 0, 1);
        Debug.Log(_hp);
        if (_hp <= 0)
        {
            this.gameObject.SetActive(false);
            if (this is Slime)
            {
                SoundManager.instance.PlaySound("slimeDeath");
                GameManager.e_Slime += 1;
            }
            else if (this is Zombie)
            {
                SoundManager.instance.PlaySound("zombieDeath");
                GameManager.e_Zombie += 1;
            }
            else if (this is Skeleton)
            {
                SoundManager.instance.PlaySound("skeletonDeath");
                GameManager.e_Skeleton += 1;
            }
            else if (this is SkeletonMage)
            {
                SoundManager.instance.PlaySound("skeletonMageDeath");
                SceneChanger.ToClear();
            }
            else if (this is Mimic)
            {
                SoundManager.instance.PlaySound("mimicDeath");
                GameManager.e_Mimic += 1;
            }

            GameManager.score += 500;
        }
        Invoke("HitOut", 0.1f);
    }

    protected void HitOut()
    {
        render.color = Color.white;
    }
}