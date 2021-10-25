using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed;
    private float spawnSpeed;
    public float speedMin = 5;
    public float speedMax = 15;
    private int scoreValue = 5;
    private float randomInput;

    private Rigidbody enemyRb;
    private GameObject playerLocation;
    public GameObject stopWatchParticle;
    private PlayerController playerScript;
    private GameManager gameManager;

    public Vector3 followPlayer;

    // Start is called before the first frame update
    void Start()
    {
        speed = spawnSpeed;
        enemyRb = gameObject.GetComponent<Rigidbody>();
        playerLocation = GameObject.FindWithTag("Player");
        playerScript = playerLocation.GetComponent<PlayerController>();
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        spawnSpeed = Random.Range(speedMin, speedMax);
        float speedRandom = Random.Range(speedMin, speedMax);
        randomInput = Random.Range(-150f, 150f);
        Vector3 randomInputVec = new Vector3(randomInput, 0, randomInput);

        if (playerScript.hasDiamond == true)
        {
            followPlayer = (playerLocation.transform.position + transform.position).normalized;
        }
        else
        {
            followPlayer = (playerLocation.transform.position - transform.position - randomInputVec).normalized;
        }

        if (playerScript.hasStop == true)
        {
            speed = 1;
            stopWatchParticle.SetActive(true);
        }
        else 
        {
            speed = speedRandom;
            stopWatchParticle.SetActive(false);
        }
        
        if (playerScript.hasDp == true)
        {
            scoreValue = scoreValue * 2;
        }
        else
        {
            scoreValue = 5;
        }

        if (transform.position.y > 8)
        {
            Destroy(gameObject);
            gameManager.score += scoreValue;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameManager.lives--;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Rocket"))
        {
            Destroy(gameObject);
            gameManager.score += scoreValue;
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        enemyRb.AddForce(followPlayer * speed, ForceMode.Acceleration);
    }
}
