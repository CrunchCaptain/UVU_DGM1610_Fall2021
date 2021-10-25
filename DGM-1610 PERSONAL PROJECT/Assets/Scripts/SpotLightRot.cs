using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotLightRot : MonoBehaviour
{
    public float speed;

    private void Start()
    {
        speed = Random.Range(5, 15);
    }

    private void Update()
    {
        transform.localEulerAngles = new Vector3(30, Mathf.PingPong(Time.time * speed, 75), 0);
    }
}
