using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    [SerializeField] private float walkSpeed = 10;
    public float xAxis,yAxis;
    [SerializeField] private float jumpForce=30;
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private float groundCheckY = 0.2f;
    [SerializeField] private float groundCheckX = 0.5f;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] public PlayerStateList pState;
    Animator anim;
    public static PlayerController Instance;
    public int currLvl = 1;
    private int jumpCounter;
    [SerializeField] private int maxJumpCounter;
    private int jumpBufferCounter;
    [SerializeField] private int jumpBufferFrames;

    [SerializeField]  private bool canDash=true;
    [SerializeField] private float dashTime, dashCoolDown, dashSpeed;
    float gravity;
    private bool dashed = false;
    [SerializeField] private GameObject dashEffect;
    [SerializeField] private GameObject AttackSpirit;

    [Header("Attack settings")]
    [SerializeField] bool attack = false;
    [SerializeField] private float timeBetweenAttack, timeSinceAttack;
    [SerializeField] private Transform sideAttackTransform;
    [SerializeField] private Vector2 sideAttackArea;
    [SerializeField] private LayerMask attackableLayer;
    [SerializeField] private float damage;


    [SerializeField] private int recoilXSteps=5, recoilYSteps=5;
    [SerializeField] private int stepsXrecoiled, stepsYrecoiled;
    [SerializeField] private float recoilXSpeed=100, recoilYSpeed=100;
    private float healTimer; 
    [SerializeField] private float timeToHeal;
    [Header("Mana Settings")]
    [SerializeField] UnityEngine.UI.Image manaStorage;
    [SerializeField] private float mana;
    [SerializeField] private float manaDrainSpeed;
    [SerializeField] private float manaGain;
     float Mana
    {
        get { return mana; }
        set
        {
            if (mana != value)
            {
                mana = Mathf.Clamp(value, 0, 1);
                manaStorage.fillAmount = mana;
            }
        }
    }

    public int Health
    {
        get { return health; }
        set
        {
            if (health != value)
            {
                health = Mathf.Clamp(value, 0, maxHealth);
                if(OnHealthChangeCallback != null)
                {
                    OnHealthChangeCallback.Invoke();
                }
            }
        }
    }
    [SerializeField] public int health, maxHealth;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private float flashSpeed;
    public delegate void OnHealthChangeDelegate();
    [HideInInspector] public OnHealthChangeDelegate OnHealthChangeCallback;
    [Header("jump on wall")]
    [SerializeField] private float wallSlidingSpeed=2f;    
    [SerializeField] private Transform wallCheckPoint;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float wallJumpingTime;
    [SerializeField] private Vector2 wallJumpingPower;
    float wallJumpingDirection;
    bool isWallJumping, isWallSliding;
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
        sr = GetComponent<SpriteRenderer>();
        pState = GetComponent<PlayerStateList>();
        rb = GetComponent<Rigidbody2D>();       
        anim = GetComponent<Animator>();
        gravity = rb.gravityScale;
        Mana = mana;
       if(manaStorage!=null) manaStorage.fillAmount = Mana;
    }

    // Update is called once per frame
    void Update()
    {
        checkHealth();
        getInputs();
        if(pState.cutScene) return;
        UpdateJumpVariables();
        if (pState.dashing) return;
        if (!isWallJumping)
        {
            Flip();
            Move();
            jump();
        }
        Heal();
        wallSlide();
        wallJumping();
        startDash();
        StartAttack();
        Recoil();
        FlashWhileInvincible();
    }
    void checkHealth()
    {
        if(Health <= 0)
        {
            Destroy(gameObject);

            SceneManager.LoadScene("LevelScene");
        }
    }
    void Flip()
    {
        if (xAxis < 0)
        {
            transform.localScale = new Vector3((float)0.5, transform.localScale.y, transform.localScale.z);
            pState.lookingRight = false;
        }
        else if(xAxis >0)
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
    private bool onWall()
    {

        return Physics2D.OverlapCircle(wallCheckPoint.position, 0.3f, wallLayer);
    }
    /*void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(wallCheckPoint.position, 0.3f);
    }*/
    void wallSlide()
    {
        if(onWall() && !onGround() && xAxis !=0)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y,-wallSlidingSpeed,float.MaxValue));
        }
        else
        {
            isWallSliding = false;
            rb.gravityScale = gravity;
        } 
    }
    void stopWallJumping()
    {
        isWallJumping = false;
    }
    void wallJumping()
    {
        if(isWallSliding)   
        {
            isWallJumping = false;  
            wallJumpingDirection = !pState.lookingRight ? 1 : -1;
            CancelInvoke(nameof(stopWallJumping));
        }
        if (Input.GetButtonDown("Jump") && isWallSliding)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingPower.x * wallJumpingDirection, wallJumpingPower.y);
            Invoke("stopWallJumping", wallJumpingTime);
            dashed = false;
            jumpCounter = 0;
            if ((pState.lookingRight && transform.eulerAngles.y == 0) || (!pState.lookingRight && transform.eulerAngles.y != 0))
            {
                pState.lookingRight = !pState.lookingRight;
                int yDirection = !pState.lookingRight ? 0 : 180;
                transform.eulerAngles = new Vector2(transform.eulerAngles.x, yDirection);
            }
            Invoke(nameof(stopWallJumping), wallJumpingTime);
        }
    }
    void Heal()
    {
        if (Input.GetButton("Heal") && Health < maxHealth && Mana > 0 && !pState.jumping && !pState.dashing)
        {
            pState.healing = true;

            //healing
            healTimer += Time.deltaTime;
            if (healTimer >= timeToHeal)
            {
                Health++;
                healTimer = 0;
            }
            Mana -= manaDrainSpeed * Time.deltaTime;
        }
        else
        {
            pState.healing = false;
            healTimer = 0;
        }
    }

    void FlashWhileInvincible()
    {
        sr.material.color = pState.invincible ? Color.Lerp(Color.white, Color.black, Mathf.PingPong(Time.time * flashSpeed, 1.0f)) : Color.white;    }

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
        rb.gravityScale = 0;
        rb.velocity =new Vector2(dashSpeed * (!pState.lookingRight ? -1:1), 0);
        Instantiate(dashEffect, transform);
        yield return new WaitForSeconds(dashTime);
        rb.gravityScale = gravity;
        pState.dashing = false;
        yield return new WaitForSeconds(dashCoolDown);
        canDash=true;
    }
    IEnumerator Attack()
    {
        anim.SetTrigger("Attack");
        GameObject spirit = Instantiate(AttackSpirit, transform.position, Quaternion.identity);
        if (pState.lookingRight)
        {
            spirit.transform.localScale = new Vector3(-3, 2, 1);
        }
        yield return new WaitForSeconds(0.5f);
        Hit(sideAttackTransform, sideAttackArea, ref pState.recoilingX, recoilXSpeed);
    }
    void StartAttack()
    {
        timeSinceAttack += Time.deltaTime;
        if(attack && timeSinceAttack >=timeBetweenAttack)
        {
            timeSinceAttack = 0;
        }
        if(attack && yAxis == 0 || yAxis < 0 && onGround())
        {
            StartCoroutine(Attack());
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(sideAttackTransform.position, sideAttackArea);
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
                if (objects[i].CompareTag("Enemy"))
                {
                    Mana += manaGain;
                }
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
    public void TakeDamage(int damage)
    {
        Health -= damage;
        StartCoroutine(StopTakingDamage());
    }
    
    public IEnumerator StopTakingDamage()
    {
        pState.invincible = true;
        yield return new WaitForSeconds(1);
        pState.invincible = false;
    }
    public IEnumerator WalkIntoScene(Vector2 exitDir,float delay)
    {
        if(exitDir.y > 0)
        {
            rb.velocity = jumpForce * exitDir;
        }
        if(exitDir.x != 0)
        {
            xAxis = exitDir.x > 0 ? 1:-1;
        }
        Flip();
        yield return new WaitForSeconds(delay);
        pState.cutScene = false;

    }
}
