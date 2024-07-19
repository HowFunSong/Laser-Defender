using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfSO> waveConfigs;
    [SerializeField] float timeBetweenWaves = 0f;
    [SerializeField] public bool isLooping = true;
    //waveConfig 有單一路徑 , 不同種類敵人
    WaveConfSO currentWaveConfig;
    
    
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }
    public WaveConfSO GetCurrentWave() 
    {
        return currentWaveConfig;
    }

    IEnumerator SpawnEnemies()
    {
        do
        {
            foreach (WaveConfSO wave in waveConfigs)
            {
                currentWaveConfig = wave;
                for (int i = 0; i < currentWaveConfig.GetEnemyCount(); i++)
                {
                    Instantiate(currentWaveConfig.GetEnemyPrefab(i),
                            currentWaveConfig.GetStartingWaypoint().position,
                            Quaternion.Euler(0,0,180),
                            transform);
                    yield return new WaitForSeconds(currentWaveConfig.GetRandomSpawnTime());
                }
                yield return new WaitForSeconds(timeBetweenWaves);
            }

        } while (isLooping);


    }

}
