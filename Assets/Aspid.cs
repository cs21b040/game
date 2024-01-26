using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aspid : Enemy
{
    // Start is called before the first frame update
    [SerializeField] private float delay;
    private float delayTimer;
    [SerializeField] private GameObject FireBall;
    protected override void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        delayTimer = delay;
    }

    // Update is called once per frame
    protected override void Awake()
    {
        base.Awake();
    }
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        Vector2 direction = (PlayerController.Instance.transform.position - transform.position).normalized;
        int temp = 0;
        if(direction.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
            temp = 1;
        }
        if(health <= 0)
        {
            Destroy(this.gameObject);
        }
        if(delayTimer > 0)
        {
            delayTimer -= Time.deltaTime;
        }
        else
        {
            delayTimer = delay;
            GameObject fireBallInstance = Instantiate(FireBall, transform.position, Quaternion.identity);
            FireBall fireBallScript = fireBallInstance.GetComponent<FireBall>();
            fireBallScript.direction = direction;
            if(temp == 1)
            {
                fireBallScript.direction.x*=-1;
            }
        }

    }
}
