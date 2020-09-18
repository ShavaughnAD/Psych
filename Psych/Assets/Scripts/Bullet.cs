using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float force = 10f;
    public float damage = 5;
    [SerializeField]
    private float secondsTillBulletDeactivates = 5f;

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * force);
        //DeactivateWhenTimerExpires();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerHealth>() != null)
        {
            other.GetComponent<PlayerHealth>().TakeDamage(damage);
            gameObject.SetActive(false);
        }
        else if(!other.tag.Equals("Weapon"))
        {
            gameObject.SetActive(false);
        }
        Debug.LogWarning(gameObject.name + " Collided with " + other.gameObject.name);
    }

    private void DeactivateWhenTimerExpires()
    {
        secondsTillBulletDeactivates -= Time.deltaTime;
        if(secondsTillBulletDeactivates <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
