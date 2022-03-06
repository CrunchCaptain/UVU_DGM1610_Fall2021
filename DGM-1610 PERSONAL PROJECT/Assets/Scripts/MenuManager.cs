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

    public void StartButton() //Loads game scene
    {
        SceneManager.LoadScene("Coin Thief");
    }

    public void ExitButton() //Ends application
    {
        Application.Quit();
    }

    public void HowToButton() //Opens the first "how to play" page
    {
        mainMenu.SetActive(false);
        howToMenu.SetActive(true);
    }

    public void ReturnMenu() //Returns to main menu from how to pages
    {
        howToMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void NextPage() //Moves to page 2 of how to pages
    {
        pageOne.SetActive(false);
        pageTwo.SetActive(true);
    }

    public void PreviousPage() //returns to 1st how to page
    {
        pageTwo.SetActive(false);
        pageOne.SetActive(true);
    }
}
