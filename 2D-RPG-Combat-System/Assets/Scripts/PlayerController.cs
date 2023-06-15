using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool FacingLeft { get => facingLeft; set => facingLeft = value; }

    [SerializeField] private float moveSpeed = 1f;

    private PlayerControls playerControls;
    private Vector2 moveInput;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private bool facingLeft = false;

    private void Awake()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Update()
    {
        PlayerInput();        
    }

    private void FixedUpdate() {
        
        Move();
        AdjustPlayerFacingDirection();
    }

    private void Move()
    {
        rb.MovePosition(rb.position + moveInput * (moveSpeed * Time.fixedDeltaTime));
    }

    private void PlayerInput()
    {
        moveInput = playerControls.Movement.Move.ReadValue<Vector2>();

        anim.SetFloat("moveX", moveInput.x);
        anim.SetFloat("moveY", moveInput.y);
    }

    private void AdjustPlayerFacingDirection()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 playerPos = transform.position;
        
        if (mousePos.x < playerPos.x)
        {
            spriteRenderer.flipX = true;
            FacingLeft = true;
        }
        else if (mousePos.x > playerPos.x)
        {
            spriteRenderer.flipX = false;
            FacingLeft = false;
        }
    }
}
