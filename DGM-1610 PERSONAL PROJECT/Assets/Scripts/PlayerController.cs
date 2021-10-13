using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public float jumpForce = 6f;
    public float rotationSpeed = 100f;

    private Rigidbody playerRb;
    public GameManager gameManager;
    private GameObject focalPoint;

    public ParticleSystem burnOut;

    public bool hasDiamond;
    public bool hasDp;
    public bool hasJump;
    public bool hasStop;

    private float diamondTime = 5f;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = gameObject.GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.gameActive == true)
            if (Input.GetKeyDown(KeyCode.Space))
                Jump();

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

    void Jump()
    {
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
            print(powerUp.gameObject.name);
            Destroy(powerUp.gameObject);
            StartCoroutine(diamondBombTime());
        }

        IEnumerator diamondBombTime()
        {
            yield return new WaitForSeconds(diamondTime);
            hasDiamond = false;
        }

        //Double points trigger
        if (powerUp.tag == "Double Points")
        {
            hasDp = true;
            print(powerUp.gameObject.name);
            Destroy(powerUp.gameObject);
            StartCoroutine(doublePointsTime());
        }

        //Double Points timer
        IEnumerator doublePointsTime()
        {
            yield return new WaitForSeconds(2);
            hasDp = false;
            print("Double points over!");
        }

        //Jump boost trigger
        if (powerUp.tag == "Jump Boost")
        {
            hasJump = true;
            print(powerUp.gameObject.name);
            Destroy(powerUp.gameObject);
            StartCoroutine(jumpBoostTimer());
        }

        //Jump boost timer
        IEnumerator jumpBoostTimer()
        {
            yield return new WaitForSeconds(2);
            hasJump = false;
            print("Jump boost is gone");
        }

        //Stop Watch trigger
        if (powerUp.tag == "Clock")
        {
            hasStop = true;
            print(powerUp.gameObject.name);
            Destroy(powerUp.gameObject);
            StartCoroutine(stopWatchTimer());
        }

        //Stop watch timer
        IEnumerator stopWatchTimer()
        {
            yield return new WaitForSeconds(2);
            hasStop = false;
            print("stop watch is gone");
        }
    }
}
