using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public float damage;
    public float healthGain;
    BoxCollider volume;
    bool attacking;
    PlayerHealth playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        volume = GetComponent<BoxCollider>();
        volume.enabled = false;
        playerHealth = transform.parent.gameObject.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !attacking)
        {
            VolumeOn();
        }
    }

    void VolumeOn()
    {
        attacking = true;
        volume.enabled = true;
    }
    void VolumeOff()
    {
        attacking = false;
        volume.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.GetComponent<EnemyHealth>().TakeDamage(damage);
            playerHealth.Healing(healthGain);
            VolumeOff();
        }
    }
}
