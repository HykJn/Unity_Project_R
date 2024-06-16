using UnityEngine;

public enum LookDir
{
    Up, Down
}
public class ArrowTrap : ETCType
{
    public LookDir direction;
    SpriteRenderer render;
    Vector2 dir;
    public float attackDelay = 5f;

    private float timer = 0f;

    private void Awake()
    {
        render = GetComponent<SpriteRenderer>();
        if (direction == LookDir.Up)
        {
            render.flipY = true;
            dir = Vector2.up;
        }
        else
        {
            render.flipY = false;
            dir = Vector2.down;
        }
        timer = attackDelay;
    }

    void Update()
    {
        if (this.transform.parent.parent.parent.GetComponent<Room>().isClear) return;
        Activate();
    }

    public override void Activate()
    {
        timer += Time.deltaTime;
        if (timer >= attackDelay)
        {
            GameObject boneShoot = ObjectManager.instance.Activate("boneShot");
            boneShoot.transform.position = this.transform.position;
            if (direction == LookDir.Up) boneShoot.transform.position += Vector3.up * 1.1f;
            else boneShoot.transform.position += Vector3.down * 1.1f;
            SoundManager.instance.PlaySound("arrowTrap");
            boneShoot.GetComponent<Rigidbody2D>().AddForce(dir * 5f, ForceMode2D.Impulse);
            timer = 0;
        }
    }
}
