using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] protected float health,recoilLength,recoilFactor;
    [SerializeField] protected bool isRecoiling;
    protected float recoilTimer;
    protected Rigidbody2D rb;
    [SerializeField] protected PlayerController player;
    [SerializeField] protected float speed;
    [SerializeField] protected int damage;
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = PlayerController.Instance;
    }
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
        if (isRecoiling)
        {
            if (recoilTimer < recoilLength)
            {
                recoilTimer += Time.deltaTime;
            }
            else
            {
                isRecoiling = false;
                recoilTimer = 0;
            }
        }
    }
    public virtual void EnemyHit(float damage,Vector2 direction,float hitForce)
    {
        health -= damage;
        if(!isRecoiling)
        {
            rb.AddForce(-1 * direction * hitForce *recoilFactor);
            isRecoiling = true;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        
        if(collision.gameObject.CompareTag("Player") && !PlayerController.Instance.pState.invincible)
        {
            if(!isRecoiling)
            {
                Attack();
            }
        }
    }
    protected virtual void Attack()
    {
        PlayerController.Instance.TakeDamage(damage);
    }

}
