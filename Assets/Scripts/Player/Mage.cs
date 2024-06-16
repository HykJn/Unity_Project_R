using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : Player
{
    //Properties of Mage
    [Header("----- Mage -----")]

    //Properties of Skiil1(Teleport)
    [Header("- Skill1(SpaceBar)")]
    public float baseTeleportDistance;
    [HideInInspector] public float teleportDistanceWeight = 0f;
    float teleportDistance
    {
        get { return baseTeleportDistance + teleportDistanceWeight; }
    }
    public float baseTeleportCool;
    [HideInInspector] public float teleportCoolDim = 0f;
    [HideInInspector]
    public float teleportCool
    {
        get
        {
            float cool = baseTeleportCool - teleportCoolDim;
            if (cool > 0) return cool;
            else return 0;
        }
    }
    [HideInInspector] public float curTeleportCool = 0f;

    //Properties of Skiil2(Bless Barrier)
    [Header("- Skill2(Right Click)")]
    public float blessDamage;
    public float baseBlessDuration;
    [HideInInspector] public float blessDurationWeight = 0f;
    float blessDuration
    {
        get { return baseBlessDuration + blessDurationWeight; }
    }
    float curBlessDuration = 0f;
    public float baseBlessCool;
    [HideInInspector] public float BlessCoolDim = 0f;
    [HideInInspector]
    public float blessCool
    {
        get
        {
            float cool = baseBlessCool - BlessCoolDim;
            if (cool > 0) return cool;
            else return 0;
        }
    }
    [HideInInspector] public float curBlessCool = 0f;
    bool onBless = false;

    [Header("- Objects")]
    //Prefabs of Missile
    public GameObject normalAttack;
    public GameObject criticalAttack;

    //Properties of Skill1(Teleport)
    public GameObject teleportEffect;
    public GameObject teleportTrail;

    //Properties of Skill2
    public GameObject barrier;
    float LockCool;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        curTeleportCool = teleportCool;
        curBlessCool = blessCool;
    }

    protected override void Update()
    {
        base.Update();
        Attack();
        SkillLock();
        Skill1();
        Skill2();      
    }

    public override void Hit(int damage, GameObject ObjInfo = null)
    {
        if (onBless)
        {
            onBless = false;
            damage = 0;
        }
        base.Hit(damage, ObjInfo);
    }

    protected override void Attack()
    {
        attackTimer += Time.deltaTime;
        if (mouseOnUI || usingPanel) return;
        if (Input.GetMouseButton(0) && attackTimer >= attackSpeed)
        {
            Vector2 attackDir = (mousePos - (Vector2)this.transform.position).normalized;
            Quaternion attackDeg = Quaternion.LookRotation(Vector3.forward, attackDir);
            bool isCrit = Random.Range((float)0, 1) <= criticalChance;
            GameObject attackObj;

            if (isCrit) //Critical
            {
                this.damage = power * criticalDamage + extraDamage;
                attackObj = ObjectManager.instance.Activate("mageCritAttack");
            }
            else //Normal
            {
                this.damage = power + extraDamage;
                attackObj = ObjectManager.instance.Activate("mageNormalAttack");
            }

            attackObj.transform.position = this.transform.position + (Vector3)attackDir * 0.1f;
            attackObj.transform.rotation = attackDeg;

            Rigidbody2D rAttack = attackObj.GetComponent<Rigidbody2D>();
            MissileController mLogic = attackObj.GetComponent<MissileController>();

            mLogic.damage = this.damage;
            mLogic.isCrit = isCrit;
            rAttack.AddForce(attackDir * 6f, ForceMode2D.Impulse);
            SoundManager.instance.PlaySound("mageAttack");
            attackTimer = 0;
        }
    }

    protected override void Skill1() //Teleport
    {
        curTeleportCool += Time.deltaTime;
        if (usingPanel) return;
        Vector2 teleportTransform = transform.position + (Vector3)inputDir.normalized * teleportDistance;
        Collider2D hit = Physics2D.OverlapBox(teleportTransform, new Vector2(0.8f, 0.8f), 0f, LayerMask.GetMask("Wall"));

        Vector2 roomPos = this.transform.parent.position;
        bool xBorderOut = Mathf.Abs(roomPos.x - teleportTransform.x) >= 11f;
        bool yBorderOut = Mathf.Abs(roomPos.y - teleportTransform.y) >= 5f;
        bool inBorder = !xBorderOut && !yBorderOut;

        if (Input.GetKeyDown(KeyCode.Space) && inBorder && hit == null && inputDir != Vector2.zero && curTeleportCool >= teleportCool)
        {
            SoundManager.instance.PlaySound("mageTeleport");
            Instantiate(teleportEffect, this.transform);
            Instantiate(teleportTrail, this.transform.position, Quaternion.identity);
            transform.Translate(inputDir.normalized * teleportDistance);
            curTeleportCool = 0;
        }
    }

    protected override void Skill2()  //Bless Barrier
    {
        curBlessCool += Time.deltaTime;
        if (mouseOnUI || usingPanel) return;
        if (Input.GetMouseButtonDown(1) && !onBless && curBlessCool >= blessCool && !isLock)
        {
            SoundManager.instance.PlaySound("mageBless");
            onBless = true;
            extraDamage += blessDamage;
            UIManager.instance.Effection(new Buff(BuffTypes.Bless, blessDuration));
        }
        if (onBless)
        {
            curBlessCool = 0;
            curBlessDuration += Time.deltaTime;
        }
        if (curBlessDuration >= blessDuration)
        {
            onBless = false;
            curBlessDuration = 0;
            extraDamage -= blessDamage;
        }

        barrier.SetActive(onBless);
    }
    public void SkillLock()
    {
        if (isLock)
        {
            LockCool += Time.deltaTime;
            if (LockCool > 3)
            {
                isLock = false;
                LockCool = 0;
            }
        }
    }    
}
