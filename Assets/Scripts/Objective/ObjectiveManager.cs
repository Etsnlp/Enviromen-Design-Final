using UnityEngine;
using System.Collections;

public class ObjectiveManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private int totalEnemiesToSpawn = 16;

    [SerializeField] private GameObject reward;
    [SerializeField] private GameObject exit;

    private int enemiesSpawned;
    private int enemiesKilled;

    private void Start()
    {
        reward.SetActive(false);
        exit.SetActive(false);
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (enemiesSpawned < totalEnemiesToSpawn)
        {
            SpawnEnemy();
            enemiesSpawned++;
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        if (enemy.TryGetComponent(out Enemy enemyScript))
        {
            enemyScript.ObjectiveManager = this;
        }
    }

    public void EnemyKilled()
    {
        enemiesKilled++;
        if (enemiesKilled >= totalEnemiesToSpawn)
        {
            SpawnReward();
        }
    }

    private void SpawnReward()
    {
        reward.SetActive(true);
        exit.SetActive(true);
    }
}