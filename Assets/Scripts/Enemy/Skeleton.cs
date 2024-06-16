using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy
{
    [Header("- Skeleton")]
    public float DistanceWithPlayer;
    public float RayLength;
    public GameObject boneAttack;

    //Properties of Attack
    Vector2 attackDir;
    Transform target;

    //Properties of Chase
    bool isChase = true;
    bool isWall = false;

    //Components
    Animator anim;

    void Awake()
    {
        //attackTick = attackSpeed;
        anim = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>(); 
    }

    void Update()
    {
        CheckDistance();
        CheakWall();
        Chase();
        Attack(); 
    }

    void Attack()
    {
        attackTick += Time.deltaTime;
        if (attackTick >= attackSpeed)
        {
            attackTick = 0f;
            GameObject bone = ObjectManager.instance.Activate("skelBone");
            bone.transform.position = this.transform.position;
            Rigidbody2D rBone = bone.GetComponent<Rigidbody2D>();
            Vector2 attackDir = pathFinder.targetObj.transform.position - this.transform.position;
            SoundManager.instance.PlaySound("skeletonAttack");
            rBone.AddForce(attackDir.normalized * 5, ForceMode2D.Impulse);
        }
    }
    protected override void Chase()
    {
        if (isChase)
        {
            base.Chase();
        }
    }
    public void CheckDistance()
    {
        if (isWall == false && Vector2.Distance(pathFinder.targetObj.transform.position, this.transform.position) > DistanceWithPlayer)
        {
            isChase = true;
        }

        else if (isWall == true)
        {
            isChase = true;
        }

        else
        {
            isChase = false;
        }
    }
    public void CheakWall()
    {
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, pathFinder.targetObj.transform.position - this.transform.position, RayLength, LayerMask.GetMask("Wall"));
        if (hit)
        {
            isWall = true;
        }
        else
        {
            isWall = false;
        }
    }
}
