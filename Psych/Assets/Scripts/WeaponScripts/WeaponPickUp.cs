using UnityEngine;
using UnityEngine.UI;
public class WeaponPickUp : MonoBehaviour
{
    public CameraController camController;
    public Camera weaponCamera;
    public ParticleSystem weaponControlledParticle;
    public Sprite weaponIcon;
    public Sprite weaponCrosshair;
    WeaponShooting weaponShooting;
    Rigidbody weaponRB = null;
    public bool pickedUp = false;
    public bool canBeStolen = true;
    public bool wasStolen;
    AttackState enemyAttackState;
    bool isPlayerHere = false;

    void Awake()
    {
        weaponRB = GetComponent<Rigidbody>();
        weaponShooting = GetComponent<WeaponShooting>();

        if(transform.parent == null)
        {
            GetComponent<BoxCollider>().enabled = true;
            pickedUp = false;
            weaponControlledParticle.gameObject.SetActive(false);
            return;
        }
        else
        {
            switch (transform.parent.tag)
            {
                case "Player":
                    pickedUp = true;
                    Equip();
                    break;

                case "Enemy":
                    pickedUp = false;
                    GetComponent<BoxCollider>().enabled = false;
                    GetComponent<Rigidbody>().useGravity = false;
                    weaponControlledParticle.gameObject.SetActive(false);
                    enemyAttackState = transform.parent.GetComponent<AttackState>();
                    break;
            }
        }
        //if (weaponShooting.equipped)
        //{
        //    pickedUp = true;
        //    Equip();

        //}
        //else
        //{
        //    pickedUp = false;
        //    weaponControlledParticle.gameObject.SetActive(false);
        //}

        //if(transform.parent.tag == "Enemy")
        //{
        //    enemyAttackState = transform.parent.GetComponent<AttackState>();
        //}
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

    public void Equip()
    {
        WeaponThrow.weaponThrow.weapon = gameObject;
        WeaponThrow.weaponThrow.weapon.GetComponent<WeaponShooting>().enabled = true;
        weaponShooting.equipped = true;
        pickedUp = true;
        gameObject.transform.parent = WeaponThrow.weaponThrow.transform;
        WeaponThrow.weaponThrow.weaponRB = weaponRB;
        PlayerAim.aim.UpdateCurrentWeaponStats(weaponShooting.rate, weaponShooting.damage, weaponShooting.ammoAmount, weaponShooting.bulletTracer);
        WeaponThrow.weaponThrow.controlledParticle = weaponControlledParticle;
        weaponControlledParticle.gameObject.SetActive(true);
        WeaponThrow.weaponThrow.ReturnWeapon();
        PauseMenu.pauseMenuRef.crosshairImage.sprite = weaponCrosshair;
        PauseMenu.pauseMenuRef.weaponIconDisplay.sprite = weaponIcon;

    }

    public void PickUp()
    {
        //Unassign the current variables in player is using
        if (WeaponThrow.weaponThrow.weapon != null)
        {
            WeaponThrow.weaponThrow.weapon.GetComponent<BoxCollider>().enabled = true;
            WeaponThrow.weaponThrow.weapon.GetComponent<WeaponPickUp>().pickedUp = false;
            WeaponThrow.weaponThrow.weapon.GetComponent<WeaponShooting>().equipped = false;
            CameraManager.cameraManager.cameraController.enabled = false;
            WeaponThrow.weaponThrow.weapon.transform.parent = null;
            WeaponThrow.weaponThrow.weapon = null;
            WeaponThrow.weaponThrow.weaponRB.useGravity = true;
            WeaponThrow.weaponThrow.weaponRB.isKinematic = false;
            WeaponThrow.weaponThrow.controlledParticle.gameObject.SetActive(false);
        }
        else
        {
            Equip();
        }
        CameraManager.cameraManager.AssignNewWeaponCam(camController, weaponCamera);
        if(enemyAttackState != null)
        {
            enemyAttackState.currentWeapon = null;
        }
        //Assign new variables
        Equip();
    }
}
