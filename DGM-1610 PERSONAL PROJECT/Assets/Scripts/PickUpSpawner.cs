using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
    private float zRange;
    private float xRange;

    private int randomPowerUp;

    public GameObject coin;

    public GameObject[] powerUps;
    // Start is called before the first frame update
    void Start()
    {
        //selects random float for ranges on x & z
        zRange = Random.Range(15, 29);
        xRange = Random.Range(15, 29);
    }

    // Update is called once per frame
    void Update()
    {
        //Selects a random power up
        randomPowerUp = Random.Range(0, 4);
    }

    private Vector3 SpawnPosition()
    {
        //Selects random floats and places them within a vector3
        float zSpawnRange = Random.Range(-zRange, zRange);
        float xSpawnRange = Random.Range(-xRange, xRange);
        Vector3 spawnPoint = new Vector3 (xSpawnRange, 0, zSpawnRange);
        return spawnPoint;
    }

    public void SpawnPickUps(int coinsToSpawn)
    {
        //For each round an additional coin will spawn
        for (int i = 0; i < coinsToSpawn ; i++)
        {
            Instantiate(coin, SpawnPosition(), coin.transform.rotation);
        }
    }

    public void SpawnPowerUps()
    {
        //Spawns one powerup per round
        Instantiate(powerUps[randomPowerUp], SpawnPosition(), powerUps[randomPowerUp].transform.rotation);
    }
}
