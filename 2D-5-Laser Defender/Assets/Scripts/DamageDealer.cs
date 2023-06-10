using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damage = 100;
    [SerializeField] List<LayerMask> friendlyFireLayers;
    
    public int GetDamage()
    {
        return damage;
    }

    public void Hit()
    {
        Destroy(gameObject);
    }

    public List<LayerMask> GetFriendlyFireLayers()
    {
        return friendlyFireLayers;
    }
}
