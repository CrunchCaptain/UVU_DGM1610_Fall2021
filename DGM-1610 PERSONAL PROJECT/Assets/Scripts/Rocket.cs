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
        if (Time.time - shootTime >= lifetime)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Wall"))
            gameObject.SetActive(false);
    } 
}
