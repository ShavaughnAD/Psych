using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleTurret : MonoBehaviour
{

    private GameObject target;
    public GameObject TurretHead1;
    public GameObject TurretHead2;
    public GameObject Bullet;
    /*public GameObject BulletRotation;*/
    public GameObject BulletSpawnPoint1;
    public GameObject BulletSpawnPoint2;

    private bool targetIsLocked;
    public float fireTimer;
    private bool shotReady;
    private bool firing1;
    
    public float distance = 15f;
    public LayerMask layer;
    private float oscillateSpeed = 0.7f;
    private float oscillateAngle = 60;
    private float disengageTimer = 0;
    private float cancelTime = 0.4f;
    private bool realigning = false;

    void Start()
    {
        shotReady = true;
        firing1 = false;
    }

    void Update()
    {

        if (targetIsLocked)
        {
            TurretHead1.transform.LookAt(target.transform);
            TurretHead2.transform.LookAt(target.transform);

            if (shotReady)
            {
                Shoot();
            }
        }
        else
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
    }

    void Shoot()
    {
        if (!firing1)
        {
            Transform _bullet = Instantiate(Bullet.transform, BulletSpawnPoint1.transform.position, BulletSpawnPoint1.transform.rotation);
            _bullet.transform.rotation = BulletSpawnPoint1.transform.rotation;
            _bullet.transform.Rotate(-90, 0, 0);
            firing1 = true;
        }
        else
        {
            Transform _bullet = Instantiate(Bullet.transform, BulletSpawnPoint2.transform.position, BulletSpawnPoint2.transform.rotation);
            _bullet.transform.rotation = BulletSpawnPoint2.transform.rotation;
            /*_bullet.transform.Rotate(-90, 0, 0);*/
            firing1 = false;
        }
        DisengageCheck();

         shotReady = false;
        StartCoroutine(FireRate());
    }

    IEnumerator FireRate()
    {
        yield return new WaitForSeconds(fireTimer);
        shotReady = true;
    }

    /*void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            target = other.gameObject;
            targetIsLocked = true;
        }
    }*/

    void TargetSearch()
    {
        RaycastHit hit;
        Ray ray = new Ray(BulletSpawnPoint2.transform.position, BulletSpawnPoint2.transform.forward);
        if(Physics.Raycast(ray, out hit, distance, layer))
        {
            if(hit.collider.tag == "Player")
            {
                target = hit.collider.gameObject;
                targetIsLocked = true; 
            }
        }
    }
    void Oscillate()
    {
        float Y = Mathf.Cos(oscillateSpeed * Time.time) * oscillateAngle;
        TurretHead1.transform.localRotation = Quaternion.Euler(0, Y, 0);
        TurretHead2.transform.localRotation = Quaternion.Euler(0, Y, 0);
        TargetSearch();
    }

    void DisengageCheck()
    {
        RaycastHit contact;
        Ray rc = new Ray(BulletSpawnPoint2.transform.position, BulletSpawnPoint2.transform.forward);
        if (Physics.Raycast(rc, out contact, distance, layer))
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
            targetIsLocked = false;
        }
    }
    void Realignment()
    {
        float Y = Mathf.Cos(oscillateSpeed * Time.time) * oscillateAngle;
        if (Y < 0)
        {
            Y = 360 + Y;
        }
        float difference1 = Mathf.Abs(Y - TurretHead1.transform.localRotation.eulerAngles.y);
        float difference2 = Mathf.Abs(Y - TurretHead2.transform.localRotation.eulerAngles.y);

        if ((difference1 <= 10f) || (difference2 <= 10f))
        {
            realigning = false;
        }
        TargetSearch();
    }
}
