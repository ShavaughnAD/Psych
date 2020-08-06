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
        if(other.tag == "Player" && !pickedUp)
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
        FindObjectOfType<WeaponThrow>().weapon = gameObject;
        FindObjectOfType<WeaponThrow>().weapon.GetComponent<WeaponShooting>().enabled = true;
        weaponShooting.equipped = true;
        pickedUp = true;
        gameObject.transform.parent = FindObjectOfType<WeaponThrow>().transform;
        FindObjectOfType<WeaponThrow>().weaponRB = weaponRB;
        GetComponentInParent<PlayerAim>().UpdateCurrentWeaponStats(weaponShooting.rate, weaponShooting.damage, weaponShooting.ammoAmount, weaponShooting.bulletTracer);
        FindObjectOfType<WeaponThrow>().controlledParticle = weaponControlledParticle;
        weaponControlledParticle.gameObject.SetActive(true);
        FindObjectOfType<WeaponThrow>().ReturnWeapon();
        PauseMenu.pauseMenuRef.crosshairImage.sprite = weaponCrosshair;
        PauseMenu.pauseMenuRef.weaponIconDisplay.sprite = weaponIcon;
    }

    public void PickUp()
    {
        //Unassign the current variables in player is using
        if (FindObjectOfType<WeaponThrow>().weapon != null)
        {
            FindObjectOfType<WeaponThrow>().weapon.GetComponent<BoxCollider>().enabled = true;
            FindObjectOfType<WeaponThrow>().weapon.GetComponent<WeaponPickUp>().pickedUp = false;
            FindObjectOfType<WeaponThrow>().weapon.GetComponent<WeaponShooting>().equipped = false;
            CameraManager.cameraManager.cameraController.enabled = false;
            FindObjectOfType<WeaponThrow>().weapon.transform.parent = null;
            FindObjectOfType<WeaponThrow>().weapon = null;
            FindObjectOfType<WeaponThrow>().weaponRB.useGravity = true;
            FindObjectOfType<WeaponThrow>().weaponRB.isKinematic = false;
            FindObjectOfType<WeaponThrow>().controlledParticle.gameObject.SetActive(false);
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
