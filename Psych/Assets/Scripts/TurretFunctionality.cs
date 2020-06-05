using UnityEngine;

public class TurretFunctionality : MonoBehaviour
{
    private float disengageTimer = 0;
    private float shootTimer = 0;
    private Transform target = null;
    private bool realigning = false;

    [Header("Targeting Variables")]
    [SerializeField] private LayerMask mask;
    [SerializeField] private float distance = 15;
    [SerializeField] private float speed = 100;
    [SerializeField] private float cancelTime = 0.4f;

    [Header("Shooting Variables")]
    [SerializeField] private GameObject ammo = null;
    [SerializeField] private float rate = 0.8f;
    [SerializeField] private float force = 80;

    [Header("Turret Parts")]
    [SerializeField] private Transform turretPivot = null;
    [SerializeField] private Transform turretSight = null;
    [SerializeField] private Transform turretMuzzle = null;

    [Header("Oscillation Variables")]
    [SerializeField] private float oscillateSpeed = 0.7f;
    [SerializeField] private float oscillateAngle = 60;

    public ParticleSystem muzzleFlash;

    void Update()
    {
        if (target == null)
        {
            if (realigning)
            {
                Realignment();
            }
            else
            {
                Oscillate();
            }
        }
        else
        {
            Targeting();
        }
    }

    void Targeting()
    {
        Vector3 targetPosition = new Vector3(target.position.x, turretPivot.position.y, target.position.z);
        Vector3 targetDirection = targetPosition - turretPivot.position;
        Vector3 newDirection = Vector3.RotateTowards(turretPivot.forward, targetDirection, speed * Time.deltaTime, 0f);
        turretPivot.rotation = Quaternion.LookRotation(newDirection);

        float half = (360f - (oscillateAngle * 2f)) / 2f;

        if (turretPivot.localRotation.eulerAngles.y > oscillateAngle && turretPivot.localRotation.eulerAngles.y < oscillateAngle + half)
        {
            turretPivot.localRotation = Quaternion.Euler(new Vector3(0, oscillateAngle, 0));
        }
        else if (turretPivot.localRotation.eulerAngles.y < 360 - oscillateAngle && turretPivot.localRotation.eulerAngles.y >= oscillateAngle + half)
        {
            turretPivot.localRotation = Quaternion.Euler(new Vector3(0, 360 - oscillateAngle, 0));
        }

        ShootProjectile();
        DisengageCheck();
    }

    void Oscillate()
    {
        float Y = Mathf.Cos(oscillateSpeed * Time.time) * oscillateAngle;
        turretPivot.localRotation = Quaternion.Euler(0, Y, 0);
        TargetSearch();
    }

    void TargetSearch()
    {
        RaycastHit hit;
        Ray ray = new Ray(turretSight.position, turretSight.forward);
        if (Physics.Raycast(ray, out hit, distance, mask))
        {
            if (hit.collider.tag == "Player")
            {
                target = hit.transform;
            }
        }
    }

    void DisengageCheck()
    {
        RaycastHit contact;
        Ray rc = new Ray(turretSight.position, turretSight.forward);
        if (Physics.Raycast(rc, out contact, distance, mask))
        {
            if (contact.collider.tag == "Player`")
            {
                disengageTimer = 0;
            }
            else
            {
                disengageTimer += Time.deltaTime;
            }
        }
        else
        {
            disengageTimer += Time.deltaTime;
        }

        if (disengageTimer >= cancelTime)
        {
            realigning = true;
            target = null;
        }
    }
    void Realignment()
    {
        float Y = Mathf.Cos(oscillateSpeed * Time.time) * oscillateAngle;
        if (Y < 0)
        {
            Y = 360 + Y;
        }
        float difference = Mathf.Abs(Y - turretPivot.localRotation.eulerAngles.y);

        if (difference <= 10f)
        {
            realigning = false;
        }
        TargetSearch();
    }

    void ShootProjectile()
    {
        shootTimer += Time.deltaTime;
        if (shootTimer >= rate)
        {
            GameObject bullet = Instantiate(ammo, turretMuzzle.position, turretMuzzle.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * force;
            muzzleFlash.Play();
            shootTimer = 0;
        }
    }
}
