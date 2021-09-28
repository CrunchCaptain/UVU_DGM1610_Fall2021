using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private float topBound = 6.0f;
    private float sideBound = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > topBound)
        {
            Destroy(gameObject);
        } 
        if (transform.position.y < -topBound)
        {
            Destroy(gameObject);
        }
        if (transform.position.x > sideBound)
        {
            Destroy(gameObject);
        }
        if (transform.position.x < -sideBound)
        {
            Destroy(gameObject);
        }
    }
}
