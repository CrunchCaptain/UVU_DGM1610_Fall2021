using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //public GameObject bulletPrefab;
    public ObjectPool bulletPool;

    public Transform muzzle;

    public int currentAmmo;
    public int maxAmmo;

    public bool infinitAmmo;

    public float bulletSpeed;
    public float fireRate;

    private float lastShootTime;
    private bool isPlayer;

    //Set audio source and sound to play
    public AudioClip shootSFX;
    private AudioSource audioSource;

    private void Awake()
    {
        //Determines if the current gameObject with the script applied is the player or not
        if (GetComponent<PlayerController>())
            isPlayer = true;

        audioSource = GetComponent<AudioSource>();
    }

    public bool CanShoot()
    {
        //Sets fire Rate
        if (Time.time - lastShootTime >= fireRate)
        {
            //Allows shooting if player has ammo or infinit ammo
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
        //GameObject bullet = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
        GameObject bullet = bulletPool.GetObject();

        bullet.transform.position = muzzle.position;
        bullet.transform.rotation = muzzle.rotation;

        //adds velocity to bullets
        bullet.GetComponent<Rigidbody>().velocity = muzzle.forward * bulletSpeed;

        if (isPlayer)
        {
            //Updates ammo amount on UI
            GameUI.instance.UpdateAmmoAmount(currentAmmo, maxAmmo);
        }

        //play shoot sound effect
        audioSource.PlayOneShot(shootSFX);
    }
}
