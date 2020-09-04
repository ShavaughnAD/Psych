using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PsychicBlast : Damage
{
    public void OnTriggerEnter(Collider other)
    {
        //Debug.LogError(other);
        if (other.gameObject.tag == "Enemy" && other.GetComponent<EnemyHealth>() != null)
        {
            SkinnedMeshRenderer currentRenderer = other.gameObject.GetComponent<PlayerVision>().GetMeshRenderer();
            if (currentRenderer != null)
            {
                hitRenderer = currentRenderer;
                defaultMaterial = currentRenderer.material;
                FlashRed();
            }

            Hits.Add(other.gameObject.GetComponent<EnemyHealth>());
            foreach (Health Harmed in Hits)
            {
                if (Hits[0])
                {
                    other.gameObject.GetComponent<EnemyHealth>().TakeDamage(weightDamage);
                }
                else
                {
                    other.gameObject.GetComponent<EnemyHealth>().TakeDamage(weightDamage / 2);
                }

                if (Hits[0] == null)
                {
                    Destroy(gameObject);
                }
            }
        }
        else if (other.gameObject.tag == "Projectile" || other.gameObject.tag == "Weapon" || other.gameObject.tag == "Player" || other.GetComponent<DoorFunctionality>() != null || other.GetComponent<SpinGenerator>() != null)
        {
            //Ignore it
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
