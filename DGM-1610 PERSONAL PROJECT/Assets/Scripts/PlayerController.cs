using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public float rotationSpeed = 100f;
    private bool canMove;

    private float jumpForce = 6f;
    private float lastJumpTime;
    public float jumpRate;
    private float jumpCoolDown;

    private Rigidbody playerRb;
    public GameManager gameManager;
    private GameObject focalPoint;
    public GameObject rocketPrefab;
    public Transform rocketBarrel;
    public ParticleSystem burnOut;

    public bool hasDiamond;
    public bool hasDp;
    public bool hasRocket;
    public bool hasStop;

    private float diamondTime = 5f;
    private float doublePointsTime = 20;
    private float stopWatchTime = 10f;
    public int rocketAmount;

    public GameObject diamondIndicator;
    public GameObject stopWatchIndicator;
    public GameObject doublePointIndicator;

    void Start()
    {
        playerRb = gameObject.GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");   
    }

    // Update is called once per frame
    void Update()
    {
        Rocket();

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
            float vInput = Input.GetAxis("Vertical") * speed;
            playerRb.AddForce(focalPoint.transform.forward * vInput, ForceMode.Acceleration);
    }

    private bool CanJump()
    {
        if (Time.time - lastJumpTime >= jumpRate)
        {
            return true;
        }
            
        return false;
    }

    void Rocket()
    {
        if (rocketAmount >= 1)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Instantiate(rocketPrefab, rocketBarrel.position, rocketBarrel.rotation);
                rocketAmount--;
            }
        }

        if (rocketAmount > 6)
            rocketAmount = 6;
    }

    void Jump()
    {
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

        IEnumerator diamondBombTime()
        {
            yield return new WaitForSeconds(diamondTime);
            hasDiamond = false;
            diamondIndicator.SetActive(false);
        }

        //Double points trigger
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

        //Stop watch timer
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
            rocketAmount += 3;
            Destroy(powerUp.gameObject);
        }
    }
}
