using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShooting : MonoBehaviour
{
    public GameObject ammo = null;
    public GameObject target;
    public float damage = 5;
    float shootTimer = 0;
    public float rate = 0;
    public float force = 50;
    public float distance = 20;
    public Transform spawnPoint = null;
    public bool equipped = false;
    public bool thrown = false;
    public bool controlling = false;

    Transform playerTrans;
    WeaponThrow weaponThrow;
    Rigidbody rb;
    CameraController cam;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        cam = Camera.main.GetComponent<CameraController>();

        if (transform.parent.tag == "Player")
        {
            equipped = true;
            return;
        }
        else
        {
            equipped = false;
        }
    }

    public void Update()
    {
        if (equipped)
        {
            weaponThrow = GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponThrow>();
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit, distance))
            {
                if(Input.GetKey(KeyCode.Alpha2) && hit.collider.tag == "Enemy")
                {
                    target = hit.collider.gameObject;
                }
            }

            if(target != null)
            {
                transform.LookAt(target.transform);
            }
            ShootProjectile();

            if(thrown && Input.GetKeyDown(KeyCode.Alpha3))
            {
                controlling = true;
                cam.target = transform;
                cam.focusPlayer = false;
                rb.isKinematic = true;
                transform.rotation = Quaternion.identity;
                transform.Rotate(playerTrans.forward);
            }
        }
    }

    public void ShootProjectile()
    {
        shootTimer += Time.deltaTime;
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (shootTimer >= rate)
            {
                GameObject bullet = Instantiate(ammo, spawnPoint.position, spawnPoint.rotation);
                bullet.GetComponent<Damage>().damage = damage;
                bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * force;
                shootTimer = 0;
            }
        }
    }
}
