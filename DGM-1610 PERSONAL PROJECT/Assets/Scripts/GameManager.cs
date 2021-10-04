using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public float lives = 3f;
    public int score;
    
    public int coinsLeft;
    public int round;
    public int coinCount;

    public bool gameOver = false;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI roundText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI coinsText;

    private GameObject player;
    private GameObject enemy;
    private GameObject coins;
    private PickUpSpawner pickUpSpawn;
    private EnemySpawner enemySpawn;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        enemy = GameObject.FindWithTag("Enemy");
        pickUpSpawn = GameObject.FindWithTag("Pick Up Spawner").GetComponent<PickUpSpawner>();
        enemySpawn = GameObject.FindWithTag("Enemy Spawner").GetComponent<EnemySpawner>();
        coins = GameObject.FindWithTag("Coin");

        ScoreUpdater(0);
        LivesUpdater(0);
    }

    // Update is called once per frame
    void Update()
    {
        coinCount = FindObjectsOfType<CoinPickUps>().Length;
        
        if (coinCount == 0)
        {
            RoundUpdater(1);

            pickUpSpawn.SpawnPickUps(round);
            enemySpawn.SpawnEnemyWave(round);
        }
        coinUpdater(coinCount);
        //ends game if player runs out of lives
        if (lives < 1)
        {
            gameOver = true;
        }
        GameOver();
    }

    void GameOver()
    {
        if (gameOver == true)
        {
            Destroy(player.gameObject);
            Destroy(enemy.gameObject);
        }
    }

    public void ScoreUpdater(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "SCORE: " + score;
    }

    public void LivesUpdater(int livesLeft)
    {
        lives += livesLeft;
        livesText.text = "LIVES: " + lives;
    }

    public void RoundUpdater(int currentRound)
    {
        round += currentRound;
        roundText.text = "ROUND: " + round;
    }

    public void coinUpdater(int coinInRound)
    {
        coinCount = coinInRound;
        coinsText.text = "COINS LEFT: " + coinCount;
    }
}
