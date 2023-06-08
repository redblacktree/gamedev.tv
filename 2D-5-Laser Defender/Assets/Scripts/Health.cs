using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 100;

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("OnTriggerEnter2D");
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (damageDealer)
        {
            TakeDamage(damageDealer.GetDamage());
            damageDealer.Hit();
        }
    }

    public void TakeDamage(int damage)
    {        
        Debug.Log("damage: " + damage);
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        Debug.Log("health: " + health);
    }
}
