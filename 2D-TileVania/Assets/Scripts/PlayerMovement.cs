using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 5f;

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
    PlayerState playerState;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        PlayerState playerState = new PlayerState();
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
}
