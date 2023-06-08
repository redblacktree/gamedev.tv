using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    WaveConfigSO waveConfig;
    EnemySpawner enemySpawner;
    List<Transform> waypoints;
    int waypointIndex = 0;

    void Awake() {
        enemySpawner = FindObjectOfType<EnemySpawner>();        
    }

    void Start()
    {
        waveConfig = enemySpawner.GetCurrentWave();
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].position;        
    }

    void Update()
    {
        FollowPath();
    }

    void FollowPath() 
    {
        if (waypointIndex < waypoints.Count)
        {
            Vector2 targetPos = waypoints[waypointIndex].position;
            float delta = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPos, delta);
            if (transform.position.x == targetPos.x && transform.position.y == targetPos.y)            
            {
                waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
