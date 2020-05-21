using UnityEngine;

public class WeaponShooting : MonoBehaviour
{
    public GameObject ammo = null;
    public GameObject target;
    public CameraController weaponCam;
    public PlayerMovement playerCam;
    public float damage = 5;
    float shootTimer = 0;
    public float rate = 0;
    public float force = 50;
    public float distance = 20;
    public Transform spawnPoint = null;
    public bool equipped = false;
    public bool thrown = false;

    Rigidbody rb;
    ObjectPooler objectpooler;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        objectpooler = ObjectPooler.instance;

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

    void Start()
    {
        if (equipped)
        {
            PlayerAim.aim.UpdateCurrentWeaponStats(rate, damage);
        }
    }

    public void Update()
    {
        if (equipped)
        {
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
            //ShootProjectile();

            if(thrown == true && Input.GetKeyDown(KeyCode.Alpha3) && WeaponThrow.weaponThrow.isReturning == false)
            {
                Debug.Log("Pressed");
                CameraManager.cameraManager.ActivateWeaponCamera();
                CameraManager.cameraManager.cameraController.target = transform;
                rb.isKinematic = true;
                transform.rotation = Quaternion.identity;
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
                GameObject bullet = objectpooler.SpawnFromPool("Bullet", spawnPoint.position, spawnPoint.rotation);
                //GameObject bullet = Instantiate(ammo, spawnPoint.position, spawnPoint.rotation);
                bullet.GetComponent<Damage>().damage = damage;
                bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * force;
                shootTimer = 0;
                //AudioManager.audioManager.Play("GunShot");
            }
        }
    }
}
