using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Game Status")]
    public bool gameOver = false;
    public bool gameActive = true;
    public bool isPaused = false;

    [Header("UI Counts")]
    public GameObject[] hearts;
    public int lives;
    public int score;   
    public int coinsLeft;
    public int round = 1;
    public int coinCount;

    [Header("UI HUD")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI roundText;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI rocketsText;
    public GameObject hud;

    [Header("UI Game Over")]
    public GameObject gameOverUI;
    public TextMeshProUGUI finalScoreText;
    public Button mainMenuButton;
    public Button restartButton;

    [Header("UI Pause Screen")]
    public GameObject pauseScreen;

    [Header("GameObjects")]
    public GameObject player;
    public GameObject enemy;
    private GameObject coins;

    [Header("Scripts/Part")]
    public CameraFollow focalPoint;
    private PickUpSpawner pickUpSpawn;
    private EnemySpawner enemySpawn;
    public EnemyAI enemyAI;
    public PlayerController playerScript;
    public ParticleSystem deathPart;

    private static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        player = GameObject.FindWithTag("Player");
        pickUpSpawn = GameObject.FindWithTag("Pick Up Spawner").GetComponent<PickUpSpawner>();
        enemySpawn = GameObject.FindWithTag("Enemy Spawner").GetComponent<EnemySpawner>();
        coins = GameObject.FindWithTag("Coin");
        lives = hearts.Length;

        hud.SetActive(true);
        player.GetComponent<Rigidbody>().useGravity = true;
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //StartGame();

        coinCount = FindObjectsOfType<CoinPickUps>().Length;
        
        
        coinUpdater(coinCount);
        ScoreUpdater(0);
        LivesUpdater();
        rocketUpdater(0);

        //when coin count hits 0 game goes up a round
        if (coinCount == 0)
        {
            RoundUpdater(1);


            pickUpSpawn.SpawnPickUps(round);
            enemySpawn.SpawnEnemyWave(round);
            enemyAI.speedMin += 1;
            enemyAI.speedMax += 1;
            pickUpSpawn.SpawnPowerUps();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
            PauseScreenDisplay();
    }

    public void RestartGame() //Restarts game
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Coin Thief Menu");
    }

    public void GameOverDisplay() //Gameover UI display
    {
        gameOverUI.SetActive(true);
        hud.gameObject.SetActive(false);

        Cursor.lockState = gameOver == true ? CursorLockMode.Confined : CursorLockMode.Locked;
    }

    public void PauseScreenDisplay()
    {
        isPaused = !isPaused;

        Time.timeScale = isPaused == true ? 0.0f : 1.0f;

        Cursor.lockState = isPaused == true ? CursorLockMode.Confined : CursorLockMode.Locked;

        if (isPaused == true)
        {
            hud.SetActive(false);
            pauseScreen.SetActive(true);
        }
        else
        {
            hud.SetActive(true);
            pauseScreen.SetActive(false);
        }
    }

    public void ScoreUpdater(int scoreToAdd) //Updates score on UI
    {
        score += scoreToAdd;
        scoreText.text = ": " + score;
        finalScoreText.text = "SCORE - " + score;
    }

    public void LivesUpdater() //updates lives on UI
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

    public void RoundUpdater(int currentRound) //Updates round on UI
    {
        round += currentRound;
        roundText.text = "ROUND: " + round;
    }

    public void coinUpdater(int coinInRound) //Updates coin count on UI
    {
        coinCount = coinInRound;
        coinsText.text = "COINS LEFT: " + coinCount;
    }

    public void rocketUpdater(int rocketsLeft) //Updates rocket count on UI
    {
        rocketsLeft = playerScript.rocketLauncher.rocketAmount;
        rocketsText.text = "Rockets: " + playerScript.rocketLauncher.rocketAmount;

        if (playerScript.rocketLauncher.rocketAmount == 6)
        {
            rocketsText.text = "Rockets: Full";
        }
    }
}
