using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collision with coin");
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("Coin collided with player");
            FindObjectOfType<GameSession>().AddCoin();
            Destroy(gameObject);
        }
    }
}
