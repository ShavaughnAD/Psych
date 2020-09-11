using UnityEngine;
using UnityEngine.UI;

public class PlayerAim : MonoBehaviour
{
    public PlayerAim aim;
    public LayerMask enemyMask;
    public LayerMask pickUpMask;
    public LayerMask weaponMask;

    public Image crosshair;

    float shootTimer = 0;
    public float rate = 0;
    public float bulletForce = 30;

    public ParticleSystem muzzleFlash;
    public GameObject psychicAttack;
    public GameObject impactEffect;
    public Transform psychicAttackSpawnPoint;
    public Camera cam;
    Vector3 centerScreen;
    public AudioSource shootingAudio;

    #region Picking Up Objects Variables

    public Transform _selection;
    public Rigidbody selectedObjectRB = null;
    public Collider selectedObjectCol = null;
    GameObject attractTarget;
    Vector3 targetPosition;
    Vector3 objectRBPosition;
    Rigidbody objectRB = null;
    [SerializeField] Rigidbody objectRBinHand = null;
    [SerializeField] Collider objectColinHand = null;
    bool isPowerSufficient = false;
    [SerializeField] float attractSpeed = 7f;
    [SerializeField] float throwSpeed = 30f;
    public bool isCarryingObject = false;
     public bool isAttracting = false;

    #endregion

    void Awake()
    {
        //aim = this;
        shootingAudio = GetComponent<AudioSource>();
    }

    void Start()
    {
        attractTarget = GameObject.Find("Target Left");
    }

    void Update()
    {
        if (isAttracting)
            MoveToTarget();

         centerScreen = new Vector3(0.5f, 0.5f, 100);
     
        Ray ray = cam.ViewportPointToRay(centerScreen);
        RaycastHit hit;

        #region ShootingWeapon

        shootTimer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Mouse0) && PowerManager.powerManager.power - 5 >= 0 && PowerManager.powerManager.isStasis == false && Time.timeScale != 0)
        {
            if (shootTimer >= rate)
            {
                GameObject psychicBlast = Instantiate(psychicAttack, psychicAttackSpawnPoint.position, Quaternion.identity);
                Vector3 trueScreenPoint = cam.ScreenToWorldPoint(Input.mousePosition);         
                psychicBlast.transform.LookAt(trueScreenPoint);
                Vector3 someforce = (cam.transform.forward) * 100.0f * throwSpeed * Time.fixedDeltaTime;
                psychicBlast.GetComponent<Rigidbody>().AddRelativeForce(someforce, ForceMode.VelocityChange);
                PowerManager.powerManager.DecrementPower(5.0f);
                //muzzleFlash.Play();
                shootTimer = 0;
                //AudioManager.audioManager.Play("GunShot", shootingAudio);

                //The line below doesn't work ;(
                //Returning Null Reference for Some Strange Reason. Check for Duplicate Script or Debug
                // GameObject impact = Instantiate(impactEffect, hit.collider.transform.position, hit.collider.transform.rotation);
                //Destroy(impact, 2);
            }
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, enemyMask))
            {
                //crosshair.color = Color.red;
                //Debug.Log(hit.collider.name);
                //if (hit.collider.GetComponent<Health>() != null)
                //{
                //    hit.collider.GetComponent<Health>().TakeDamage(damage);
                //}
                //if (hit.collider.GetComponent<Rigidbody>() != null)
                //{
                //    Debug.Log("Rigidbody Detected");
                //    hit.collider.GetComponent<Rigidbody>().AddForce(-hit.normal * bulletForce);
                //}
            }
            else
            {
                //crosshair.color = Color.white;
            }
        }

        #endregion

        if (_selection != null)
        {
            selectedObjectRB = null;
            _selection = null;
        }

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, pickUpMask))
        {
            var selection = hit.transform;
            if (selection.CompareTag("Selectable") || selection.CompareTag("Enemy"))
            {
                selectedObjectRB = selection.GetComponent<Rigidbody>();
                selectedObjectCol = selection.GetComponent<Collider>();

                _selection = selection;
            }
        }
        //if(Input.GetKey(KeyCode.R))
        //{
        //    PowerManager.powerManager.DecrementPower(-0.05f);
        //}
        #region StealWeapon

        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    //Debug.LogError("Q has been pressed");
        //    if (Physics.Raycast(ray, out hit, Mathf.Infinity, weaponMask))
        //    {
        //        //Debug.LogError("Weapon Mask");
        //        if (hit.transform.GetComponent<AttackState>().currentWeapon != null)
        //        {
        //            if (hit.transform.GetComponent<AttackState>().currentWeapon.GetComponent<WeaponPickUp>().canBeStolen)
        //            {
        //                hit.transform.GetComponent<AttackState>().WeapSteal();
        //                hit.transform.GetComponent<AttackState>().currentWeapon.GetComponent<WeaponPickUp>().PickUp();
        //                hit.transform.GetComponent<AttackState>().currentWeapon = null;
        //            }
        //        }
        //    }
        //}
        #endregion
    }

    #region Pick Up & Throw Objects

    public void ObjectPickUP()
    {
        if(selectedObjectRB != null)
        {
            objectRB = selectedObjectRB;
            //selectedObjectCol.enabled = true; //Make the Collider a variable later to save the Frames :D
            objectRB.rotation = Quaternion.Euler(Vector3.zero);

            if (objectRB != null && isCarryingObject != true)
            {
                isPowerSufficient = PowerManager.powerManager.DecrementPower(5f);
                if (!isPowerSufficient)
                {
                    return;
                }
                objectColinHand = selectedObjectCol;
                selectedObjectCol.enabled = false;
                objectRBinHand = objectRB;
                objectRBinHand.isKinematic = true;
                isCarryingObject = true;
                isAttracting = true;
            }
        }
    }

    public void ThrowObject()
    {
        if (isCarryingObject == true)
        {
            isCarryingObject = false;
            objectRBinHand.transform.parent = null;
            objectRBinHand.isKinematic = false;
            objectRBinHand.collisionDetectionMode = CollisionDetectionMode.Continuous;
            objectColinHand.enabled = true;
            objectRBinHand.tag = "Selectable";
            //Vector3 someforce = CameraManager.cameraManager.playerCam.transform.TransformDirection(centerScreen) * throwSpeed * Time.fixedDeltaTime;

            Vector3 trueScreenPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            objectRBinHand.transform.LookAt(trueScreenPoint);
            Vector3 someforce = (cam.transform.forward) * 100.0f * throwSpeed * Time.fixedDeltaTime;
            objectRBinHand.AddRelativeForce(someforce, ForceMode.VelocityChange);

            objectColinHand = null;
            objectRBinHand = null;
            objectRB = null;
        }
    }

    void MoveToTarget()
    {
        if(objectRBinHand != null)
        {
            objectRBPosition = objectRBinHand.position;
            targetPosition = attractTarget.transform.position;
            float speed = attractSpeed * Time.deltaTime;
            if (objectRBPosition == targetPosition)
            {
                objectRBinHand.transform.parent = GameObject.FindGameObjectWithTag("Player").transform;
               // objectRBinHand.tag = "NotSelectable";
                isAttracting = false;
                return;
            }
            objectRBinHand.position = Vector3.MoveTowards(objectRB.position, targetPosition, speed);
        }
        
    }

    #endregion

    public void EmptyHand()
    {
        if(isCarryingObject == true)
        {
            isCarryingObject = false;
            objectRBinHand.isKinematic = false;
            objectRBinHand.transform.parent = null;
            objectColinHand.enabled = true;
            objectRBinHand.tag = "Selectable";
            _selection = null;
        }
    }
}