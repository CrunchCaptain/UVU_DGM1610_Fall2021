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
    private Weapon weaponScript;

    private void Awake()
    {
        //Disable cursor
        Cursor.lockState = CursorLockMode.Locked;

        weaponScript = GetComponent<Weapon>();
    }

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
        CamLook();
        Aim();
        //Fire Button
        if (Input.GetMouseButton(0))
        {
            if (weaponScript.CanShoot())
                weaponScript.Shoot();
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
        
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal") * movementSpeed;
        float z = Input.GetAxis("Vertical") * movementSpeed;

        //playerRb.velocity = new Vector3(x, playerRb.velocity.y, z);
        Vector3 dir = transform.right * x + transform.forward * z;
        playerRb.velocity = dir;
    }

    void Jump()
    {
        Ray ray = new Ray(transform.position, Vector3.down);

        if (Physics.Raycast(ray, 1.1f))
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    //Function creating an aim mechanic
    void Aim()
    {
        if(Input.GetMouseButtonDown(1))
        {
            GameObject.Find("Gun").transform.localPosition = new Vector3(0, -0.2f, 0.66f);
        }
        if (Input.GetMouseButtonUp(1))
        {
            GameObject.Find("Gun").transform.localPosition = new Vector3(0.17f, -0.26704f, 0.66f);
        }
    }

    void CamLook()
    {
        float y = Input.GetAxis("Mouse X") * lookSensitivity;
        rotX += Input.GetAxis("Mouse Y") * lookSensitivity;

        rotX = Mathf.Clamp(rotX, minLookX, maxLookX);
        playerCamera.transform.localRotation = Quaternion.Euler(-rotX, 0, 0);
        transform.eulerAngles += Vector3.up * y;
    }
}
