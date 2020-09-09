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
    protected SkinnedMeshRenderer hitRenderer;
    public Material hitMaterial;
    public Material defaultMaterial;
    protected bool isRed = false;
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
            if (other.gameObject.tag == "Player")
            {
                if (other.GetComponent<PlayerHealth>() != null)
                {
                    other.GetComponent<PlayerHealth>().TakeDamage(weightDamage);
                }
                //Debug.LogError("Don't Destroy Psychic Blast :" + other.gameObject + " was hit.");
            }
            else if (other.gameObject.tag == "Projectile" || other.gameObject.tag == "Weapon" || other.GetComponent<DoorFunctionality>() != null || other.GetComponent<SpinGenerator>() != null)
            {

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
    public void FlashRed()
    {
        isRed = true;
        hitRenderer.material = hitMaterial;
        flashTimer = 0.1f;
        Invoke("ResetShader", flashTimer);
    }

    void ResetShader()
    {
        hitRenderer.material = defaultMaterial;
        isRed = false;
    }
}