using UnityEngine;

public class GunRack : MonoBehaviour
{
    public GameObject weaponSelection;
    public GameObject[] weapons;
    public Transform weaponSpawnPoint;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            weaponSelection.SetActive(true);
        }
    }

    public void ChooseWeapon(int weaponID)
    {
        //ID 0 = 44 Magnum
        //ID 1 = Uzi
        //ID 2 = AK47
        GameObject weaponSpawned =  Instantiate(weapons[weaponID], weaponSpawnPoint.position, Quaternion.identity);
        weaponSpawned.GetComponent<WeaponPickUp>().PickUp();
        weaponSelection.SetActive(false);
    }
}
