using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    private Rigidbody2D rb;
    public Vector2 direction;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        float directionFromEnemy = transform.position.x - PlayerController.Instance.transform.position.x;
        rb.velocity = new Vector2(direction.x * speed * (directionFromEnemy > 0 ? -1:1) , direction.y * speed);
    }
    void Uodate()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController.Instance.TakeDamage(1);
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.CompareTag("FireBall") || collision.gameObject.CompareTag("Enemy"))
        {

        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
