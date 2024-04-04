using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : Player
{
    //Properties of Missile
    public GameObject normalAttack;
    public GameObject criticalAttack;
    public float missileSpeed;

    //Properties of Mage's Dodge(Teleport)
    public float teleportDistance;

    protected override void Awake()
    {
        base.Awake();
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        Attack();
    }

    protected override void Attack()
    {
        //Attack speed
        attackTick += Time.deltaTime;

        Vector2 attackDir = (mousePos - (Vector2)this.transform.position);
        float attackDeg = Vector2.SignedAngle(Vector2.right, attackDir);
        if(Input.GetMouseButton(0) && attackTick >= attackSpeed)
        {
            //Critical
            if(Random.Range((float)0, 1) <= criticalChance)
            {
                GameObject cAttack = GameObject.Instantiate(criticalAttack, this.transform.position, Quaternion.Euler(0, 0, attackDeg));
                Rigidbody2D rAttack = cAttack.GetComponent<Rigidbody2D>();
                MissileController mLogic = cAttack.GetComponent<MissileController>();

                mLogic.damage = this.power * this.criticalDamage;
                rAttack.AddForce(attackDir.normalized * missileSpeed, ForceMode2D.Impulse);
            }
            //Normal Hit
            else
            {
                GameObject nAttack = GameObject.Instantiate(normalAttack, this.transform.position, Quaternion.Euler(0, 0, attackDeg));
                Rigidbody2D rAttack = nAttack.GetComponent<Rigidbody2D>();
                MissileController mLogic = nAttack.GetComponent<MissileController>();

                mLogic.damage = this.power;
                rAttack.AddForce(attackDir.normalized * missileSpeed, ForceMode2D.Impulse);
            }

            attackTick = 0;
        }
    }

    protected override void Dodge()
    {
        //Dodge speed
        dodgeTick += Time.deltaTime;

        RaycastHit2D hit = Physics2D.Raycast(transform.position + (Vector3)inputDir.normalized * (teleportDistance+0.5f), transform.forward, 1, LayerMask.GetMask("Wall"));

        if (dodgeTick >= dodgeCool && hit.collider == null && Input.GetKeyDown(KeyCode.Space))
        {
            transform.Translate(inputDir.normalized * teleportDistance);
            dodgeTick = 0;
        }
    }
}
