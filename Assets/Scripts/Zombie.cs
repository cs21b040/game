using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Enemy
{
    // Start is called before the first frame update
    void Start()
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
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }
    public override void EnemyHit(float damage, Vector2 direction, float hitForce)
    {
        base.EnemyHit(damage, direction, hitForce);
    }
}
