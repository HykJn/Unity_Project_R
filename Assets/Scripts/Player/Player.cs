using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    //Properties of Input
    protected float axisH, axisV;
    protected Vector2 inputDir;
    protected Vector2 mousePos;
    protected Canvas canv;
    protected GraphicRaycaster raycaster;
    protected PointerEventData ped;
    protected bool mouseOnUI = false;

    //Properties of Hit
    [Header("- Hit")]
    public int maxHp;
    public int hp;
    protected float unHittableTime = 1f;
    protected float hitTimer;
    protected bool isHittable = true;
    protected bool hit = false;
    public bool isDie = false;

    //Properties of Attack
    [Header("- Attack")]
    public float basePower;
    [HideInInspector] public float powerWeight;
    [HideInInspector] public float power
    {
        get
        {
            return basePower + powerWeight;
        }
    }
    public float baseAttackSpeed;
    [HideInInspector] public float attackSpeedWeight;
    [HideInInspector] public float attackSpeed
    {
        get
        {
            return baseAttackSpeed + attackSpeedWeight;
        }
    }
    public float criticalChance;
    public float criticalDamage;
    [HideInInspector] public float extraDamage;
    [HideInInspector] public float damage;
    protected float attackTimer;

    //Properties of Movement
    [Header("- Movement")]
    public float baseSpeed;
    [HideInInspector] public float speedWeight;
    [HideInInspector] public float speed
    {
        get
        {
            return baseSpeed + speedWeight;
        }
    }
    protected Vector2 moveDir;
    [HideInInspector] public bool isBind = false;
    //public bool onWeb = false;

    [Header("- Panels")]
    public GameObject minimapPanel;
    public GameObject statusPanel;
    public GameObject optionPanel;
    protected bool usingPanel
    {
        get
        {
            bool temp;
            if (optionPanel.activeSelf) temp = true;
            else temp = false;
            return temp;
        }
    }

    public bool isLock = false;

    //Properties of Items
    public Dictionary<string, bool> buffItems = new Dictionary<string, bool>()
    {
        { "Shield", false },
        { "Eisen", false }
    };
    public Dictionary<string, float> buffItemsTime = new Dictionary<string, float>()
    {
        { "Eisen", 0f }
    };
    //Components
    protected SpriteRenderer render;
    protected Camera cam;
    protected Rigidbody2D rbody;

    public Transform curRoom
    {
        get
        {
            return this.transform.parent;
        }
    }
    public GameObject minimapMask;

    protected virtual void Awake()
    {
        render = GetComponent<SpriteRenderer>();
        cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        rbody = GetComponent<Rigidbody2D>();
        canv = GameObject.Find("Canvas").GetComponent<Canvas>();
        raycaster = canv.GetComponent<GraphicRaycaster>();
        ped = new PointerEventData(null);
    }

    protected virtual void Start()
    {
        attackTimer = attackSpeed;
    }

    protected virtual void Update()
    {
        GetInput();
        View();
        Move();
        ChangeSprite();
        BuffControl();
        Die();
        SwitchPanel();
    }

    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        if (isHittable)
        {
            if (collision.tag == "Enemy")
            {
                Hit(1, collision.gameObject);
            }
        }
    }

    protected virtual void GetInput()
    {
        axisH = Input.GetAxisRaw("Horizontal");
        axisV = Input.GetAxisRaw("Vertical");
        inputDir = new Vector2(axisH, axisV).normalized;
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        ped.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(ped, results);
        if (results.Count > 0) mouseOnUI = true;
        else mouseOnUI = false;
    }

    protected virtual void Move()
    {
        moveDir = inputDir;
        Vector2 upperPoint = (Vector2)this.transform.position + Vector2.up * 0.4f;
        Vector2 lowerPoint = (Vector2)this.transform.position + Vector2.down * 0.4f;
        Vector2 rightPoint = (Vector2)this.transform.position + Vector2.right * 0.4f;
        Vector2 leftPoint = (Vector2)this.transform.position + Vector2.left * 0.4f;

        RaycastHit2D hitUpperX = Physics2D.Raycast(upperPoint, Vector2.right * axisH, 0.5f, LayerMask.GetMask("Wall"));
        RaycastHit2D hitCenterX = Physics2D.Raycast(this.transform.position, Vector2.right * axisH, 0.5f, LayerMask.GetMask("Wall"));
        RaycastHit2D hitLowerX = Physics2D.Raycast(lowerPoint, Vector2.right * axisH, 0.5f, LayerMask.GetMask("Wall"));
        RaycastHit2D hitRightY = Physics2D.Raycast(rightPoint, Vector2.up * axisV, 0.5f, LayerMask.GetMask("Wall"));
        RaycastHit2D hitCenterY = Physics2D.Raycast(this.transform.position, Vector2.up * axisV, 0.5f, LayerMask.GetMask("Wall"));
        RaycastHit2D hitLeftY = Physics2D.Raycast(leftPoint, Vector2.up * axisV, 0.5f, LayerMask.GetMask("Wall"));

        if (hitUpperX.collider != null || hitCenterX.collider != null || hitLowerX.collider) moveDir.x = 0;
        if (hitRightY.collider != null || hitCenterY.collider != null || hitLeftY.collider) moveDir.y = 0;
        if (isBind) moveDir = Vector2.zero;
        this.transform.Translate(moveDir.normalized * speed * Time.deltaTime);
    }

    protected virtual void View()
    {
        if (mousePos.x - this.transform.position.x > 0) render.flipX = true;
        if (mousePos.x - this.transform.position.x < 0) render.flipX = false;
    }

    public virtual void Hit(int damage, GameObject ObjInfo = null)
    {
        if (!isHittable) return;
        if (buffItems["Shield"])
        {
            buffItems["Shield"] = false;
            return;
        }
        isHittable = false;
        hp -= damage;
        SoundManager.instance.PlaySound("playerHit");
        if (ObjInfo != null) Debug.Log(ObjInfo.name);
        if (hp <= 0)
        {
            if(ObjInfo != null) GameManager.deathBy = ObjInfo.name.Replace("(Clone)", "");
            SceneChanger.ToGameOver();
            this.gameObject.SetActive(false);
        }
        if (damage > 0) hit = true;
        Invoke("HitOut", unHittableTime);
    }

    protected virtual void HitOut()
    {
        isHittable = true;
        render.color = Color.white;
        hitTimer = 0;
        hit = false;
    }

    protected virtual void ChangeSprite()
    {
        if (hit)
        {
            hitTimer += Time.deltaTime;
            render.color = new Color(1, 1, 1) * (Mathf.Cos(hitTimer * 18.5f) * 0.3f + 1) + new Color(0, 0, 0, 1);
        }
    }

    protected virtual void SwitchPanel()
    {
        if (Input.GetKeyDown(KeyCode.M)) minimapPanel.SetActive(!minimapPanel.activeSelf);
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            statusPanel.SetActive(!statusPanel.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            optionPanel.SetActive(!optionPanel.activeSelf);
            Time.timeScale = optionPanel.activeSelf ? 0 : 1;
        }
    }

    protected virtual void Attack() { }
    protected virtual void Skill1() { }
    protected virtual void Skill2() { }

    protected void BuffControl()
    {

    }
    public void Die()
    {
        if (hp <= 0)
        {
            isDie = true;
            if(isDie)
            {
                transform.rotation = Quaternion.Euler(0, 0, 270);
            }           
        }
    }
}