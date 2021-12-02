using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public PickupType type;
    public int value;


    [Header("Bobbing Motion")]
    public float rotationSpeed;
    public float bobSpeed;
    public float bobHeight;
    private bool isBobbing;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    public enum PickupType
    {
        Health,
        Ammo
    }

    private void OnTriggerEnter(Collider other)
    {
        //Recognize which pick up the player collides with and gives that value accordingly 
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();

            switch (type)
            {
                case PickupType.Health:
                    player.GiveHealth(value);
                        break;
                case PickupType.Ammo:
                    player.GiveAmmo(value);
                        break;
                default:
                    print("Type not accepted");
                        break;
            }

            Destroy(gameObject);
        }
    }

    void Update()
    {
        //Animates pickups
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        Vector3 offset = (isBobbing == true ? new Vector3(0, bobHeight / 2, 0) : new Vector3(0, -bobHeight / 2, 0));

        transform.position = Vector3.MoveTowards(transform.position, startPos + offset, bobSpeed * Time.deltaTime);

        if (transform.position == startPos + offset)
            isBobbing = !isBobbing;
    }
}
