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
            Hits.Add(other.gameObject.GetComponent<EnemyHealth>());
            foreach (EnemyHealth Harmed in Hits)
            {
                if (Harmed.currentHealth > 0)
                {
                    Harmed.TakeDamage(weightDamage);
                    SkinnedMeshRenderer currentRenderer = other.gameObject.GetComponent<PlayerVision>().GetMeshRenderer();
                    if (currentRenderer != null && Harmed != null && isRed == false)
                    {
                        DamageHandler handler = null;
                        if(currentRenderer.gameObject.GetComponent<DamageHandler>())
                        {
                            handler = currentRenderer.gameObject.GetComponent<DamageHandler>();
                        }
                        else
                        {
                            handler = currentRenderer.gameObject.AddComponent<DamageHandler>();
                            handler.AssignEverything();
                        }
                        handler.ShowDamage();
                        handler.StartCoroutine(handler.ResetCountdown(1));
                        //hitRenderer = currentRenderer;
                        //defaultMaterial = currentRenderer.material;
                        //FlashRed();
                    }
                }

                if (Hits[0] == null)
                {
                    Destroy(gameObject);
                    Hits.Clear();
                }
            }
        }
        else if (other.gameObject.tag == "Projectile" || other.gameObject.tag == "Weapon" || other.gameObject.tag == "Player" || other.GetComponent<DoorFunctionality>() != null || other.GetComponent<SpinGenerator>() != null || other.GetComponent<Bullet>() != null)
        {
            //Ignore it

        }
        else
        {

            Destroy(gameObject);
        }
    }
}
