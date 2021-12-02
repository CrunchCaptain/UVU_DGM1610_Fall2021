using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public float lifetime;
    private float shootTime;

    public GameObject hitParticle;

    private void OnEnable()
    {
        shootTime = Time.time;
    }

    void OnTriggerEnter(Collider other)
    {
        //Creates the hit particle
        GameObject obj = Instantiate(hitParticle, transform.position, Quaternion.identity);
        Destroy(obj, 1f);

        //if hit, deals damage to player
        if (other.CompareTag("Player"))
            other.GetComponent<PlayerController>().TakeDamage(damage);
        //if hit, deals damage to enemy.
        else if (other.CompareTag("Enemy"))
            other.GetComponent<Enemy>().TakeDamage(damage);

        //Disables projectile for future use
        gameObject.SetActive(false); 
    }

    // Update is called once per frame
    void Update()
    {
        //Disables projectiles via timer after being shot
        if (Time.time - shootTime >= lifetime)
            gameObject.SetActive(false);
    }
}
