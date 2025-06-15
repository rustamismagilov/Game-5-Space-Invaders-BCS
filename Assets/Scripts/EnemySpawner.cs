using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<WaveConfigSO> waveConfigs;
    [SerializeField] private float timeBetweenWaves = 1f;
    private WaveConfigSO currentWave;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnEnemyWaves()); // StartCoroutine() will be started once the game starts and will continiously execute the SpawnEnemyWaves() method until new condition will be met (in this case until list of WaveConfig's will run out of wave configs)
    }

    // Update is called once per frame
    void Update()
    {
        // if youre going to execute StartCoroutine in Update, it will be executed every frame (which means new enemy will be spawned every frame), which is not what we want here
    }

    public WaveConfigSO GetCurrentWave() //get current wave
    {
        return currentWave;
    }

    IEnumerator SpawnEnemyWaves()//spawn correct enemy at first waypoint in path with default rotation as a child of the EnemySpawner object
    {
        foreach (WaveConfigSO wave in waveConfigs)
        {
            currentWave = wave;

            for (int i = 0; i < currentWave.GetEnemyCount(); i++)
            {
                Instantiate(currentWave.GetEnemyPrefab(i),
                    currentWave.GetStartingWaypoint().position,
                    Quaternion.identity,
                    transform);

                yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
            }

            yield return new WaitForSeconds(timeBetweenWaves);

        }
    }
}
