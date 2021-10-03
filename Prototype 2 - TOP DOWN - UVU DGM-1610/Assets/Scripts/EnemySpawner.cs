using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private GameManager gameManager;
    public GameObject enemyPrefab;

    private float startDelay = 1f;
    private float spawnDelay = 3f;

    private int xTopRange;
    private float yTopRange = 5.75f;
    private Vector2 spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        //Retrieves gameManager script
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

        //Spawns enemies on a timer
        InvokeRepeating("SpawnEnemy", startDelay, spawnDelay);
    }

    void SpawnEnemy()
    {
        //Spawns enemies randomly above screen
        xTopRange = Random.Range(-8, 8);
        spawnPoint = new Vector2(xTopRange, yTopRange);

        //If the game isn't over, enemies keep spawning
        if (gameManager.gameOver == false)
            Instantiate(enemyPrefab, spawnPoint, enemyPrefab.transform.rotation);
    }
}
