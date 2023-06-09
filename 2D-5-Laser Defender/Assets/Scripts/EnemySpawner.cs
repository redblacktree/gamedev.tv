using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    WaveConfigSO currentWave;
    [SerializeField] List<WaveConfigSO> waveConfigs;
    [SerializeField] float timeBetweenWaves = 5f;
    [SerializeField] bool looping = true;

    void Start()
    {
        StartCoroutine(SpawnEnemyWaves());
    }

    // get current wave
    public void SetWaveConfig(WaveConfigSO waveConfig)
    {
        this.currentWave = waveConfig;
    }
    
    public WaveConfigSO GetCurrentWave()
    {
        return currentWave;
    }    

    IEnumerator SpawnEnemyWaves()
    {
        while(looping)
        {
            foreach (WaveConfigSO wave in waveConfigs)
            {
                currentWave = wave;
                Transform startingWaypoint = currentWave.GetStartingWaypoint();
                for (int i = 0; i < currentWave.GetEnemyCount(); i++)
                {
                    GameObject enemyPrefab = currentWave.GetEnemyPrefab(i);
                    GameObject enemy = Instantiate(enemyPrefab, 
                                                startingWaypoint.position, 
                                                Quaternion.Euler(0, 0, 180),
                                                transform);
                    yield return new WaitForSeconds(currentWave.GetTimeBetweenSpawns());
                }
                yield return new WaitForSeconds(timeBetweenWaves);
            }
        }
    }
}
