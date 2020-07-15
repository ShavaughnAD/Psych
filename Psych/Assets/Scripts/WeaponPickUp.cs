using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{
    public WeaponThrow playerWeapon;
    public CameraController camController;
    public Camera weaponCamera;
    public Sprite weaponIcon;
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
        playerWeapon.weapon.GetComponent<WeaponShooting>().enabled = true;
        weaponShooting.equipped = true;
        pickedUp = true;
        gameObject.transform.parent = playerWeapon.transform;
        playerWeapon.weaponRB = weaponRB;
        playerWeapon.ReturnWeapon();
        PlayerAim.aim.UpdateCurrentWeaponStats(weaponShooting.rate, weaponShooting.damage, weaponShooting.ammoAmount, weaponShooting.bulletTracer);
    }

    void PickUp()
    {
        //Unassign the current variables in player is using
        if (playerWeapon != null)
        {
            playerWeapon.weapon.GetComponent<BoxCollider>().enabled = true;
            playerWeapon.weapon.GetComponent<WeaponPickUp>().pickedUp = false;
            playerWeapon.weapon.GetComponent<WeaponShooting>().equipped = false;
            CameraManager.cameraManager.cameraController.enabled = false;
            playerWeapon.weapon.transform.parent = null;
            playerWeapon.weapon = null;
            playerWeapon.weaponRB.isKinematic = false;
            playerWeapon.weaponRB = null;
        }
        CameraManager.cameraManager.AssignNewWeaponCam(camController, weaponCamera);
        //Assign new variables
        Equip();
    }
}
