using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public string pickupName;

    private GameManager gameManager;
    public GameObject keyOne;
    public GameObject keyTwo;
    public GameObject keyThree;
    public GameObject keyFour;

    public void Start()
    {
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        pickupName = "golden key";
    }

    public void Update()
    {
        transform.Rotate(Vector3.back * 15 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && gameObject.CompareTag("1"))
        {
            gameManager.keyAmount++;
            print("You picked up the first " + pickupName + "!");
            Destroy(gameObject);
            keyTwo.SetActive(true);
        }
        if (collision.CompareTag("Player") && gameObject.CompareTag("2"))
        {
            gameManager.keyAmount++;
            print("You picked up the second " + pickupName + "!");
            Destroy(gameObject);
            keyThree.SetActive(true);
        }
        if (collision.CompareTag("Player") && gameObject.CompareTag("3"))
        {
            gameManager.keyAmount++;
            print("You picked up the third " + pickupName + "!");
            Destroy(gameObject);
            keyFour.SetActive(true);
        }
        if (collision.CompareTag("Player") && gameObject.CompareTag("4"))
        {
            gameManager.keyAmount++;
            print("You picked up the fourth " + pickupName + "!");
            Destroy(gameObject);
        }
    }
}
