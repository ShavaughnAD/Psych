using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public List<Health> Hits;
    public enum WeaponSize
    {
        Small, Medium, Large, Heavy
    }
    public WeaponSize weaponSize;
    public float weightDamage = 5;
    Collider weaponCol;
    public float flashTimer;
    SkinnedMeshRenderer hitRenderer;
    public Material hitMaterial;
    Material defaultMaterial;
    void Awake()
    {
        weaponCol = GetComponent<Collider>();
        switch (weaponSize)
        {
            case WeaponSize.Small:
                weightDamage = 8;
                break;

            case WeaponSize.Medium:
                weightDamage = 10;
                break;

            case WeaponSize.Large:
                weightDamage = 12;
                break;

            case WeaponSize.Heavy:
                weightDamage = 20;
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.tag == "Projectile")
        {
            //Debug.LogError(other);
            if (other.gameObject.tag == "Enemy")
            {
                SkinnedMeshRenderer currentRenderer = other.gameObject.GetComponent<PlayerVision>().GetMeshRenderer();
                if (currentRenderer != null)
                {
                    hitRenderer = currentRenderer;
                    defaultMaterial = currentRenderer.material;
                    FlashRed();
                }
                    
                Hits.Add(other.gameObject.GetComponent<Health>());
                foreach (Health Harmed in Hits)
                {
                    if (Hits[0])
                    {
                        other.gameObject.GetComponent<Health>().TakeDamage(weightDamage);
                    }
                    else
                    {
                        other.gameObject.GetComponent<Health>().TakeDamage(weightDamage / 2);
                    }

                    if (Hits[0] == null)
                    {
                        //Destroy(gameObject);
                    }
                }
            }
            else if(other.gameObject.tag == "Player" || other.gameObject.tag == "Projectile" || other.gameObject.tag == "Weapon")
            {
                //Debug.LogError("Don't Destroy Psychic Blast :" + other.gameObject + " was hit.");
            }
            else
            {
                Destroy(gameObject);
            }
        }
        //else
        //{
        //    if (other.tag == "Enemy")
        //    {
        //        other.GetComponent<Health>().TakeDamage(weightDamage);
        //    }
        //}
    }
    void FlashRed()
    {
        hitRenderer.material = hitMaterial;
        flashTimer = 0.1f;
        Time.timeScale = 1;
        Invoke("ResetShader", flashTimer); 
    }

    void ResetShader()
    {
        hitRenderer.material = defaultMaterial;
    }
}