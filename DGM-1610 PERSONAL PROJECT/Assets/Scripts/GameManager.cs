using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float lives = 3f;
    public int score;
    
    public int coinsLeft;
    public int round;
    public int coinCount;

    public bool gameOver = false;
    public bool gameActive = false;

    public GameObject mainMenu;
    public Button startButton;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI roundText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI coinsText;
    public GameObject hud;

    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI finalScoreText;
    public Button restartButton;

    public GameObject player;
    public GameObject enemy;
    private GameObject coins;
    public CameraFollow focalPoint;
    private PickUpSpawner pickUpSpawn;
    private EnemySpawner enemySpawn;
    public EnemyAI enemyAI;
    public ParticleSystem deathPart;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        pickUpSpawn = GameObject.FindWithTag("Pick Up Spawner").GetComponent<PickUpSpawner>();
        enemySpawn = GameObject.FindWithTag("Enemy Spawner").GetComponent<EnemySpawner>();
        coins = GameObject.FindWithTag("Coin");
    }

    // Update is called once per frame
    void Update()
    {
        StartGame();

        coinCount = FindObjectsOfType<CoinPickUps>().Length;
        
        
        coinUpdater(coinCount);
        //ends game if player runs out of lives
        if (lives < 1)
        {
            gameActive = false;
            enemyAI.speed = 5;
            Destroy(player.gameObject);
            Instantiate(deathPart, player.transform.position, deathPart.transform.rotation);
            GameOverDisplay();
           
        }
    }

    public void StartButton()
    {
        startButton.onClick.AddListener(StartGame);
        gameActive = true;

        focalPoint.transform.rotation = focalPoint.startY;
    }

    public void StartGame()
    {
        if (gameActive == true)
        {
            mainMenu.SetActive(false);
            hud.SetActive(true);
            player.GetComponent<Rigidbody>().useGravity = true;

            ScoreUpdater(0);
            LivesUpdater(0);
            if (coinCount == 0)
            {
                RoundUpdater(1);


                pickUpSpawn.SpawnPickUps(round);
                enemySpawn.SpawnEnemyWave(round);

            }
        }

        
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOverDisplay()
    {
        gameOverText.gameObject.SetActive(true);
        hud.gameObject.SetActive(false);
    }

    public void ScoreUpdater(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "SCORE: " + score;
        finalScoreText.text = "SCORE - " + score;
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
