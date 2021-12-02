using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int scoreToWin;
    public int curScore;

    public bool gamePaused;


    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //pauses game on esc key press
        if (Input.GetButtonDown("Cancel"))
        {
            TogglePauseGame();
        }
    }

    //adjusts game time & toggles pause screen
    public void TogglePauseGame()
    {
        gamePaused = !gamePaused;

        Time.timeScale = gamePaused == true ? 0.0f : 1.0f;

        //Toggles pause menu & cursor
        GameUI.instance.TogglePauseMenu(gamePaused);
        Cursor.lockState = gamePaused == true ? CursorLockMode.Confined : CursorLockMode.Locked;

    }

    public void AddScore(int score)
    {
        curScore += score;

        //Update score text
        GameUI.instance.UpdateScoreText(curScore);

        //Do we have enough points to win?
        if(curScore >= scoreToWin)
        {
            WinGame();
        }
    }

    void WinGame()
    {
        //Show win screen
        GameUI.instance.SetGameOverScreen(true, curScore);
        Time.timeScale = 0.0f;
        gamePaused = true;
        Cursor.lockState = gamePaused == true ? CursorLockMode.Confined : CursorLockMode.Locked;
    }

    public void LoseGame()
    {
        //ends game and displays game over screen
        GameUI.instance.SetGameOverScreen(false, curScore);
        Time.timeScale = 0.0f;
        gamePaused = true;
        Cursor.lockState = gamePaused == true ? CursorLockMode.Confined : CursorLockMode.Locked;
    }
}
