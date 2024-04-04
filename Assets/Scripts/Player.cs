using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Properties of Input
    protected float axisH;
    protected float axisV;
    protected Vector2 inputDir;
    protected Vector2 mousePos;

    //Properties of Movement
    protected Vector2 moveDir;
    public float speed;
    protected bool onWall = false;

    //Properties of Dodge
    public float dodgeCool;
    public float dodgeSpeed;
    public float dodgeTime;
    protected float dodgeTick;
    protected bool isDodging = false;
    protected Vector2 dodgeDir;

    //Properties of Hit & Attack
    public int hp;
    public float criticalChance;
    public float criticalDamage;
    public float power;
    public float attackSpeed;
    protected float attackTick;
    protected float hitTick;
    protected bool onHit = false;

    //Components
    protected SpriteRenderer render;
    protected Camera cam;
    protected Rigidbody2D rbody;

    protected virtual void Awake()
    {
        render = GetComponent<SpriteRenderer>();
        cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        rbody = GetComponent<Rigidbody2D>();
        attackTick = attackSpeed;
        dodgeTick = dodgeCool;
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        GetInput();
        View();
        Move();
        Dodge();
        Hit();
    }

    protected virtual void GetInput()
    {
        axisH = Input.GetAxisRaw("Horizontal");
        axisV = Input.GetAxisRaw("Vertical");
        inputDir = new Vector2(axisH, axisV);
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    protected virtual void Move()
    {
        if (isDodging) moveDir = dodgeDir;
        else moveDir = inputDir;
        RaycastHit2D hitX = Physics2D.Raycast(transform.position, new Vector2(axisH, 0), 0.5f, LayerMask.GetMask("Wall"));
        RaycastHit2D hitY = Physics2D.Raycast(transform.position, new Vector2(0, axisV), 0.5f, LayerMask.GetMask("Wall"));

        if (hitX.collider != null) moveDir.x = 0;
        if (hitY.collider != null) moveDir.y = 0;

        this.transform.Translate(moveDir.normalized * speed * Time.deltaTime);
    }

    protected virtual void View()
    {
        if (mousePos.x - this.transform.position.x > 0) render.flipX = true;
        if (mousePos.x - this.transform.position.x < 0) render.flipX = false;
    }

    protected virtual void Dodge()
    {
        dodgeTick += Time.deltaTime;
        if (!isDodging && dodgeTick >= dodgeCool && Input.GetKeyDown(KeyCode.Space))
        {
            dodgeDir = moveDir;
            speed *= dodgeSpeed;
            isDodging = true;
            Invoke("DodgeOut", dodgeTime);
        }
    }

    protected virtual void DodgeOut() 
    {
        speed /= dodgeSpeed;
        dodgeTick = 0;
        isDodging = false;
    }
    protected virtual void Attack() { }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (!onHit && !isDodging && collision.gameObject.tag == "Enemy")
        {
            hitTick = 0f;
            onHit = true;
            hp--;
            if (hp <= 0)
            {
                Destroy(this.gameObject);
            }
            Invoke("HitOut", 1f);
        }

        if(collision.gameObject.tag == "Wall")
        {
            onWall = true;
        }
    }

    protected virtual void Hit() 
    {
        if (onHit)
        {
            hitTick += Time.deltaTime;
            render.color = new Color(1, 1, 1) * (Mathf.Cos(hitTick * 18.5f) * 0.3f + 1) + new Color(0, 0, 0, 1);
        }
    }

    protected virtual void HitOut()
    {
        hitTick = 0;
        render.color = Color.white;
        onHit = false;
    }
}