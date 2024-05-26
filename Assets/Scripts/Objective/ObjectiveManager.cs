using UnityEngine;
using System.Collections;

public class ObjectiveManager : MonoBehaviour
{
    public GameObject enemyPrefab; 
    public Transform[] spawnPoints;
    public float spawnInterval = 2f; 
    public int totalEnemiesToSpawn = 16; 

    private int enemiesSpawned = 0;
    private int enemiesKilled = 0;

    public GameObject reward; 

    void Start()
    {
        reward.SetActive(false); 
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (enemiesSpawned < totalEnemiesToSpawn)
        {
            SpawnEnemy();
            enemiesSpawned++;
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnEnemy()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        Enemy enemyScript = enemy.GetComponent<Enemy>();
        if (enemyScript != null)
        {
            enemyScript.objectiveManager = this; 
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

    void SpawnReward()
    {
        reward.SetActive(true); 
    }
}
