using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [Header("Object Variables")]
    public Rigidbody2D theRB;
    public Animator anim;

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
    public float knockBackForce;
    private float knockBackCounter;

    [Header("Stamina and Sprint Variables")]
    public bool isSprinting;
    public int maxStamina = 100;
    public float currentStamina;
    private Coroutine regenStamina;
    public float startSpeed, sprintSpeed, refillSpeed, staminaCost;

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

                //Input for player to jump
                if (Input.GetButtonDown("Jump") && isGrounded)
                {
                    Jump();
                }

                //Input for player to sprint
                if (Input.GetButton("Sprint") && theRB.velocity.x != 0 && currentStamina > staminaCost)
                {
                    isSprinting = true;
                    moveSpeed = sprintSpeed;
                }
                else
                {
                    isSprinting = false;
                    moveSpeed = startSpeed;
                }

            }

            //Adding KnockBack to Player Depending on Direction
            else if (knockBackCounter > 0)
            {
                knockBackCounter -= Time.deltaTime;
                if (facingRight)
                    theRB.velocity = new Vector2(-knockBackForce, theRB.velocity.y);

                else if (!facingRight)
                    theRB.velocity = new Vector2(knockBackForce, theRB.velocity.y);
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
        }
        else if (theRB.velocity.x > 0)
        {
            transform.localScale = Vector3.one;
            facingRight = true;
        }
    }

    void Jump()
    {
        theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
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
            UIController.instance.UpdateStaminaUI();
            yield return new WaitForSeconds(refillSpeed);
        }
    }

    void Animations()
    {
        anim.SetFloat("moveSpeed", Mathf.Abs(theRB.velocity.x));
        anim.SetBool("isGrounded", isGrounded);
    }

    public void KnockBack()
    {
        knockBackCounter = knockBackLength;
        theRB.velocity = new Vector2(0f, knockBackForce);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(groundCheckPoint.position, .2f);
    }
}
