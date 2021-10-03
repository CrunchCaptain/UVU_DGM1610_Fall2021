using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int score = 0;
    public int keyAmount = 0;

    public bool gameOver = false;
    public bool hasKey;
    public bool isDoorLocked;

    private PlayerController player;
    public EnemyAI enemy;
    
    public TextMesh scoreUI;
    public TextMesh livesUI;
    public TextMesh keysUI;

    // Start is called before the first frame update
    void Start()
    {
        hasKey = false;
        isDoorLocked = true;

        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        enemy.gameObject.GetComponent<EnemyAI>();
    }

    // Update is called once per frame
    void Update()
    {
        GameIsOver();

        if (keyAmount == 4)
            hasKey = true;

        if (hasKey && !isDoorLocked)
        {
            print("You exit the room!");
        }
    }

    private void GameIsOver()
    {
        //Ends game and destroys player if player runs out of health
        if (player.playerHealth < 1)
        {
            gameOver = true;
            Destroy(player.gameObject);
        }
    }
}
