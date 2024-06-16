using System.Collections.Generic;
using UnityEngine;


public class Explosive : MonoBehaviour
{
    public float hp = 9; // OakJar 오브젝트의 체력
    public float radius;
    public float damage;
    public GameObject effect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어 미사일과 충돌했을 때
        if (collision.gameObject.CompareTag("PlayerMissile"))
        {
            // OakJar의 체력 감소
            MissileController mLogic = collision.GetComponent<MissileController>();
            hp -= mLogic.damage;
            Debug.Log("OakJar HP: " + hp);

            // OakJar의 체력이 0 이하일 때
            if (hp <= 0)
            {
                Collider2D[] objects = Physics2D.OverlapCircleAll(this.transform.position, radius, LayerMask.GetMask("Wall"));
                foreach(Collider2D obj in objects)
                {
                    if (obj == null) continue;
                    Breakable objLogic = obj.GetComponent<Breakable>();
                    if(objLogic != null)
                    {
                        objLogic._hp -= damage;
                    }
                }
                Collider2D[] enemies = Physics2D.OverlapCircleAll(this.transform.position, radius, LayerMask.GetMask("Enemy"));
                foreach (Collider2D enemy in enemies)
                {
                    if (enemy == null) continue;
                    Enemy eLogic = enemy.GetComponent<Enemy>();
                    if (eLogic != null)
                    {
                        eLogic.Hit(damage);
                    }
                }

                Collider2D player = Physics2D.OverlapCircle(this.transform.position, radius, LayerMask.GetMask("Player"));
                if(player != null)
                {
                    player.GetComponent<Player>().Hit(2, this.gameObject);
                }
                // OakJar 오브젝트 파괴
                Destroy(gameObject);
                Instantiate(effect, this.transform.position, Quaternion.identity);
                SoundManager.instance.PlaySound("explosion");
                this.transform.parent.parent.parent.GetComponent<NodeMap>().GetNodeMap();
            }
        }
    }
}
