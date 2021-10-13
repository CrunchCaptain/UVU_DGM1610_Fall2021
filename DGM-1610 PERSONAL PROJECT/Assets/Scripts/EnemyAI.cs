using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 6.5f;
    public float scoreValue = 5f;
    private int damage = -1;

    private Rigidbody enemyRb;
    private GameObject playerLocation;
    private PlayerController playerScript;
    private GameManager gameManager;

    public Vector3 followPlayer;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = gameObject.GetComponent<Rigidbody>();
        playerLocation = GameObject.FindWithTag("Player");
        playerScript = playerLocation.GetComponent<PlayerController>();
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();

        speed = 6.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerScript.hasDiamond == true)
        {
            followPlayer = (playerLocation.transform.position + transform.position).normalized;
        }
        else
        {
            followPlayer = (playerLocation.transform.position - transform.position).normalized;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameManager.LivesUpdater(damage);
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
