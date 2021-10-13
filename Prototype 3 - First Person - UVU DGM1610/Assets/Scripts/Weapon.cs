using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;

    public Transform muzzle;

    public int currentAmmo;
    public int maxAmmo;

    public bool infinitAmmo;

    public float bulletSpeed;
    public float fireRate;

    private float lastShootTime;
    private bool isPlayer;

    private void Awake()
    {
        if (GetComponent<PlayerController>())
            isPlayer = true;
    }

    public bool CanShoot()
    {
        if (Time.time - lastShootTime >= fireRate)
        {
            if (currentAmmo > 0 || infinitAmmo == true)
                return true;
        }

        return false;
       
    }

    public void Shoot()
    {
        //Cooldown
        lastShootTime = Time.time;
        currentAmmo--;

        //Creating an instaqnce of the bullet prefab at muzzles position and rotation
        GameObject bullet = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);

        //adds velocity to bullets
        bullet.GetComponent<Rigidbody>().velocity = muzzle.forward * bulletSpeed;
    }
}
