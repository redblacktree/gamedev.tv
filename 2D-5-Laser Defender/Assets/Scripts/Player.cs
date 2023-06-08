using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Shooter shooter;
    Vector2 rawInput;
    Vector2 minBounds;
    Vector2 maxBounds;
    Vector2 minPlayerBounds;
    Vector2 maxPlayerBounds;

    [SerializeField] float speed = 10f;
    [SerializeField] float paddingLeft = 0.5f;
    [SerializeField] float paddingRight = 0.5f;
    [SerializeField] float paddingTop = 13f;
    [SerializeField] float paddingBottom = 2.5f;

    void Awake()
    {
        shooter = GetComponent<Shooter>();
    }

    void Start()
    {
        InitBounds();
    }

    void Update()
    {
        Move();
    }

    void InitBounds()
    {
        Camera cam = Camera.main;
        minBounds = cam.ViewportToWorldPoint(new Vector2(0, 0));
        minPlayerBounds = minBounds + new Vector2(paddingLeft, paddingBottom);
        maxBounds = cam.ViewportToWorldPoint(new Vector2(1, 1));
        maxPlayerBounds = maxBounds - new Vector2(paddingRight, paddingTop);
    }

    private void Move()
    {
        Vector3 delta = rawInput * Time.deltaTime * speed;
        Vector3 newPos = transform.position + delta;
        newPos.x = Mathf.Clamp(newPos.x, minPlayerBounds.x, maxPlayerBounds.x);
        newPos.y = Mathf.Clamp(newPos.y, minPlayerBounds.y, maxPlayerBounds.y);
        transform.position = newPos;
    }

    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();        
    }

    void OnFire(InputValue value)
    {
        Debug.Log("OnFire");
        if (shooter != null)
        {
            Debug.Log("shooter != null");
            shooter.isFiring = value.isPressed;
        }
    }
}
