using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D rb;
    Animator animator;
    CapsuleCollider2D capsuleCollider;
    BoxCollider2D footCollider;
    int jumps = 0;
    struct PlayerState
    {
        public bool isRunning;
        public bool isJumping;
        public bool isClimbing;
        public bool isStoppedClimbing;
        public bool isFalling;
        public bool isAttacking;
        public bool isDead;
    }

    [SerializeField] float speed = 5f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] PlayerState playerState;
    [SerializeField] float playerGravity = 5f;
    [SerializeField] int extraJumps = 1;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = playerGravity;
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        footCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        UpdateState();
        Run();
        Climb();
        Animate();
    }

    void UpdateState()
    {
        playerState.isRunning = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;;
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

    void FlipSprite()
    {
        if (playerState.isRunning)
        {
            transform.localScale = new Vector2(Mathf.Sign(moveInput.x), 1);
        }
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
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
}
