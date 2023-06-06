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
        public bool isOnLadder;
        public bool isAlive;
    }
    PlayerMovementState playerState;

    [SerializeField] float speed = 5f;
    [SerializeField] float runDeadZone = 1f;
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
        animator.SetBool("IsOnLadder", playerState.isOnLadder);
    }

    void Run() 
    {
        float moveX = moveInput.x;
        // dampen X move speed if on a ladder, so that joystick controls are more forgiving
        if (playerState.isOnLadder)
        {
            moveX = moveInput.x * 0.5f;
        }
        rb.velocity = new Vector2(moveX * speed, rb.velocity.y);
        playerState.isRunning = Mathf.Abs(rb.velocity.x) > runDeadZone;
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    void Climb()
    {
        if (footCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")) && !footCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            playerState.isOnLadder = true;            
        }
        else
        {            
            playerState.isOnLadder = false;
        }
        if (footCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")) && Mathf.Abs(moveInput.y) > 0f)
        {
            playerState.isClimbing = true;            
        }
        else
        {            
            playerState.isClimbing = false;
        }


        if (playerState.isOnLadder)
        {
            rb.gravityScale = 0;
            if (playerState.isClimbing)
            {
                rb.velocity = new Vector2(rb.velocity.x, moveInput.y * climbSpeed);
            }
            else 
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }
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
        else 
        {
            Debug.Log("No Jumps");
        }
    }

    void OnFire(InputValue value)
    {
        if (!playerState.isAlive) { return; }
        GameObject shot = Instantiate(bullet, gun.transform.position, transform.rotation);
        shot.transform.localScale = new Vector2(transform.localScale.x, 1f);
    }
}
