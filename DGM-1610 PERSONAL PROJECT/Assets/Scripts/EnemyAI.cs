using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

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
        //Randomizes enemy movement/speed
        spawnSpeed = Random.Range(speedMin, speedMax);
        float speedRandom = Random.Range(speedMin, speedMax);
        randomInput = Random.Range(-150f, 150f);
        Vector3 randomInputVec = new Vector3(randomInput, 0, randomInput);

        if (playerScript.hasDiamond == true) //pushes enemies away if player has diamond bomb
        {
            followPlayer = (playerLocation.transform.position + transform.position).normalized;
        }
        else //default enemy movment
        {
            followPlayer = (playerLocation.transform.position - transform.position - randomInputVec).normalized;
        }

        if (playerScript.hasStop == true) //Slows enemies down if player has stop watch
        {
            speed = 1;
            stopWatchParticle.SetActive(true);
        }
        else //default speed
        {
            speed = speedRandom;
            stopWatchParticle.SetActive(false);
        }
        
        if (playerScript.hasDp == true) //doubles enemies score value if player has double points
        {
            scoreValue = 10;
        }
        else //default score value
        {
            scoreValue = 5;
        }

        if (transform.position.y > 8) //Destroy enemies if diamond bomb pushs them out of bounds
        {
            Destroy(gameObject);
            gameManager.score += scoreValue;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //hurts player if enemy touches them
        if (collision.gameObject.tag == "Player")
        {
            gameManager.lives--;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Destroyes enemies and adds to score if hit by rocket
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
        //adds force to enemies
        enemyRb.AddForce(followPlayer * speed, ForceMode.Acceleration);
    }
}
