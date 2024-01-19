using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Zombie : Enemy
{
    // Start is called before the first frame update
    protected override void Start()
    {
        rb.gravityScale = 12;
    }
    protected override void Awake()
    {
        base.Awake();
    }
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if(!isRecoiling)
        {
            transform.position = Vector2.MoveTowards(transform.position, PlayerController.Instance.transform.position, speed * Time.deltaTime);
        }
        // for flipping
        if (PlayerController.Instance.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (PlayerController.Instance.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
    public override void EnemyHit(float damage, Vector2 direction, float hitForce)
    {
        base.EnemyHit(damage, direction, hitForce);
    }
}
