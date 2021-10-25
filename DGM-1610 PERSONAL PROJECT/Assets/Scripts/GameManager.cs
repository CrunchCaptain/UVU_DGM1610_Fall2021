using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] hearts;
    public int lives;
    public int score;
    
    public int coinsLeft;
    public int round = 0;
    public int coinCount;

    public bool gameOver = false;
    public bool gameActive = false;

    public GameObject mainMenu;
    public Button startButton;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI roundText;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI rocketsText;
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
    public PlayerController playerScript;
    public ParticleSystem deathPart;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        pickUpSpawn = GameObject.FindWithTag("Pick Up Spawner").GetComponent<PickUpSpawner>();
        enemySpawn = GameObject.FindWithTag("Enemy Spawner").GetComponent<EnemySpawner>();
        coins = GameObject.FindWithTag("Coin");
        lives = hearts.Length;
    }

    // Update is called once per frame
    void Update()
    {
        StartGame();

        coinCount = FindObjectsOfType<CoinPickUps>().Length;
        
        
        coinUpdater(coinCount);
    }

    public void StartButton()
    {
        startButton.onClick.AddListener(StartGame);
        gameActive = true;
        enemyAI.speedMin = 10f;
        enemyAI.speedMax = 30f;

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
            LivesUpdater();
            rocketUpdater(0);
            if (coinCount == 0)
            {
                RoundUpdater(1);


                pickUpSpawn.SpawnPickUps(round);
                enemySpawn.SpawnEnemyWave(round);
                enemyAI.speedMin += 1;
                enemyAI.speedMax += 1;
                pickUpSpawn.SpawnPowerUps();
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
        scoreText.text = ": " + score;
        finalScoreText.text = "SCORE - " + score;
    }

    public void LivesUpdater()
    {
        if (lives < 1) //ends game if player runs out of lives
        {
            Destroy(hearts[2].gameObject);
            gameActive = false;
            gameOver = true;
            Destroy(player.gameObject);
            Instantiate(deathPart, player.transform.position, deathPart.transform.rotation);
            GameOverDisplay();
        }
        if (lives < 2)
        {
            Destroy(hearts[1].gameObject);
        }
        if (lives < 3)
        {
            Destroy(hearts[0].gameObject);
        }
        
        
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

    public void rocketUpdater(int rocketsLeft)
    {
        rocketsLeft = playerScript.rocketAmount;
        rocketsText.text = "Rockets: " + playerScript.rocketAmount;

        if (playerScript.rocketAmount == 6)
        {
            rocketsText.text = "Rockets: Full";
        }
    }
}
