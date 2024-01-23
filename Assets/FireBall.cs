using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private Vector2 direction;
    [SerializeField] private float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(direction.x * speed , direction.y * speed);
    }
    void Uodate()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            PlayerController.Instance.TakeDamage(1);
            Destroy(this.gameObject);
        }
        else if(collision.gameObject.CompareTag("FireBall"))
        {
            
        }
        else
        {
            Destroy(this.gameObject);
        }

    }
}
