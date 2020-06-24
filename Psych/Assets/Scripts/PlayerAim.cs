using UnityEngine;

//Reference: Brackeys. (2017). Shooting with Raycasts - Unity Tutorial. Retrieved from https://www.youtube.com/watch?v=THnivyG0Mvo
public class PlayerAim : MonoBehaviour
{
    public static PlayerAim aim;
    public LayerMask enemyMask;
    public LayerMask pickUpMask;

    float shootTimer = 0;
    float rate = 0;
    float damage;
    public float bulletForce = 30;
    public int ammo;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public Camera cam;
    Vector3 centerScreen;

    //[SerializeField] Material highlightMaterial;
    //[SerializeField] Material defaultMaterial;
    public AudioSource shootingAudio;

    #region Picking Up Objects Variables

    public Transform _selection;
    public Rigidbody selectedObjectRB = null;
    public Collider selectedObjectCol = null;
    GameObject attractTarget;
    Vector3 targetPosition;
    Vector3 objectRBPosition;
    Rigidbody objectRB = null;
    Rigidbody objectRBinHand = null;
    Collider objectColinHand = null;
    bool isPowerSufficient = false;
    [SerializeField] float attractSpeed = 7f;
    [SerializeField] float throwSpeed = 30f;
    public bool isCarryingObject = false;
    public bool isAttracting = false;

    #endregion

    void Awake()
    {
        aim = this;
        //shootingAudio = GetComponent<AudioSource>();
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

        shootTimer += Time.deltaTime;
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (shootTimer >= rate  && ammo  > 0)
            {
                muzzleFlash.Play();
                ammo--;
                shootTimer = 0;
                //AudioManager.audioManager.Play("GunShot", shootingAudio);
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, enemyMask))
                {
                    Debug.Log(hit.collider.name);
                    hit.collider.GetComponent<Health>().TakeDamage(damage);
                }
                if(hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * bulletForce);
                }
                //The line below doesn't work ;(
                //Returning Null Reference for Some Strange Reason. Check for Duplicate Script or Debug
                // GameObject impact = Instantiate(impactEffect, hit.collider.transform.position, hit.collider.transform.rotation);
                //Destroy(impact, 2);
            }
        }


        if (_selection != null)
        {
            //var selectionRenderer = _selection.GetComponent<Renderer>();
            //selectionRenderer.material = defaultMaterial;
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
                //var selectionRenderer = selection.GetComponent<Renderer>();
                //if (selectionRenderer != null)
                //{
                //    selectionRenderer.material = highlightMaterial;
                //    selectedObjectRB = selectionRenderer.GetComponent<Rigidbody>();
                //    selectedObjectCol = selectionRenderer.GetComponent<Collider>();
                //}

                _selection = selection;
            }
        }
    }

    public void UpdateCurrentWeaponStats(float weaponRateOfFire, float weaponDamage, int ammoAmount)
    {
        //Need to Add Camera
        //Need to add ParticleSystem
        damage = weaponDamage;
        rate = weaponRateOfFire;
        ammo = ammoAmount;
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
        //Vector3 targetPoint = selectedObjectRB.transform.position;
        if (isCarryingObject == true)
        {
            isCarryingObject = false;
            objectRBinHand.transform.parent = null;
            objectRBinHand.isKinematic = false;
            objectColinHand.enabled = true;
            objectRBinHand.tag = "Selectable";

            objectRBinHand.AddForce(Camera.main.ViewportPointToRay(centerScreen).direction * throwSpeed, ForceMode.Impulse);

            //Vector3 velocity = SetThrowVelocity(objectRBinHand, targetPoint, throwSpeed);
            //Vector3 velocity = SetThrowVelocity(objectRBinHand, centerScreen, throwSpeed);
            //if (velocity != Vector3.zero)
            //{
                //objectRBinHand.AddForce(velocity, ForceMode.VelocityChange);
            //    objectRBinHand.velocity = (centerScreen - Camera.main.transform.position) * throwSpeed;
            //}
        }
    }

    void MoveToTarget()
    {
        objectRBPosition = objectRBinHand.position;
        targetPosition = attractTarget.transform.position;
        float speed = attractSpeed * Time.deltaTime;
        if (objectRBPosition == targetPosition)
        {
            objectRBinHand.transform.parent = GameObject.FindGameObjectWithTag("Player").transform;
            objectRBinHand.tag = "NotSelectable";
            isAttracting = false;
            return;
        }
        objectRBinHand.position = Vector3.MoveTowards(objectRB.position, targetPosition, speed);
    }

    Vector3 SetThrowVelocity(Rigidbody rigidbody, Vector3 target, float force, float arch = 0.2f)
    {
        Mathf.Clamp(arch, 0, 1);
        var origin = rigidbody.position;
        float x = target.x - origin.x;
        float y = target.y - origin.y;
        float z = target.z - origin.z;
        float gravity = -Physics.gravity.y;
        float b = force * force - y * gravity;
        float discriminant = b * b - gravity * gravity * (x * x + y * y + z * z);
        if (discriminant < 0)
        {
            Debug.Log("Object not in range");
            return Vector3.zero;
        }
        float discriminantSquareRoot = Mathf.Sqrt(discriminant);
        float minTime = Mathf.Sqrt((b - discriminantSquareRoot) * 2) / Mathf.Abs(gravity);
        float maxTime = Mathf.Sqrt((b + discriminantSquareRoot) * 2) / Mathf.Abs(gravity);
        float time = (maxTime - minTime) * arch + minTime;
        float vx = x / time;
        float vy = y / time + time * gravity / 2;
        float vz = z / time;
        Vector3 velocity = new Vector3(vx, vy, vz);
        return velocity;
    }

    #endregion
}
