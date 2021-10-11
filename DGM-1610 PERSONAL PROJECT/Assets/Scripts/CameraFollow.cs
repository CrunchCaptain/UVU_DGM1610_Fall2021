using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private float rotationSpeed = 100f;
    private float menuSpeed = 25f;

    public GameManager gameManager;
    public Rigidbody player;
    public Quaternion startY;


    private void Start()
    {
        startY = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {

        if (gameManager.gameActive == true)
        {
            float hInput = Input.GetAxis("Horizontal");
            transform.Rotate(Vector3.up, hInput * rotationSpeed * Time.deltaTime);
            player.transform.Rotate(Vector3.up, hInput * rotationSpeed * Time.deltaTime);
            transform.position = player.transform.position;
            
        } 
        else
        {
            transform.Rotate(Vector3.up * menuSpeed * Time.deltaTime);
        }
    }
}
