using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 100;
    [SerializeField] ParticleSystem hitEffect;

    [SerializeField] bool applyCameraShake = false;
    CameraShake cameraShake;

    void Awake() {
        cameraShake = Camera.main.GetComponent<CameraShake>();
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
