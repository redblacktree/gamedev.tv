using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;
    BoxCollider2D reversePeriscope;

    [SerializeField] float speed = 1f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        reversePeriscope = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        rb.velocity = new Vector2(speed, 0f); 
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            speed = -speed;
            transform.localScale = new Vector2(Mathf.Sign(speed), 1f);
        }        
    }
}
