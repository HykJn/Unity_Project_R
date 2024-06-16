using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PatternState
{
    Clear, None, Fail
}
public class SkeletonMage : Enemy
{
    [Header("- Skeleton Mage")]
    public float DistanceWithPlayer;
    public float RayLength;
    public GameObject normalAttack;

    [Header("- Summon Monster")]
    [Space(15)]
    // 몬스터 소환
    public GameObject[] monsterPrefabs;
    public float summonCool;
    float summonCoolTick;
    //public Transform[] spawnPoints;

    [Header("- Boss Healing")]
    [Space(15)]
    // 보스 힐 기믹
    public Transform[] PotionGenPoints_L;
    public Transform[] PotionGenPoints_R;
    public GameObject BossHealPotion;
    public PatternState Phase1 = PatternState.None;
    public PatternState Phase2 = PatternState.None;
    public PatternState Phase3 = PatternState.None;
    float HealTimer;
    bool potionGen = false;
    [HideInInspector]public int potionCount = 0;
    int potions = 3;
    float stunTime;
    

    [Header("- Slience Meteor")]
    [Space(15)]
    //메테오 스킬 
    public GameObject Meteor;
    public float meteorTimer;
    public float metoorCool;

    //Properties for Attack
    Vector2 attackDir;
    Transform target;

    //Properties for Chase
    bool isChase = true;
    bool isWall = false;

    //Components
    Animator anim;

    void Awake()
    {
        attackTick = attackSpeed;
        anim = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
        //summonCoolTick = summonCool;      
    }

    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        CheckDistance();
        CheckWall();               
        Attack();
        SummonMonster();
        CheckPhase();
        SlienceMeteor();
        Chase();
    }

    void Attack()
    {
        attackTick += Time.deltaTime;
        if (attackTick >= attackSpeed)
        {
            attackTick = 0f;
            GameObject attack = ObjectManager.instance.Activate("skelMageAttack");
            attack.transform.position = this.transform.position;
            Rigidbody2D rAttack = attack.GetComponent<Rigidbody2D>();
            Vector2 attackDir = target.position - this.transform.position;
            rAttack.AddForce(attackDir.normalized * 5, ForceMode2D.Impulse);
            SoundManager.instance.PlaySound("skeletonMageAttack");
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
        if (isWall == false && Vector2.Distance(target.position, this.transform.position) > DistanceWithPlayer)
        {
            isChase = true;
        }

        else if(isWall == true)  
        {
            isChase = true;
        }

        else 
        {
            isChase = false;
        }
    }
    public void CheckWall()
    {
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, target.position - this.transform.position, RayLength, LayerMask.GetMask("Wall"));
        if (hit)
        {
            isWall = true;
        }
        else
        {
            isWall = false;
        }
    }
    public void SummonMonster()
    {
        int monsterCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if(monsterCount == 1) summonCoolTick += Time.deltaTime;

        if (summonCoolTick < summonCool) return;      
        NodeMap nodeMap = this.transform.parent.GetComponent<NodeMap>();
        Node randNode;
        do
        {
            int x = Random.Range(0, nodeMap.w);
            int y = Random.Range(0, nodeMap.h);
            randNode = nodeMap.nodeMap[x, y];
        } while (randNode.isWall);

        SoundManager.instance.PlaySound("skeletonMageSummon");
        if (monsterCount <= 3)
        {
            GameObject zombie = ObjectManager.instance.Activate("zombie");
            zombie.transform.position = new Vector2(randNode.x, randNode.y);
            zombie.transform.parent = this.transform.parent;
        }

        else if (monsterCount <= 5)
        {
            GameObject skeleton = ObjectManager.instance.Activate("skeleton");
            skeleton.transform.position = new Vector2(randNode.x, randNode.y);
            skeleton.transform.parent = this.transform.parent;
        }

        if (monsterCount == 6) summonCoolTick = 0;
    }

    public void HealingPotion(ref PatternState phase)
    {
        HealTimer += Time.deltaTime;
        this.GetComponent<Collider2D>().enabled = false;

        Transform[] spawnGroup;
        if (this.transform.localPosition.x >= 0.5f) spawnGroup = PotionGenPoints_L;
        else spawnGroup = PotionGenPoints_R;
        if (potionCount == potions)
        {
            potionGen = true;
            render.color = new Color(1, 1, 1, 0.5f);
        }

        //포션 생성 
        if (!potionGen && potionCount < potions)
        {
            foreach (Transform t in spawnGroup)
            {
                GameObject potion = ObjectManager.instance.Activate("skelMagePotion");
                potion.transform.position = t.position;
                potion.transform.parent = t;
                potionCount++;
            }
        }

        ChangeSpeed(true);

        //클리어 판별
        if (potionGen && potionCount == 0)
        {          
            phase = PatternState.Clear;
            potionGen = false;
            HealTimer = 0;

            render.color = new Color(1, 1, 1, 1);
            this.GetComponent<Collider2D>().enabled = true;
            ChangeSpeed(false);
        }
        else if (HealTimer >= 10)
        {
            phase = PatternState.Fail;
            this._hp += 36;
            potionGen = false;
            HealTimer = 0;

            render.color = new Color(1, 1, 1, 1);
            this.GetComponent<Collider2D>().enabled = true;
            ChangeSpeed(false);
        }
        if (phase == PatternState.Fail)
        {
            foreach (Transform t in spawnGroup)
            {
                if (t.childCount > 0)
                {
                    t.GetChild(0).gameObject.SetActive(false);
                    t.GetChild(0).SetParent(null);
                }
            }
            potionCount = 0;
            phase = PatternState.None;
        }
    }
    void CheckPhase()
    {
        if (_hp <= 140 && Phase1 == PatternState.None)
        {
            HealingPotion(ref Phase1);
            isChase = false; 
        }
        else if (_hp <= 80 && Phase2 == PatternState.None) 
        {
            HealingPotion(ref Phase2);
            isChase = false;
        }
        else if (_hp <= 18 && Phase3 == PatternState.None)
        {
            HealingPotion(ref Phase3);
            isChase = false;
        }

        if(Phase1 == PatternState.Clear)
        {
            stunTime += Time.deltaTime;
            if (stunTime < 2)
            {
                isChase = false;
            }
        }
        if (Phase2 == PatternState.Clear)
        {
            stunTime += Time.deltaTime;
            if (stunTime < 2)
            {
                isChase = false;
            }
        }
        if (Phase3 == PatternState.Clear)
        {
            stunTime += Time.deltaTime;
            if (stunTime < 2)
            {
                isChase = false;
            }
        }
    }

    public void SlienceMeteor()
    {
        meteorTimer += Time.deltaTime;
        if (meteorTimer >= metoorCool)
        {
            GameObject meteor = Instantiate(Meteor, target.position, Quaternion.identity);
            meteorTimer = 0;
            Destroy(meteor, 2.2f);
        }      
    }

    void ChangeSpeed(bool fast)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            if (enemy == this.gameObject || enemy == null) continue;
            Enemy elogic = enemy.GetComponent<Enemy>();
            elogic.speedWeight = fast ? 2 : 1;
        }
    }
}
