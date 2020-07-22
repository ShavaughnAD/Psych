using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public float damage;
    public float healthGain;
    BoxCollider volume;
    public Animator Anim;
    bool attacking;
    PlayerHealth playerHealth;
    public float MeleeCooldown = 2;
    public float NextMeleeTime = 0;
    
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
        if(Time.time > NextMeleeTime)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Anim.SetBool("canAttack", true);
                NextMeleeTime = Time.time + MeleeCooldown;
                VolumeOn();
            }
        }
        
        Anim.SetBool("canAttack", false);
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
