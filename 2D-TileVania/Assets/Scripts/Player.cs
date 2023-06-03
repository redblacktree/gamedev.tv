using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D rb;
    Animator animator;
    struct PlayerState
    {
        public bool isRunning;
        public bool isJumping;
        public bool isFalling;
        public bool isAttacking;
        public bool isDead;
    }

    [SerializeField] float speed = 5f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] PlayerState playerState;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        UpdateState();
        Run();
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
        Jump();
    }
}
