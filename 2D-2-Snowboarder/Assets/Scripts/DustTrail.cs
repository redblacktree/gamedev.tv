using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustTrail : MonoBehaviour
{
    [SerializeField] ParticleSystem dustTrail;
    [SerializeField] float dustTrailLifetimeMultiplier = 0.5f;
    [SerializeField] float dustTrailSpeedMultiplier = 0.1f;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (rb.velocity.x > 0)
        {
            var main = dustTrail.main;
            main.startLifetimeMultiplier = rb.velocity.x * dustTrailLifetimeMultiplier;
            main.startSpeedMultiplier = rb.velocity.x * dustTrailSpeedMultiplier;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Ground") {
            dustTrail.Play();
        }
    }   

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.tag == "Ground") {
            dustTrail.Stop();
        }
    }
}
