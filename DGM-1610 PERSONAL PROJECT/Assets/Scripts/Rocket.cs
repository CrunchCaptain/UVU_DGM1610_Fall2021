using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private Rigidbody rocketRb;
    public Transform rocketMuzzle;
    private Vector3 direction;
    private float velocity = 60;

    private float maxDist = 30;

    // Start is called before the first frame update
    void Start()
    {
        rocketRb = gameObject.GetComponent<Rigidbody>();
        rocketMuzzle = GameObject.Find("Rocket Barrel").transform;

    }

    // Update is called once per frame
    void Update()
    {
        rocketRb.velocity = rocketMuzzle.forward * velocity;

        if (transform.position.x > maxDist)
            Destroy(gameObject);
        if (transform.position.x < -maxDist)
            Destroy(gameObject);
        if (transform.position.z > maxDist)
            Destroy(gameObject);
        if (transform.position.z < -maxDist)
            Destroy(gameObject);
    }
}
