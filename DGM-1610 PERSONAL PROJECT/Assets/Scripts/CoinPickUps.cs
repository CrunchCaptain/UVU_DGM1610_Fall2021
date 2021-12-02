using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUps : MonoBehaviour
{
    public float yRotationSpeed = 30f;
    public float xRotationSpeed = 20f;

    public int scoreValue = 5;

    private GameManager gameManager;
    public PlayerController playerScript;
    public ParticleSystem explosionPart;
    public AudioClip coinSound;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        playerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Rotates coin so it's visiable to players
        transform.Rotate(Vector3.right * yRotationSpeed * Time.deltaTime);
        transform.Rotate(Vector3.forward * xRotationSpeed * Time.deltaTime);

        if (playerScript.hasDp == true)
        {
            scoreValue = 10; //coins are double points while player has double point pick up
        }
        else
        {
            scoreValue = 5; //Resets to original value
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) //Detects when player collects a coin
        {
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(coinSound, transform.position);
            Instantiate(explosionPart, transform.position, explosionPart.transform.rotation);
            gameManager.ScoreUpdater(scoreValue);
        }
    }
}
