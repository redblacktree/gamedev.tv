using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] WaveConfigSO waveConfig;
    List<Transform> waypoints;
    int waypointIndex = 0;

    void Start()
    {
        waypoints = waveConfig.GetWaypoints();
        Debug.Log("waypoints: " + waypoints.Count);
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
            Debug.Log("waypointIndex: " + waypointIndex);
            Vector2 targetPos = waypoints[waypointIndex].position;
            float delta = waveConfig.GetMoveSpeed() * Time.deltaTime;
            Debug.Log("delta: " + delta);
            transform.position = Vector2.MoveTowards(transform.position, targetPos, delta);
            Debug.Log("transform.position: " + transform.position);
            Debug.Log("targetPos: " + targetPos);
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
