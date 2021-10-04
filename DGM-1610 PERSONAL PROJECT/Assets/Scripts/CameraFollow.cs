using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float rotationSpeed = 100f;

    // Update is called once per frame
    void Update()
    {
        float hInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, hInput * rotationSpeed * Time.deltaTime);

        transform.position = GameObject.Find("Player").transform.position;
    }
}
