using UnityEngine;
public class WeaponPickUp : MonoBehaviour
{
    public WeaponThrow playerWeapon;
    public CameraController camController;
    public Camera weaponCamera;
    public ParticleSystem weaponControlledParticle;
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
            weaponControlledParticle.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (isPlayerHere == true && pickedUp == false)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                PickUp();
                GameManager.gameManager.displayWeapon.SetActive(true);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            isPlayerHere = true;
            GameManager.gameManager.displayWeaponImage.sprite = weaponIcon;
            GameManager.gameManager.displayWeapon.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            isPlayerHere = false;
            GameManager.gameManager.displayWeapon.SetActive(false);
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
        PlayerAim.aim.UpdateCurrentWeaponStats(weaponShooting.rate, weaponShooting.damage, weaponShooting.ammoAmount, weaponShooting.bulletTracer);
        WeaponThrow.weaponThrow.controlledParticle = weaponControlledParticle;
        weaponControlledParticle.gameObject.SetActive(true);
        playerWeapon.ReturnWeapon();
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
            playerWeapon.controlledParticle.gameObject.SetActive(false);
        }
        CameraManager.cameraManager.AssignNewWeaponCam(camController, weaponCamera);
        //Assign new variables
        Equip();
    }
}
