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
    public float bulletSpeed = 50f;
    private bool firing1;

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
       

        shotReady = false;
        StartCoroutine(FireRate());
    }

    IEnumerator FireRate()
    {
        yield return new WaitForSeconds(fireTimer);
        shotReady = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            target = other.gameObject;
            targetIsLocked = true;
        }
    }

}
