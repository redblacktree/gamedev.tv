using UnityEngine;

class CustomMath
{
    public static float GetPerlinNoise(float seed = 0, float time = 0)
    {
        if (seed == 0)
        {
            seed = Random.Range(0, 1);
        }
        if (time == 0)
        {
            time = Time.time;
        }
        float noise = Mathf.Clamp01(Mathf.PerlinNoise(seed, time));
        return noise;
    }

    public static float GetSignedPerlinNoise(float seed = 0, float time = 0)
    {
        float noise = GetPerlinNoise(seed, time);
        return noise * 2 - 1;
    }
}