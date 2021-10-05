using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUps : MonoBehaviour
{
    public float yRotationSpeed = 30f;
    public float xRotationSpeed = 20f;

    public int scoreValue = 5;

    private GameManager gameManager;
    public ParticleSystem explosionPart;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Rotates coin so it's visiable to players
        transform.Rotate(Vector3.right * yRotationSpeed * Time.deltaTime);
        transform.Rotate(Vector3.forward * xRotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            Instantiate(explosionPart, transform.position, explosionPart.transform.rotation);
            gameManager.ScoreUpdater(scoreValue);
        }
    }
}
