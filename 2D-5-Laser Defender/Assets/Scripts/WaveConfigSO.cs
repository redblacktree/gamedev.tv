using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave Config", fileName = "Wave Config")]
public class WaveConfigSO : ScriptableObject
{
    [SerializeField] GameObject pathPrefab;
    [SerializeField] List<GameObject> enemyPrefabs;
    [SerializeField] float timeBetweenSpawns = 0.5f;
    [SerializeField] float spawnRandomFactor = 0.3f;
    [SerializeField] float minimumSpawnDelay = 0.2f;
    [SerializeField] float moveSpeed = 5f;

    // get starting waypoint
    public Transform GetStartingWaypoint()
    {
        return pathPrefab.transform.GetChild(0);
    }

    public List<Transform> GetWaypoints()
    {
        List<Transform> waypoints = new List<Transform>();
        foreach (Transform child in pathPrefab.transform)
        {
            waypoints.Add(child);
        }
        return waypoints;
    }
    
    public float GetMoveSpeed()
    {
        return moveSpeed;
    }
    
    public float GetTimeBetweenSpawns()
    {
        return Mathf.Clamp(timeBetweenSpawns + Random.Range(-spawnRandomFactor, spawnRandomFactor), 
                           minimumSpawnDelay, 
                           float.MaxValue);        
    }

    public int GetEnemyCount()
    {
        return enemyPrefabs.Count;
    }

    public GameObject GetEnemyPrefab(int index)
    {
        return enemyPrefabs[index];
    }
}
