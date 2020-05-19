using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    public static PlayerAim aim;
    public LayerMask enemyMask;
    public LayerMask pickUpMask;

    float shootTimer = 0;
    float rate = 0;
    float damage;

    void Awake()
    {
        aim = this;
    }

    void Update()
    {
        Vector3 centerScreen = new Vector3(0.5f, 0.5f, 100);
        Ray ray = Camera.main.ViewportPointToRay(centerScreen);
        RaycastHit hit;

        shootTimer += Time.deltaTime;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, enemyMask))
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                if(shootTimer >= rate)
                {
                    Debug.LogError(WeaponThrow.weaponThrow.weapon.GetComponent<WeaponShooting>().damage);
                    hit.collider.GetComponent<Health>().TakeDamage(damage);
                    shootTimer = 0;

                    //AudioManager.audioManager.Play("GunShot");
                }
            }
        }
        if(Physics.Raycast(ray, out hit, Mathf.Infinity, pickUpMask))
        {
            //PickUp Object
        }
    }

    public void UpdateCurrentWeaponStats(float weaponRateOfFire, float weaponDamage)
    {
        damage = weaponDamage;
        rate = weaponRateOfFire;
    }
}
