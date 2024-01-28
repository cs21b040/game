using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : Enemy
{
    // Start is called before the first frame update
    private Animator animator;
    [SerializeField] private int x_low,x_high,y_low,y_high;
    [SerializeField] private float dashSpeed;
    [SerializeField] private GameObject fireBalls;
    [SerializeField] private GameObject teleportEffectPrefab;
    private bool transition = false;
    [SerializeField] private GameObject dealer;
    protected override void Start()
    {
        speed = 1f;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(BossBehavior());
    }
    // Update is called once per frame
    protected override void Update()
    {
        Vector3 enemyScale = transform.localScale;

        if (PlayerController.Instance.transform.position.x < transform.position.x)
        {
            enemyScale.x = -Mathf.Abs(enemyScale.x);
        }
        else
        {
            enemyScale.x = Mathf.Abs(enemyScale.x);
        }
        transform.localScale = enemyScale;
        if(health<=15 && !transition)
        {
            transition = true;
            speed = 0.5f;
            animator.SetTrigger("Evolve");
        }
        if (health <= 0f)
        {
            dealer.SetActive(true);
            Destroy(gameObject);
        }
    }
    public override void EnemyHit(float damage, Vector2 direction, float hitForce)
    {
        health -= damage;
        if (!isRecoiling)
        {
            rb.AddForce(-1 * direction * hitForce * recoilFactor);
            isRecoiling = true;
        }
    }
    void teleport(int x1,int y1,int x2,int y2)
    {
        Instantiate(teleportEffectPrefab, transform.position, Quaternion.identity);
        float x = Random.Range(x1, x2);
        float y = Random.Range(y1, y2);
        transform.position = new Vector3(x, y, 0); 
        Instantiate(teleportEffectPrefab, transform.position, Quaternion.identity);
    }
    void attack()
    {
        animator.SetBool("Idle", false);
        animator.SetBool("Dash", false);
        animator.SetBool("Attack", true);
        Vector3 temp = transform.position;
        GameObject fireBallsInstance = Instantiate(fireBalls, new Vector3(temp.x -2, temp.y, temp.z + 5), Quaternion.identity);
        Vector3 fireBallsScale = fireBallsInstance.transform.localScale;
        if (PlayerController.Instance.transform.position.x < temp.x)
        {
            fireBallsScale.x = -Mathf.Abs(fireBallsScale.x);
        }
        else
        {
            fireBallsScale.x = Mathf.Abs(fireBallsScale.x);
        }
        fireBallsInstance.transform.localScale = fireBallsScale;
    }
    void dashTowardsPlayer()
    {
        animator.SetBool("Idle", false);
        animator.SetBool("Attack", false);
        animator.SetBool("Dash", true);
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        Vector2 direction = (PlayerController.Instance.transform.position - transform.position).normalized;
        rb.velocity = direction * dashSpeed;
        }
    void setIdle()
    {
        animator.SetBool("Attack", false);
        animator.SetBool("Dash", false);
        animator.SetBool("Idle", true);
        rb.velocity = Vector2.zero;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController.Instance.TakeDamage(damage);
            rb.angularVelocity = 0f;
            rb.velocity = Vector2.zero;
            if(animator.GetBool("Dash"))
            {
                teleport(x_low, y_low, x_high, y_high);
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rb.angularVelocity = 0f;
            rb.velocity = Vector2.zero;
        }
    }
    IEnumerator BossBehavior()
    {
        while (true)
        {
            yield return new WaitForSeconds(speed);
            int temp = Random.Range(0, 4);
            if (temp == 0)
            {
                attack();
            }
            else if(temp == 1)
            {
                dashTowardsPlayer();
            }
            else if(temp == 2)
            {
                teleport(x_low, y_low, x_high, y_high);
            }
            else
            {
                setIdle();
            }
        }
    }
}
