using UnityEngine;

public class WeaponThrow : MonoBehaviour
{
    #region Public Variables
    public static WeaponThrow weaponThrow;
    public GameObject weapon = null;
    public Rigidbody weaponRB = null;
    public float returnRotSpeed = 50;
    public float throwForce = 50;
    public Transform target = null;
    public Transform curvePoint = null;
    public bool isThrown = false;
    public bool isReturning = false;
    public ParticleSystem controlledParticle;
    #endregion
    #region Private Variables
    Transform player;
    float time = 0;
    Vector3 prevPos = Vector3.zero;

    void Awake()
    {
        weaponThrow = this;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    #endregion
    void Update()
    {
        Vector3 centerScreen = new Vector3(0.5f, 0.5f, 100);
        Ray ray = CameraManager.cameraManager.playerCam.ViewportPointToRay(centerScreen);
        RaycastHit hit;

        #region Input
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if(isReturning)
            {
                return;
            }
            if (isThrown == false)
            {
                ThrowWeapon();
            }
            else
            {
                ReturnWeapon();
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ReturnWeapon();
        }

        #endregion

        if (isReturning)
        {
            if(time < 1)
            {
                weaponRB.position = BezierQCP(time, prevPos, curvePoint.position, target.position);
                weaponRB.rotation = Quaternion.Slerp(weaponRB.transform.rotation, target.rotation, returnRotSpeed * Time.deltaTime);
                time += Time.deltaTime;
            }
            else
            {
                ResetWeapon();
            }
        }

        if (isThrown == false && isReturning == false)
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if(weapon != null)
                {
                    weapon.transform.LookAt(hit.point);
                }
            }
        }
    }

    void ThrowWeapon()
    {
        isThrown = true;
        weapon.GetComponent<WeaponShooting>().thrown = true;
        isReturning = false;
        weaponRB.transform.parent = null;
        weaponRB.isKinematic = false;
        weaponRB.AddForce(Camera.main.transform.TransformDirection(Vector3.forward) * throwForce, ForceMode.Impulse);
        weaponRB.AddTorque(weaponRB.transform.TransformDirection(Vector3.right) * 100, ForceMode.Impulse);
        weapon.GetComponent<Collider>().enabled = true;
        //if (controlledParticle != null)
        //{
        //    controlledParticle.Play();
        //}
        //CameraManager.cameraManager.playerMovement.isBeingControlled = true;
    }

    public void ReturnWeapon()
    {
        time = 0;
        prevPos = weaponRB.position;
        isReturning = true;
        weaponRB.velocity = Vector3.zero;
        weaponRB.isKinematic = true;
        weapon.GetComponent<Collider>().enabled = false;
        CameraManager.cameraManager.ActivatePlayerCamera();
        if(controlledParticle != null)
        {
            controlledParticle.Play();
        }
        //CameraManager.cameraManager.playerMovement.isBeingControlled = true;
    }

    void ResetWeapon()
    {
        isReturning = false;
        isThrown = false;
        weaponRB.transform.parent = transform;
        weaponRB.position = target.position;
        weaponRB.rotation = target.rotation;
        weapon.GetComponent<WeaponShooting>().thrown = false;
        CameraManager.cameraManager.playerMovement.isBeingControlled = false;
        if(controlledParticle != null)
        {
            controlledParticle.Stop();
        }
    }

    Vector3 BezierQCP(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        Vector3 p = (uu * p0) + (2 * u * t * p1) + (tt * p2);
        return p;
    }
}