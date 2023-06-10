using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CustomMath;

public class CameraShake : MonoBehaviour
{
    [SerializeField] float duration = 0.5f;
    [SerializeField] float magnitude = 0.25f;
    [SerializeField] float maxAngle = 5f;

    Vector3 originalPosition;
    Quaternion originalRotation;
    float trauma;


    public void Shake()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        StartCoroutine(ShakeRoutine());
    }

    IEnumerator ShakeRoutine()
    {
        Vector3 originalPosition = transform.position;
        float elapsed = 0f;
        float seed = Random.Range(0, 1);
        while (elapsed < duration)
        {
            var noise = GetSignedPerlinNoise();
            float angle = noise * maxAngle * magnitude;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            transform.position = new Vector3(originalPosition.x + noise * magnitude,
                                             originalPosition.y + noise * magnitude,
                                             originalPosition.z);
            elapsed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.rotation = originalRotation;
        transform.position = originalPosition;
    }

    void Start()
    {
        
    }
}
