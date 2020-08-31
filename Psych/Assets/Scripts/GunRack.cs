using UnityEngine;

public class GunRack : MonoBehaviour
{
    //public GameObject weaponSelection;
    //public GameObject[] weapons;
    //public Transform weaponSpawnPoint;
    public int CurInRack;
    void OnTriggerEnter(Collider other)
    {
        //if(other.tag == "Player")
        //{
        //    weaponSelection.SetActive(true);
        //}
        if(other.tag == "Enemy" && other.GetComponent<AttackState>().currentWeapon == null && CurInRack > 0)
        {
            AttackState enemyAttackState = other.GetComponent<AttackState>();
            enemyAttackState.currentWeapon = Instantiate(enemyAttackState.C_Stolen, enemyAttackState.GunHand.position, new Quaternion(0,0,0,0), enemyAttackState.GunHand);
            enemyAttackState.currentWeapon.transform.localScale = new Vector3(0.00007f, 0.00007f, 0.00007f);
            enemyAttackState.currentWeapon.transform.localEulerAngles = enemyAttackState.storedWeaponEulerAngleRotation;      
        }
        else if(CurInRack < 0)
        {
            //Panic
        }
    }

    //public void ChooseWeapon(int weaponID)
    //{
    //    //ID 0 = 44 Magnum
    //    //ID 1 = Uzi
    //    //ID 2 = AK47
    //    GameObject weaponSpawned =  Instantiate(weapons[weaponID], weaponSpawnPoint.position, Quaternion.identity);
    //    weaponSpawned.GetComponent<WeaponPickUp>().PickUp();
    //    weaponSelection.SetActive(false);
    //}
}
