using System.Collections.Generic;
using UnityEngine;

public class WeaponShooting : MonoBehaviour
{
    public GameObject ammo = null;
    public GameObject target;
    public CameraController weaponCam;
    public float damage = 5;
    float shootTimer = 0;
    public float rate = 0;
    public float force = 50;
    public float distance = 20;
    public int ammoAmount = 30;
    public Transform spawnPoint = null;
    public bool equipped = false;
    public bool thrown = false;
    public int pelletCount;
    public float spreadAngle;
    public float pelletTravelSpeed = 5f;
    public GameObject pellet;
    public ParticleSystem bulletTracer;
    ShotgunHandler shotgunHandler;
    public Animator anim;
    [SerializeField]
    private float projectileFiringWaitTime = .5f;
    private bool enableShooting = true;
    Rigidbody rb;
    ObjectPooler objectpooler;

    public void Awake()
    {
        
        rb = GetComponent<Rigidbody>();
        objectpooler = ObjectPooler.instance;
        if(transform.parent == null)
        {
            equipped = false;
            return;
        }
        else if (transform.parent.tag == "Player")
        {
            equipped = true;
            GetComponent<Collider>().enabled = false;
        }
    }

    public virtual void Start()
    {

        shotgunHandler = GetComponent<ShotgunHandler>();
    }

    public virtual void Update()
    {
        if (equipped)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, distance))
            {
                if (Input.GetKey(KeyCode.Alpha2) && hit.collider.tag == "Enemy")
                {
                    target = hit.collider.gameObject;
                }
            }

            if (target != null)
            {
                transform.LookAt(target.transform);
            }

            shootTimer += Time.deltaTime;
            if (Input.GetKey(KeyCode.Mouse0) && shootTimer >= rate)
            {
                shootTimer = 0;
            }

            if (thrown == true && Input.GetKeyDown(KeyCode.Alpha3) && FindObjectOfType<WeaponThrow>().isReturning == false)
            {
                CameraManager.cameraManager.ActivateWeaponCamera();
                //CameraManager.cameraManager.cameraController.target = transform;
                rb.isKinematic = true;
                transform.rotation = Quaternion.identity;
            }
        }
    }

    public virtual void ShootProjectile()
    {
        //GameObject bullet = objectpooler.SpawnFromPool("Bullet", spawnPoint.position, spawnPoint.rotation);
        GameObject bullet = Instantiate(ammo, spawnPoint.position, spawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * force;
        //AudioManager.audioManager.Play("GunShot   
    }

    public void EnemyShootProjectile()
    {

        shootTimer += Time.deltaTime;

        if (shootTimer >= rate)
        {
            if (enableShooting)
            {

                anim.SetBool("CanAttack", true);
                Invoke("FireTheProjectile", projectileFiringWaitTime);
                enableShooting = false;
            }
            
        }
        else
        {
            enableShooting = true;
            anim.SetBool("CanAttack", false);
        }

    }

    private void FireTheProjectile()
    {
        //Debug.Log("Firing - (⌐■_■)–︻╦╤─<<- - -");
        // GameObject bullet = objectpooler.SpawnFromPool("Bullet", spawnPoint.position, spawnPoint.rotation);
        GameObject bullet = Instantiate(ammo, spawnPoint.position, spawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * force;
        shootTimer = 0;
        //AudioManager.audioManager.Play("GunShot");
    }
}
