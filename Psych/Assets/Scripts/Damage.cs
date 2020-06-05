using UnityEngine;

public class Damage : MonoBehaviour
{
    public enum WeaponSize
    {
        Small, Medium, Large, Heavy
    }
    public WeaponSize weaponSize;
    public float weightDamage = 5;
    Collider weaponCol;

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

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<Health>().TakeDamage(weightDamage); 
            weaponCol.enabled = false;
        }
    }
}