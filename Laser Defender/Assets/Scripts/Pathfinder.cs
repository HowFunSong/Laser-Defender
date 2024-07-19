using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    EnemySpawner enemySpawner;
    WaveConfSO waveConfig;
    List<Transform> waypoints;
    int waypointIndex = 0;

    private void Awake()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
        // pathfinder 掛在 enemy下 , 利用 findobjectoftype 可以往上找到最近的WaveConfSO
        waveConfig = enemySpawner.GetCurrentWave();

        //  check enemySpawner is empty
        //if (enemySpawner != null)
        //{
        //    Debug.Log("EnemySpawner found: " + enemySpawner.name);
        //}
        //else
        //{
        //    Debug.LogWarning("EnemySpawner not found.");
        //}

        //// check waveConfig is empty
        //if (waveConfig != null)
        //{
        //    Debug.Log("WaveConfSO found: " + waveConfig.name);
        //}
        //else
        //{
        //    Debug.LogWarning("WaveConfSO not found.");
        //}
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

    private void FollowPath()
    {
        if (waypointIndex < waypoints.Count)
        {
            Vector3 targetPosition = waypoints[waypointIndex].position;
            float delta = waveConfig.GetMoveSpeed() * Time.deltaTime;
            //會計算出一個差值, 讓每一禎移動一點點
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta);
            if (transform.position == targetPosition)
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
