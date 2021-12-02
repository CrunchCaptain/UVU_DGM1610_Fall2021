using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("Main Menu")]
    public GameObject mainMenu;
    public Button startButton;
    public Button howToButton;
    public Button exitButton;

    [Header("How To Play Menu")]
    public GameObject howToMenu;
    public GameObject pageOne;
    public Button pageOneButton;
    public GameObject pageTwo;
    public Button pageTwoButton;
    public Button menuReturnButton;


    // Start is called before the first frame update
    void Start()
    {
        mainMenu.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartButton()
    {
        SceneManager.LoadScene("Coin Thief");
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void HowToButton()
    {
        mainMenu.SetActive(false);
        howToMenu.SetActive(true);
    }

    public void ReturnMenu()
    {
        howToMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void NextPage()
    {
        pageOne.SetActive(false);
        pageTwo.SetActive(true);
    }

    public void PreviousPage()
    {
        pageTwo.SetActive(false);
        pageOne.SetActive(true);
    }
}
