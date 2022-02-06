using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [Header("Object Variables")]
    public Rigidbody2D theRB;
    public Animator anim;
    SpriteRenderer theSR;

    [Header("Movement Variables")]
    public float moveSpeed;
    private float movX;
    public float jumpForce;
    private bool facingRight;

    [Header("GroundCheck Variables")]
    public LayerMask groundLayer;
    public Transform groundCheckPoint;
    private bool isGrounded;

    [Header("KnockBack Variables")]
    public float knockBackLength;
    [HideInInspector] public float knockBackForceX, knockBackForceY;
    private float knockBackCounter;

    [Header("Stamina and Sprint Variables")]
    public bool isSprinting;
    public int maxStamina = 100;
    private Coroutine regenStamina;
    public float currentStamina, sprintSpeed, refillSpeed, staminaCost;
    private float startSpeed;

    [Header("Wall Jump Variables")]
    public float wallJumpTime = 0.2f;
    public float wallJumpSpeed = 0.3f;
    public float wallDistance = 0.5f;
    private bool isWallSliding = false;
    private float jumpTime;
    private RaycastHit2D wallCheckHit;

    [Header("Whip Attack Variables")]
    public float timeBetweenWhips;
    private float whipCounter;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        currentStamina = maxStamina;
    }

    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        theSR = GetComponent<SpriteRenderer>();

        startSpeed = moveSpeed;
    }

    void Update()
    {
        if (!PauseMenu.instance.isPaused && !UIController.instance.isDead)
        {
            if (knockBackCounter <= 0)
            {
                //Detect Input for Horizontal Movement
                movX = Input.GetAxisRaw("Horizontal");

                //Function for Checking if Player is Grounded
                GroundCheck();

                //Whip Attack
                if (Input.GetButtonDown("Attack") && !isWallSliding && whipCounter <= 0)
                {
                    anim.SetTrigger("Attack");
                    whipCounter = timeBetweenWhips;
                }

                WhipCooldown();

                //Input for player to jump
                if (Input.GetButtonDown("Jump") && isGrounded || isWallSliding && Input.GetButtonDown("Jump"))
                {
                    Jump();
                }

                //WallJump 
                WallJump();

                //Input for player to sprint
                if ((Input.GetButton("Sprint") || Input.GetAxis("Sprint") > 0.05f) && movX != 0 && currentStamina > staminaCost)
                {
                    isSprinting = true;
                    moveSpeed = sprintSpeed;
                }
                else
                {
                    isSprinting = false;
                    moveSpeed = startSpeed;
                }

                if (Input.GetButtonDown("Heal") && GameManager.instance.currentSouls >= 50 &&
                PlayerHealthController.instance.currentHealth < PlayerHealthController.instance.maxHealth)
                {
                    PlayerHealthController.instance.RestoreHealth(PlayerHealthController.instance.maxHealth);
                    GameManager.instance.SubtractSouls();
                    Debug.Log("Healing");
                }

            }

            //Adding KnockBack to Player Depending on Direction
            else if (knockBackCounter > 0)
            {
                knockBackCounter -= Time.deltaTime;
                if (facingRight)
                    theRB.velocity = new Vector2(-knockBackForceX, knockBackForceY);

                else if (!facingRight)
                    theRB.velocity = new Vector2(knockBackForceX, knockBackForceY);
            }

            Animations();
        }
    }

    void FixedUpdate()
    {
        if (knockBackCounter <= 0)
        {
            theRB.velocity = new Vector2(movX * moveSpeed, theRB.velocity.y);
            Flip();

            if (isSprinting)
            {
                UseStamina();
            }

            if (isWallSliding)
            {
                theRB.velocity = new Vector2(theRB.velocity.x, Mathf.Clamp(theRB.velocity.y, wallJumpSpeed, float.MaxValue));
            }
        }
    }

    void GroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, groundLayer);
    }

    void Flip()
    {
        if (theRB.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            facingRight = false;
            wallCheckHit = Physics2D.Raycast(transform.position, new Vector2(-wallDistance, 0), wallDistance, groundLayer);
        }
        else if (theRB.velocity.x > 0)
        {
            transform.localScale = Vector3.one;
            facingRight = true;
            wallCheckHit = Physics2D.Raycast(transform.position, new Vector2(wallDistance, 0), wallDistance, groundLayer);
        }
    }

    void Jump()
    {
        theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
    }

    void WhipCooldown()
    {
        if (whipCounter > 0)
        {
            whipCounter -= Time.deltaTime;
        }
    }

    void WallJump()
    {
        if (wallCheckHit && !isGrounded && movX != 0)
        {
            isWallSliding = true;
            jumpTime = Time.time + wallJumpTime;
            theSR.flipX = true;
        }
        else if (jumpTime < Time.time)
        {
            isWallSliding = false;
            theSR.flipX = false;
        }
    }

    void UseStamina()
    {
        if (currentStamina - staminaCost >= 0)
        {
            currentStamina -= staminaCost;
            UIController.instance.UpdateStaminaUI();

            if (regenStamina != null)
            {
                StopCoroutine(regenStamina);
            }

            regenStamina = StartCoroutine(RegenStaminaCO());
        }
    }

    public IEnumerator RegenStaminaCO()
    {
        yield return new WaitForSeconds(1f);



        while (currentStamina < maxStamina)
        {
            currentStamina += maxStamina / 100;
            if (currentStamina >= maxStamina)
            {
                currentStamina = maxStamina;
            }
            UIController.instance.UpdateStaminaUI();
            yield return new WaitForSeconds(refillSpeed);
        }
    }

    void Animations()
    {
        anim.SetFloat("moveSpeed", Mathf.Abs(theRB.velocity.x));
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("wallGrabbing", isWallSliding);
    }

    public void KnockBack(int objectKnockBackX, int objectKnockBackY)
    {
        knockBackForceX = objectKnockBackX;
        knockBackForceY = objectKnockBackY;

        knockBackCounter = knockBackLength;
        theRB.velocity = Vector2.zero;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(groundCheckPoint.position, .2f);
    }
}
