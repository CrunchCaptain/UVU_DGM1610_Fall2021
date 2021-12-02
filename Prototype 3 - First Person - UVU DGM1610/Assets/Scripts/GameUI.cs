using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    [Header("HUD")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI ammoText;
    public Image healthBarFill;

    [Header("Pause Menu")]
    public GameObject pauseMenu;

    [Header("Game Over Screen")]
    public GameObject endGameScreen;
    public TextMeshProUGUI endGameHeaderText;
    public TextMeshProUGUI endGameScoreText;

    // Instance / Singleton 
    public static GameUI instance;

    void Awake()
    {
        //Set instance to this script
        instance = this;
    }

    public void UpdateHealthBar(int curHP, int maxHp)
    {
        //Updates health bar to players current HP
        healthBarFill.fillAmount = (float)curHP / (float)maxHp;
    }

    public void UpdateScoreText(int score)
    {
        //Adds player's  current score to the UI 
        scoreText.text = "Score: " + score;
    }

    public void UpdateAmmoAmount(int curAmmo, int maxAmmo)
    {
        //Displays player's current Ammon on UI
        ammoText.text = curAmmo + " / " + maxAmmo;
    }

    public void TogglePauseMenu(bool pause)
    {
        pauseMenu.SetActive(pause);
    }

    public void SetGameOverScreen(bool won, int score)
    {
        //Game over UI changes based on if the player won or lost
        endGameScreen.SetActive(true);
        endGameHeaderText.text = won == true ? "You Win!" : "You Lose.";
        endGameScoreText.color = won == true ? Color.green : Color.red;
        endGameScoreText.text = "<b>Score</b>\n" + score;
    }

    public void OnResumeButton()
    {
        //resumes current game from pause menu
        GameManager.instance.TogglePauseGame();
    }

    public void OnRestartButton()
    {
        //Restarts game scene
        SceneManager.LoadScene("Game");
    }

    public void OnMenuButton()
    {
        //Returns to menu scene
        SceneManager.LoadScene("Menu");
    }
}
