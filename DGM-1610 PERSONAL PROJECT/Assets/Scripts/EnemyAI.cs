using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 5;
    public float scoreValue = 5f;
    private int damage = -1;

    private Rigidbody enemyRb;
    private GameObject playerLocation;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = gameObject.GetComponent<Rigidbody>();
        playerLocation = GameObject.FindWithTag("Player");
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        Vector3 followPlayer = (playerLocation.transform.position - transform.position).normalized;

        enemyRb.AddForce(followPlayer * speed);
    }
}
