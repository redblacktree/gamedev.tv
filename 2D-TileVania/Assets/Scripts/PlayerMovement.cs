using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D rb;
    Animator animator;
    CapsuleCollider2D bodyCollider;
    BoxCollider2D footCollider;
    
    int jumps = 0;
    struct PlayerMovementState
    {
        public bool isRunning;
        public bool isJumping;
        public bool isClimbing;
        public bool isStoppedClimbing;
        public bool isAlive;
    }
    PlayerMovementState playerState;

    [SerializeField] float speed = 5f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] float playerGravity = 5f;
    [SerializeField] int extraJumps = 1;
    [SerializeField] GameObject gun;
    [SerializeField] GameObject bullet;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = playerGravity;
        animator = GetComponent<Animator>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        footCollider = GetComponent<BoxCollider2D>();
    }

    void Start() {
        playerState.isAlive = true;
    }

    void Update()
    {
        if (!playerState.isAlive) { return; }
        Run();
        Climb();
        Animate();
        Die();
    }

    void Animate()
    {
        FlipSprite();
        animator.SetBool("IsRunning", playerState.isRunning);
        animator.SetBool("IsClimbing", playerState.isClimbing);
        animator.SetBool("IsStoppedClimbing", playerState.isStoppedClimbing);
    }

    void Run() 
    {
        rb.velocity = new Vector2(moveInput.x * speed, rb.velocity.y);
        playerState.isRunning = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        animator.SetBool("IsRunning", playerState.isRunning);
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    void Climb()
    {
        if (footCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")) && Mathf.Abs(moveInput.y) > Mathf.Epsilon)
        {
            rb.velocity = new Vector2(rb.velocity.x, moveInput.y * climbSpeed);
            if (moveInput.y == 0) 
            {
                playerState.isStoppedClimbing = true;                
            }
            else
            {
                playerState.isStoppedClimbing = false;
                playerState.isClimbing = true;
            }
        }
        else
        {
            playerState.isClimbing = false;
            playerState.isStoppedClimbing = false;
        }
        if (playerState.isClimbing || playerState.isStoppedClimbing) 
        {
            rb.gravityScale = 0;
        } 
        else 
        {
            rb.gravityScale = playerGravity;
        }              
    }

    void Die()
    {
        if (bodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards")))
        {
            playerState.isAlive = false;
            animator.SetTrigger("Die");
            rb.velocity = new Vector2(0f, 0f);
        }
    }

    void FlipSprite()
    {
        if (playerState.isRunning)
        {
            transform.localScale = new Vector2(Mathf.Sign(moveInput.x), 1);
        }
    }

    void OnMove(InputValue value)
    {
        if (!playerState.isAlive) { return; }
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (!playerState.isAlive) { return; }
        if (footCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            jumps = extraJumps;
            Jump();
        }
        else if (jumps > 0)
        {
            Jump();
            jumps--;
        }
    }

    void OnFire(InputValue value)
    {
        if (!playerState.isAlive) { return; }
        GameObject shot = Instantiate(bullet, gun.transform.position, transform.rotation);
        shot.transform.localScale = new Vector2(transform.localScale.x, 1f);
    }
}
