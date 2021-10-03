using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private float shootDelay = 5f;
    private float fireRate = 5f;
    public float moveSpeed = 0.5f;
    public float enemyHealth = 3f;

    private Transform player;
    private PlayerController playerControllerScript;
    private GameManager gameManager;
    public GameObject projectile;
    public Transform launcher;
    private Rigidbody2D rb;
    private BoxCollider2D bxc;

    private Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bxc = GetComponent<BoxCollider2D>();
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        playerControllerScript = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

        InvokeRepeating("Fire", shootDelay, fireRate);
    }

    // Update is called once per frame
    void Update()
    {
        //Directs enemies towards player
        Vector2 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        direction.Normalize();
        movement = direction;

        //Destroys enemies if their health reach 0
        if (enemyHealth < 1)
        {
            Destroy(gameObject);
            gameManager.score++;

        }
        //Destroys enemies/projectiles if game ends
        if (gameManager.gameOver == true)
        {
            Destroy(gameObject);
            Destroy(GameObject.FindWithTag("Enemy Projectile"));
        }
    }

    private void FixedUpdate()
    {
        MoveEnemy(movement);
    }

    private void MoveEnemy(Vector2 direction)
    {
        //Moves the enemies towards the player
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //Removes one point from enemy health
        if (collider.CompareTag("Projectile"))
        {
            enemyHealth--;
            Destroy(collider.gameObject);
        } 
        
        //Destroys enemy and drops player health by 1
        if (collider.CompareTag("Player"))
        {
            playerControllerScript.playerHealth--;
            Destroy(gameObject);
        }
    }

    private void Fire()
    {
        Instantiate(projectile, launcher.transform.position, launcher.transform.rotation);
    }
}
