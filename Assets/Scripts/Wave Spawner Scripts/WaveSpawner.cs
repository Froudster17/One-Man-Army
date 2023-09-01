using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float timeBetweenEnemies = 1f;
    [SerializeField] private int enemiesPerWave = 5;

    private int waveNumber = 0;
    private int enemiesSpawned = 0;
    private bool isSpawning = false;

    private void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        while (true) // Infinite loop for endless waves
        {
            yield return new WaitForSeconds(timeBetweenWaves);
            waveNumber++;
            enemiesSpawned = 0;

            while (enemiesSpawned < enemiesPerWave)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(timeBetweenEnemies);
            }
        }
    }

    private void SpawnEnemy()
    {
        if (enemyPrefabs.Length == 0 || spawnPoints.Length == 0)
        {
            Debug.LogWarning("No enemy prefabs or spawn points defined!");
            return;
        }

        GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        if (enemyPrefab != null && spawnPoint != null)
        {
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            enemiesSpawned++;
        }
    }
}
