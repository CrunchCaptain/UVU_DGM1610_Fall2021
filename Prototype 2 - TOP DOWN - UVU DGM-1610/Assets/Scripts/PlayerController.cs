using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5;
    public float turnSpeed = 100;
    
    private float hInput;
    private float vInput;
    private float xRange = 8.4f;
    private float yRange = 4.5f;

    public GameObject projectile;
    public Transform launcher;
    //public float health;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Player Movement
        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");

        transform.Rotate(Vector3.back * turnSpeed * hInput * Time.deltaTime); 
        transform.Translate(Vector3.up * speed * vInput * Time.deltaTime);

        //locking gameplay area with invisible walls
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }
        if (transform.position.y < -yRange)
        {
            transform.position = new Vector3(transform.position.x, -yRange, transform.position.z);
        }
        if (transform.position.y > yRange)
        {
            transform.position = new Vector3(transform.position.x, yRange, transform.position.z);
        }

        //Fires projectile
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(projectile, launcher.transform.position, launcher.transform.rotation);
        }
    }
}
