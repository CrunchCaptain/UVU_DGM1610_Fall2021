using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpRot : MonoBehaviour
{
    public float yRotationSpeed = 30f;

    // Update is called once per frame
    void Update()
    {
        //Rotates power ups
        transform.Rotate(Vector3.up * yRotationSpeed * Time.deltaTime);
    }
}
