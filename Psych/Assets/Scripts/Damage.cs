using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public float damage = 5;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Health>())
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
            if(gameObject.tag == "Projectile")
            {
                gameObject.SetActive(false);
                //Destroy(gameObject);
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyHealth>())
        {
            Debug.LogError(other.name + " Was Hit");
            other.GetComponent<EnemyHealth>().TakeDamage(damage);
            if (gameObject.tag == "Projectile")
            {
                gameObject.SetActive(false);
                //Destroy(gameObject);
            }
        }
    }
}
