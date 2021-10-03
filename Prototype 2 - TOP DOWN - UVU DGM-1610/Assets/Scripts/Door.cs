using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && gameManager.hasKey == true)
        {
            print("You unlocked the door with the key");
            gameManager.isDoorLocked = false;
            gameManager.gameOver = true;
        }
        else
        {
            print("The door is locked, you cannot escape! Bwahaha");
        }
    }
}
