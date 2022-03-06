using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float lifetime;
    private float shootTime;

    // Start is called before the first frame update
    void Start()
    {
        shootTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        //Sets rocket inactive after set amount of time
        if (Time.time - shootTime >= lifetime)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        //If the rocket hits a wall or enemy, the rocket will be set to inactive
        if (other.CompareTag("Enemy") || other.CompareTag("Wall"))
            gameObject.SetActive(false);
    } 
}
