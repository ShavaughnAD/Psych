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
        if (other.tag == "Player" || other.tag == "Enemy")
        {
            other.GetComponent<Health>().TakeDamage(damage);
            gameObject.SetActive(false);
        }
        gameObject.SetActive(false);
        Debug.LogWarning(this.gameObject.name + " Collided with " + other.gameObject.name);
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
