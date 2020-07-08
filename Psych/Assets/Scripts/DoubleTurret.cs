using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleTurret : MonoBehaviour
{
    private float disengageTimer = 0;
    private Transform target = null;
    private bool realigning = false;
    private bool shotReady = true;
    private bool firing1 = false;

    [Header("Targeting Variables")]
    [SerializeField] private LayerMask mask;
    [SerializeField] private float distance = 15;
    [SerializeField] private float speed = 100;
    [SerializeField] private float cancelTime = 0.4f;

    [Header("Shooting Variables")]
    [SerializeField] private GameObject ammo = null;
    [SerializeField] private float rate = 0.75f;

    [Header("Turret Parts")]
    [SerializeField] private Transform turretPivot = null;
    [SerializeField] private Transform turretSight = null;
    [SerializeField] private Transform turretLeftMuzzle = null;
    [SerializeField] private Transform turretRightMuzzle = null;
    [SerializeField] private ParticleSystem leftMuzzleFlash;
    [SerializeField] private ParticleSystem rightMuzzleFlash;

    [Header("Oscillation Variables")]
    [SerializeField] private float oscillateSpeed = 0.7f;
    [SerializeField] private float oscillateAngle = 60;

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
        if (shotReady)
        {
            if (!firing1)
            {
                GameObject bullet = Instantiate(ammo, turretLeftMuzzle.position, turretLeftMuzzle.rotation);
                leftMuzzleFlash.Play();
                firing1 = true;
            }
            else
            {
                GameObject shot = Instantiate(ammo, turretRightMuzzle.position, turretRightMuzzle.rotation);
                rightMuzzleFlash.Play();
                firing1 = false;
            }
            shotReady = false;
            StartCoroutine(FireRate());

        }
    }

    IEnumerator FireRate()
    {
        yield return new WaitForSeconds(rate);
        shotReady = true;
    }
}
