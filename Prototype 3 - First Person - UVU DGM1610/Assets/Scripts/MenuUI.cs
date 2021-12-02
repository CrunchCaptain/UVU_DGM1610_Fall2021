using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    // On Button press "Play" loads "Game" scene
    public void OnPlayButton()
    {
        SceneManager.LoadScene("Game");
    }

    // On button press "Quit" closes application
    public void OnQuitButton()
    {
        Application.Quit();
    }
}
