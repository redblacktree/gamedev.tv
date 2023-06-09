using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLIfetime = 5f;
    [SerializeField] float fireRate = 0.3f;

    [Header("AI")]
    [SerializeField] bool useAI = false;
    [SerializeField] float aiFireRate = 1f;
    [SerializeField] float aiFireRateRandomFactor = 0.3f;
    [SerializeField] float aiFireRateMinimum = 0.2f;

    [HideInInspector] public bool isFiring = false;
    Coroutine fireCoroutine;

    void Start()
    {
        if (useAI)
        {
            isFiring = true;
        }
    }

    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (isFiring && fireCoroutine == null)
        {
            fireCoroutine = StartCoroutine(FireContinuously());
        }
        else if (!isFiring && fireCoroutine != null)
        {
            StopCoroutine(fireCoroutine);
            fireCoroutine = null;
        }        
    }

    IEnumerator FireContinuously()
    {
        while(isFiring)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile.GetComponent<Rigidbody2D>().velocity = transform.up * projectileSpeed;
            Destroy(projectile, projectileLIfetime);
            yield return new WaitForSeconds(GetFireRate());
        }
    }

    float GetFireRate()
    {
        if (useAI)
        {
            return Mathf.Clamp(aiFireRate + Random.Range(-aiFireRateRandomFactor, aiFireRateRandomFactor),
                               aiFireRateMinimum,
                               float.MaxValue);
        }
        else 
        {
            return fireRate;
        }
    }
}
