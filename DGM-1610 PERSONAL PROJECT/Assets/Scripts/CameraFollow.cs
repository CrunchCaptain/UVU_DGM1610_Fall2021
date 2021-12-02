using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private float rotationSpeed = 100f;

    public Rigidbody player;
    public Quaternion startY;

    private void Start()
    {
        //Locks camera's starting rotation
        startY = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
            float hInput = Input.GetAxis("Horizontal");
            transform.Rotate(Vector3.up, hInput * rotationSpeed * Time.deltaTime);
            player.transform.Rotate(Vector3.up, hInput * rotationSpeed * Time.deltaTime);
            transform.position = player.transform.position;
    }
}
