using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private float zRange;
    private float xRange;

    public GameObject enemyPrefab;
    private EnemyAI enemyAI;

    // Start is called before the first frame update
    void Start()
    {
        zRange = Random.Range(12, 29);
        xRange = Random.Range(12, 29);

        enemyAI = enemyPrefab.GetComponent<EnemyAI>();
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    private Vector3 SpawnPosition()
    {
        float zSpawnPoint = Random.Range(-zRange, zRange);
        float xSpawnPoint = Random.Range(-xRange, xRange);
        Vector3 spawnPoint = new Vector3(xSpawnPoint, 0, zSpawnPoint);
        return spawnPoint;
    }

    public void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, SpawnPosition(), enemyPrefab.transform.rotation);
        }
            
    }
}
