using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{
    public WeaponThrow playerWeapon;
    WeaponShooting weaponShooting;
    Rigidbody weaponRB = null;
    public bool pickedUp = false;

    bool isPlayerHere = false;

    void Start()
    {
        playerWeapon = GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponThrow>();
        weaponRB = GetComponent<Rigidbody>();
        weaponShooting = GetComponent<WeaponShooting>();
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

    void Update()
    {
        if (isPlayerHere == true && pickedUp == false)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                PickUp();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            isPlayerHere = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            isPlayerHere = false;
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

        //Assign new variables
        Equip();
    }
}
