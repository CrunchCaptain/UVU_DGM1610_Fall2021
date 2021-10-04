using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Player movement speed in units per second
    public float movementSpeed;

    //Force applied upwards
    public float jumpForce;

    //Mouse look sensitivity
    public float lookSensitivity;

    //Limits the mouse/camera's rotation
    public float maxLookX = 50f;
    public float minLookX = -65f;

    //Current x rotation of the camera
    private float rotX;

    //Declares player's camera & RigidBody
    private Camera playerCamera;
    private Rigidbody playerRb;

    // Start is called before the first frame update
    void Start()
    {
        //Calls player's camera & RigidBody
        playerCamera = Camera.main;
        playerRb = GameObject.Find("Player").GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal") * movementSpeed;
        float z = Input.GetAxis("Vertical") * movementSpeed;

        playerRb.velocity = new Vector3(x, playerRb.velocity.y, z);
    }

    void CamLook()
    {
        float y = Input.GetAxis("Mouse X") * lookSensitivity;
        rotX += Input.GetAxis("Mouse Y") * lookSensitivity;
    }
}
