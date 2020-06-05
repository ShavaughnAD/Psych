using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{
    public WeaponThrow playerWeapon;
    BoxCollider boxCollider;
    WeaponShooting weaponShooting;
    Rigidbody weaponRB = null;
    public bool pickedUp = false;
    void Start()
    {
        playerWeapon = GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponThrow>();
        weaponRB = GetComponent<Rigidbody>();
        weaponShooting = GetComponent<WeaponShooting>();
        boxCollider = GetComponent<BoxCollider>();
        if (weaponShooting.equipped)
        {
            pickedUp = true;
            Equip();

        }
        else
        {
            pickedUp = false;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && pickedUp == false)
        {
            Debug.LogError("Player Here");

            if (Input.GetKeyDown(KeyCode.E))
            {
                PickUp();
                boxCollider.isTrigger = false;
            }
        }
    }

    void Equip()
    {
        playerWeapon.weapon = gameObject;
        weaponShooting.equipped = true;
        pickedUp = true;
        gameObject.transform.parent = playerWeapon.transform;
        playerWeapon.weaponRB = weaponRB;
        playerWeapon.ReturnWeapon();
        PlayerAim.aim.UpdateCurrentWeaponStats(weaponShooting.rate, weaponShooting.damage, weaponShooting.ammoAmount);
    }

    void PickUp()
    {
        //Unassign the current variables in player is using
        playerWeapon.weapon.GetComponent<WeaponPickUp>().pickedUp = false;
        playerWeapon.weapon.GetComponent<WeaponShooting>().equipped = false;
        playerWeapon.weapon.transform.parent = null;
        playerWeapon.weapon = null;
        playerWeapon.weaponRB.isKinematic = false;
        playerWeapon.weaponRB = null;
        boxCollider.isTrigger = true;

        //Assign new variables
        Equip();
    }
}
