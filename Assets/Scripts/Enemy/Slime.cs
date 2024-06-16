using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    [Header("- Slime")]
    public Sprite chargeSprite;
    public Sprite dashSprite;

    //Properties of Charge
    bool charge = false;
    Vector2 attackDir;

    //Components
    Animator anim;

    void Awake()
    {
        attackTick = attackSpeed;
        anim = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        attackTick += Time.deltaTime;
        if(!isAttack && attackTick >= attackSpeed && Vector2.Distance(this.transform.position, GameObject.FindWithTag("Player").transform.position) <= distance)
        {
            isAttack = true;
            attackDir = GameObject.FindWithTag("Player").transform.position - this.transform.position;
            anim.enabled = false;
            render.sprite = chargeSprite;
            Invoke("Attack", 1f);
        }
        else if (!isAttack)
        {
            Chase();
        }

        if(charge)
        {
            transform.Translate(attackDir.normalized * speed *  Time.deltaTime * 6);
        }
    }
    
    void Attack()
    {
        charge = true;
        render.sprite = dashSprite;
        Invoke("AttackOut", 0.3f);
    }
    
    void AttackOut()
    {
        SoundManager.instance.PlaySound("slimeAttack");
        charge = false;
        isAttack = false;
        anim.enabled = true;
        attackTick = 0;
    }

    protected override void Chase()
    {
        base.Chase();
    }
}