using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public float jumpForce = 5f;
    public float rotationSpeed = 100f;

    private Rigidbody playerRb;
    public GameManager gameManager;
    private GameObject focalPoint;

    public ParticleSystem burnOut;
    

    // Start is called before the first frame update
    void Start()
    {
        playerRb = gameObject.GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
       

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
}
