using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMenu : MonoBehaviour
{
    public GameObject menuCoin;

    public float yRotationSpeed = 30f;
    public float xRotationSpeed = 45f;

    // Update is called once per frame
    void Update()
    {
        //Animates the menu coin
        transform.Rotate(Vector3.right * yRotationSpeed * Time.deltaTime);
        transform.Rotate(Vector3.forward * xRotationSpeed * Time.deltaTime);
    }
}
