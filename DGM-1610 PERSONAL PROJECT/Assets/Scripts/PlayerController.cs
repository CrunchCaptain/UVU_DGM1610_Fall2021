using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public float jumpForce = 5f;

    private Rigidbody playerRb;
    private GameObject focalPoint;
    

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
        Movement();
    }

    void Movement()
    {
        float vInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * vInput);
    }
}
