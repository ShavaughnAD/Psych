using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    public float damage;
    public float radius;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Testing");
        if(collision.gameObject.tag == "Projectile")
        {
            Debug.Log("BOMB HIT");
            Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
            foreach (Collider item in colliders)
            {
                if(item.tag =="Player" || item.tag =="Enemy")
                {
                    Debug.Log(item.name + " Got bomb hit");
                    item.GetComponent<Health>().TakeDamage(damage);
                    Destroy(gameObject);
                }
            }
        }
    }
}
