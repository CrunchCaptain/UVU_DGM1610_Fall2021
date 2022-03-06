using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Rates")]
    public float speed = 10f;
    public float rotationSpeed = 100f;
    public float jumpRate;

    private float jumpForce = 6f;
    private float lastJumpTime;

    private Rigidbody playerRb;

    [Header("Scripts")]
    public RocketLauncher rocketLauncher;
    public GameManager gameManager;

    [Header("PowerUps")]
    public bool hasDiamond;
    public bool hasDp;
    public bool hasRockets;
    public bool hasStop;

    private float diamondTime = 5f;
    private float doublePointsTime = 20;
    private float stopWatchTime = 10f;
    
    [Header("PowerUp GameObjects")]
    public GameObject diamondIndicator;
    public GameObject stopWatchIndicator;
    public GameObject doublePointIndicator;
    public GameObject rocketPrefab;
    private GameObject focalPoint;

    
    void Start()
    {
        playerRb = gameObject.GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");   
    }

    // Update is called once per frame
    void Update()
    {
        Rocket();

        //if the game is active player can jump
        if (gameManager.gameActive == true)
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (CanJump() == true)
                {
                    Jump();
                    print("You jumped!");
                }
                else
                    print("Jump cooling down");
            }
    }

    void FixedUpdate()
    {
        if (gameManager.gameActive == true)
            Movement();
    }

    public void Movement()
    {
       //adds force/input to player   
       float vInput = Input.GetAxis("Vertical") * speed;
       playerRb.AddForce(focalPoint.transform.forward * vInput, ForceMode.Acceleration);
    }

    private bool CanJump()
    {
        //Jump cooldown
        if (Time.time - lastJumpTime >= jumpRate)
        {
            return true;
        }
            
        return false;
    }

    void Rocket()
    {
        //active rockets if player has any
        if (rocketLauncher.rocketAmount >= 1)
            hasRockets = true;
        else
            hasRockets = false;

        if (hasRockets == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
                rocketLauncher.OnRocketSpawn();
        }
    }

    void Jump()
    {
        //jump mechanic
        lastJumpTime = Time.time;

        Ray jumpRay = new Ray(transform.position, Vector3.down);

        if (Physics.Raycast(jumpRay, 1.1f))
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    

    private void OnTriggerEnter(Collider powerUp)
    {
        //Diamond Bomb trigger
        if (powerUp.tag == "Diamond")
        {
            hasDiamond = true;
            diamondIndicator.SetActive(true);
            print(powerUp.gameObject.name);
            Destroy(powerUp.gameObject);
            StartCoroutine(diamondBombTime());
        }
        //Diamond Bomb timer
        IEnumerator diamondBombTime()
        {
            yield return new WaitForSeconds(diamondTime);
            hasDiamond = false;
            diamondIndicator.SetActive(false);
        }

        //Double Points trigger
        if (powerUp.tag == "Double Points")
        {
            hasDp = true;
            doublePointIndicator.SetActive(true);
            print(powerUp.gameObject.name);
            Destroy(powerUp.gameObject);
            StartCoroutine(doublePointsTimer());
        }

        //Double Points timer
        IEnumerator doublePointsTimer()
        {
            yield return new WaitForSeconds(doublePointsTime);
            hasDp = false;
            doublePointIndicator.SetActive(false);
            print("Double points over!");
        }

        //Stop Watch trigger
        if (powerUp.tag == "Clock")
        {
            hasStop = true;
            stopWatchIndicator.SetActive(true);
            Destroy(powerUp.gameObject);
            stopWatchIndicator.GetComponent<AudioSource>().Play();
            StartCoroutine(stopWatchTimer());
        }

        //Stop Watch timer
        IEnumerator stopWatchTimer()
        {
            yield return new WaitForSeconds(stopWatchTime);
            hasStop = false;
            stopWatchIndicator.GetComponent<AudioSource>().Pause();
            stopWatchIndicator.SetActive(false);
        }

        //Gives player 3 more rockets
        if (powerUp.tag == "Rocket Pickup")
        {
            //Gives player 3 rockets if they don't have the max amount of rockets (6)
            if (rocketLauncher.rocketAmount < 6)
            {
                rocketLauncher.rocketAmount += 3;
                Destroy(powerUp.gameObject);
            } 
        }
    }
}
