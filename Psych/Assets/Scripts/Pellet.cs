using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pellet : MonoBehaviour
{
    public float damage = 5;
    public float delayTime = 3f;

    private void Update()
    {
        Destroy(gameObject, delayTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Health>().TakeDamage(damage);
            gameObject.SetActive(false);
        }
        Debug.Log(other.name + " was Hit");
        gameObject.SetActive(false);
    }
}
