using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 50f;
    public float turnSpeed = 55f;
    
    private float hInput;
    private float vInput;

    public GameObject missle;

    void Update()
    {
        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");
        //Move the tank left and right
        transform.Rotate(Vector3.up, turnSpeed * hInput * Time.deltaTime);
        //Moves the tank forward and backwards
        transform.Translate(Vector3.forward * speed * Time.deltaTime * vInput);

        //Thought I remembered how to do this, I was wrong :(
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate<GameObject>(missle, gameObject.transform);
            
        }
    }
}
