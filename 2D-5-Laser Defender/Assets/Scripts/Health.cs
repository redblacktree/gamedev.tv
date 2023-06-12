using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 100;
    [SerializeField] int score = 100;
    [SerializeField] bool isPlayer = false;
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] bool applyCameraShake = false;
    
    CameraShake cameraShake;
    AudioPlayer audioPlayer;
    ScoreKeeper scoreKeeper;
    LevelManager levelManager;

    void Awake() {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    public int GetHealth()
    {
        return health;
    }

    void OnTriggerEnter2D(Collider2D other) {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (damageDealer && !IsFriendlyFire(other, damageDealer.GetFriendlyFireLayers()))
        {
            TakeDamage(damageDealer.GetDamage());
            PlayHitEffect();
            ShakeCamera();
            damageDealer.Hit();
        }
    }

    public void TakeDamage(int damage)
    {        
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
        else 
        {
            audioPlayer.PlayHit(isPlayer);
        }
    }

    void Die() {
        audioPlayer.PlayExplosion(isPlayer);
        if (isPlayer)
        {
            levelManager.LoadGameOver();
            Destroy(gameObject);
        }
        else
        {
            scoreKeeper.AddToScore(score);
            Destroy(gameObject);
        }
    }

    public bool IsFriendlyFire(Collider2D other, List<LayerMask> friendlyFireLayers)
    {
        if (friendlyFireLayers != null)
        {
            foreach (LayerMask layer in friendlyFireLayers)
            {
                if (other.gameObject.layer == layer)
                {
                    return true;
                }
            }        
        }        
        if (other.gameObject.layer == gameObject.layer)
        {
            return true;
        }
        return false;
        
    }
    
    void PlayHitEffect()
    {
        if (hitEffect != null)
        {
            ParticleSystem instance = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

    void ShakeCamera() 
    {
        if (cameraShake != null && applyCameraShake)
        {
            cameraShake.Shake();
        }
    }
}
