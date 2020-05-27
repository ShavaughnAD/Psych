using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float force = 10f;
    public float damage = 5;

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * force);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<Health>().TakeDamage(damage);
            gameObject.SetActive(false);
        }
        Debug.Log(other.name + " was Hit");
        gameObject.SetActive(false);
    }
}
