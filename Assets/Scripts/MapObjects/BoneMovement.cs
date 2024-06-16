using UnityEngine;

public class BoneMovement : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            this.gameObject.SetActive(false);
        }
        if (collision.tag == "Player")
        {
            Player pLogic = collision.GetComponent<Player>();
            pLogic.Hit(1, this.gameObject);
            this.gameObject.SetActive(false);
        }
    }
}