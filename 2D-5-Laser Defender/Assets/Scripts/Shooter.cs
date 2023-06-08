using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLIfetime = 5f;
    [SerializeField] float fireRate = 0.1f;

    public bool isFiring = false;
    Coroutine fireCoroutine;

    void Start()
    {
        
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
        else if (fireCoroutine != null)
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
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            Destroy(projectile, projectileLIfetime);
            yield return new WaitForSeconds(fireRate);
        }
    }
}
