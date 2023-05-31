using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    GameObject ground;
    SurfaceEffector2D chainLift;
    ParticleSystem crashEffect;
    bool dustTrailActive = false;
    bool canMove = true;
    bool crashed = false;
    AudioSource audioSource;
    [SerializeField] float torque = 20f;
    [SerializeField] float sceneReloadDelay = 2f;
    [SerializeField] float jumpForce = 350f;
    [SerializeField] float minimumSpeed = 15f;
    [SerializeField] float boostForce = 100f;
    [SerializeField] float dustTrailLifetimeMultiplier = 0.5f;
    [SerializeField] float dustTrailSpeedMultiplier = 0.1f;    
    [SerializeField] AudioClip crashSound;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ground = GameObject.Find("Ground");
        chainLift = ground.GetComponent<SurfaceEffector2D>();
        crashEffect = transform.GetChild(2).GetComponent<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        if (canMove) {
            Rotate();
            Jump();
            ChainLift();
            Boost();
        }        
    }

    private void Boost()
    {
        // if the player presses the boost button, add force to the player on the x axis        
        if (Input.GetButtonDown("Boost"))
        {
            rb.AddForce(new Vector2(boostForce, 0));
        }
    }

    private void ChainLift()
    {
        // if the player is moving too slowly, turn on surface effector
        if (rb.velocity.x < minimumSpeed)
        {
            chainLift.enabled = true;
        }
        else
        {
            chainLift.enabled = false;
        }
    }

    private void Jump()
    {
        // jump only if player is colliding with ground
        if (Input.GetButtonDown("Jump") && ground.GetComponent<Collider2D>().IsTouching(GetComponent<Collider2D>()))
        {
            // jump up relative to the world
            rb.AddForce(new Vector2(0, jumpForce));

        }
    }

    private void Rotate()
    {
        // if horizontal axis left, torque left
        if (Input.GetAxis("Horizontal") < 0)
        {
            rb.AddTorque(torque);
        }
        // if horizontal axis right, torque right
        if (Input.GetAxis("Horizontal") > 0)
        {
            rb.AddTorque(-torque);
        }
    }

    private void DisableControls() {
        canMove = false;        
    }

    public void EndRun() {
        Debug.Log("Run ended");
        DisableControls();
        Invoke("ReloadScene", sceneReloadDelay);
    }

    public void Crash() {
        if (!crashed) {
            crashed = true;
            Debug.Log("Crashed");
            DisableControls();
            Invoke("ReloadScene", sceneReloadDelay);
            crashEffect.Play();
            audioSource.PlayOneShot(crashSound);
            // stop all movement
            rb.velocity = Vector2.zero;
        }
    }

    void ReloadScene() {
        canMove = true;
        crashed = false;
        SceneManager.LoadScene("Level1");
    }
}
