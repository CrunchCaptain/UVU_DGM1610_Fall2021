using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
    private float zRange;
    private float xRange;

    public GameObject coin;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        zRange = Random.Range(15, 29);
        xRange = Random.Range(15, 29);
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Vector3 SpawnPosition()
    {
        float zSpawnRange = Random.Range(-zRange, zRange);
        float xSpawnRange = Random.Range(-xRange, xRange);
        Vector3 spawnPoint = new Vector3 (xSpawnRange, 0, zSpawnRange);
        return spawnPoint;
    }

    public void SpawnPickUps(int coinsToSpawn)
    {
        for (int i = 0; i < coinsToSpawn ; i++)
        {
            Instantiate(coin, SpawnPosition(), coin.transform.rotation);
        }
    }
}
