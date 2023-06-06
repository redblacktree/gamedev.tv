using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    PlayerMovement player;
    float xSpeed;

    [SerializeField] float speed = 10f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>(); 
    }

    void Start() 
    {
        xSpeed = player.transform.localScale.x * speed;
    }

    void Update()
    {        
        rb.velocity = new Vector2(xSpeed, 0f);        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other) {
        Destroy(gameObject);
    }
}
