using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    [SerializeField] private float walkSpeed = 10;
    private float xAxis,yAxis;
    [SerializeField] private float jumpForce=30;
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private float groundCheckY = 0.2f;
    [SerializeField] private float groundCheckX = 0.5f;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] public PlayerStateList pState;
    Animator anim;
    public static PlayerController Instance;

    private int jumpCounter;
    [SerializeField] private int maxJumpCounter;
    private int jumpBufferCounter;
    [SerializeField] private int jumpBufferFrames;

    [SerializeField]  private bool canDash=true;
    [SerializeField] private float dashTime, dashCoolDown, dashSpeed;
    float gravity;
    private bool dashed = false;
    [SerializeField] private GameObject dashEffect;

    [Header("Attack settings")]
    [SerializeField] bool attack = false;
    [SerializeField] private float timeBetweenAttack, timeSinceAttack;
    [SerializeField] private Transform sideAttackTransform, upAttackTransform;
    [SerializeField] private Vector2 sideAttackArea, upAttackArea;
    [SerializeField] private LayerMask attackableLayer;
    [SerializeField] private float damage;


    [SerializeField] private int recoilXSteps=5, recoilYSteps=5;
    [SerializeField] private int stepsXrecoiled, stepsYrecoiled;
    [SerializeField] private float recoilXSpeed=100, recoilYSpeed=100;
    private void Awake()
    {
        if(Instance !=null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    void Start()
    { 
        jumpCounter = 0;
        pState = GetComponent<PlayerStateList>();
        rb = GetComponent<Rigidbody2D>();       
        anim = GetComponent<Animator>();
        gravity = rb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        getInputs();
        UpdateJumpVariables();
        if (pState.dashing) return;
        Flip();
        Move();
        jump();
        startDash();
        Attack();
        Recoil();
    }
    void Flip()
    {   
        if(xAxis < 0)
        {
            transform.localScale = new Vector3((float)0.5, transform.localScale.y, transform.localScale.z);
            pState.lookingRight = false;
        }
        else
        {
            transform.localScale = new Vector3((float)-0.5, transform.localScale.y, transform.localScale.z);
            pState.lookingRight = true;
        }
    }
    void getInputs()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        yAxis = Input.GetAxisRaw("Vertical");
        attack = Input.GetButtonDown("Attack");
    }
    private void Move()
    {
        rb.velocity = new Vector2 (xAxis * walkSpeed, rb.velocity.y);
        anim.SetBool("Walking", rb.velocity.x!=0 && onGround());
    }
    private bool onGround()
    {
        if (Physics2D.Raycast(groundCheckPoint.position, Vector2.down, groundCheckY, whatIsGround)){
            jumpCounter = 0;
            return true;
        }
        else if(Physics2D.Raycast(groundCheckPoint.position + new Vector3(groundCheckX,0,0), Vector2.down, groundCheckY, whatIsGround)
            && Physics2D.Raycast(groundCheckPoint.position - new Vector3(groundCheckX, 0, 0), Vector2.down, groundCheckY, whatIsGround))
        {
            jumpCounter = 0;
            return true;
        }
        return false;

    }
    void jump()
    {
        if (Input.GetButtonDown("Jump") && rb.velocity.y >0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            pState.jumping = false;
        }
        if(!pState.jumping)
        {
            if(jumpBufferCounter > 0 && onGround())
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                pState.jumping = true;
            }
            else if(!onGround() && jumpCounter < maxJumpCounter && Input.GetButtonDown("Jump")) {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                pState.jumping = true;
                jumpCounter++;
            }
        }
    }
    void UpdateJumpVariables()
    {
        if (onGround())
        {
            pState.jumping = false;
        }
        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferFrames;
        }
        else
        {
            jumpBufferCounter--;
        }
    }
    void startDash()
    {
        if(Input.GetButtonDown("Dash") && canDash && !dashed)
        {
            StartCoroutine(Dash());
            dashed = true;
        }
        if (onGround())
        {
            dashed = false;
        }
    }
    IEnumerator Dash()
    {
        canDash = false;
        pState.dashing = true;
        anim.SetBool("Dashing", true);
        rb.gravityScale = 0;
        rb.velocity =new Vector2((rb.velocity.x+1) * dashSpeed, 0);
        Instantiate(dashEffect, transform);
        yield return new WaitForSeconds(dashTime);
        rb.gravityScale = gravity;
        pState.dashing = false;
        yield return new WaitForSeconds(dashCoolDown);
        canDash=true;
    }
    void Attack()
    {
        timeSinceAttack += Time.deltaTime;
        if(attack && timeSinceAttack >=timeBetweenAttack)
        {
            timeSinceAttack = 0;
        }
        if(attack && yAxis == 0 || yAxis < 0 && onGround())
        {
            Hit(sideAttackTransform,sideAttackArea,ref pState.recoilingX,recoilXSpeed);
        }
        else if(attack && yAxis > 0)
        {
            Hit(upAttackTransform,upAttackArea,ref pState.recoilingY,recoilYSpeed);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(sideAttackTransform.position, sideAttackArea);
        Gizmos.DrawWireCube(upAttackTransform.position, upAttackArea);
    }
    private void Hit(Transform attackTransform,Vector2 attackArea,ref bool recoilDirection,float recoilStrength)
    {
        Collider2D[] objects = Physics2D.OverlapBoxAll(attackTransform.position, attackArea,0, attackableLayer);
        if(objects.Length > 0)
        {
            recoilDirection = true;
        }
        for(int i = 0; i < objects.Length; i++)
        {
            if (objects[i].GetComponent<Enemy>() != null)
            {
                objects[i].GetComponent<Enemy>().EnemyHit(damage,(transform.position - objects[i].transform.position).normalized,recoilStrength);
            }
        }
    }
    private void Recoil()
    {
        if(pState.recoilingX)
        {
            if(pState.lookingRight)
            {
                rb.velocity = new Vector2(-recoilXSpeed,0);
            }
            else
            {
                rb.velocity = new Vector2(recoilXSpeed,0);
            }
        }
        if (pState.recoilingY)
        {
            rb.gravityScale = 0;
            if(yAxis < 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, -recoilYSpeed);
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, recoilYSpeed);
            }
            jumpCounter = 0;
        }
        else
        {
            rb.gravityScale = gravity;
        }
        //stop recoil
        if( pState.recoilingX && stepsXrecoiled < recoilXSteps)
        {
            stepsXrecoiled++;
        }
        else
        {
            StopRecoilX();
        }
        if (pState.recoilingY && stepsYrecoiled < recoilYSteps)
        {
            stepsYrecoiled++;
        }
        else
        {
            StopRecoilY();
        }
        if (onGround())
        {
            StopRecoilY();
        }


    }
    void StopRecoilX()
    {
        stepsXrecoiled = 0;
        pState.recoilingX = false;

    }
    void StopRecoilY()
    {
        stepsYrecoiled = 0;
        pState.recoilingY = false;

    }
}
