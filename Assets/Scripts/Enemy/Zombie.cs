using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Enemy
{  
    //Properties of Attack
    GameObject target;
    Vector2 attackDir;

    //Components
    Animator anim;

    void Awake()
    {
        attackTick = attackSpeed;
        anim = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
        target = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        attackTick += Time.deltaTime;
        if (!isAttack && attackTick >= attackSpeed && Vector2.Distance(this.transform.position, GameObject.FindWithTag("Player").transform.position) <= distance)
        {
            isAttack = true;
            anim.enabled = false;
            Invoke("Attack", 0.5f);
        }
        else if (!isAttack)
        {
            Chase();
        }
    }

    void Attack()
    {
        Invoke("AttackOut", 0.3f);

        attackDir = target.transform.position - this.transform.position;
    }

    void AttackOut()
    {
        isAttack = false;
        anim.enabled = true;
        attackTick = 0;
        GameObject slash = ObjectManager.instance.Activate("zomSlash");
        SoundManager.instance.PlaySound("zombieAttack");
        slash.transform.position = this.transform.position;
        slash.transform.position += (Vector3)attackDir.normalized * 0.75f;
    }

    protected override void Chase()
    {
        base.Chase();
    }
}
