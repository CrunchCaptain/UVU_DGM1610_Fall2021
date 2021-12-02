using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapScript : MonoBehaviour
{
    public Transform player;

    private void LateUpdate()
    {
        //locks the mini map camera to the player's position
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;

        //Allows the camera to rotate in the player's direction
        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0);
    }
}
