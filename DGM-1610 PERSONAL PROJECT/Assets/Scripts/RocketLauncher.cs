using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour, IPooledRockets 
{
    public RocketPool rocketPool;

    public int rocketAmount, maxRockets;

    public float rocketSpeed = 45f;

    private void Update()
    {
        //Limits the player's rocket count to 6
        if (rocketAmount > 6)
            rocketAmount = 6;
    }

    public void OnRocketSpawn() //Spawns rockets
    {
            rocketAmount--;

            GameObject rocket = rocketPool.GetRocket();

            rocket.transform.position = gameObject.transform.position;
            rocket.transform.rotation = gameObject.transform.rotation;

            rocket.GetComponent<Rigidbody>().velocity = gameObject.transform.forward * rocketSpeed;
    }
}
