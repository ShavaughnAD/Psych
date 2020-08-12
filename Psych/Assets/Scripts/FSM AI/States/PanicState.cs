using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PanicState : MonoBehaviour
{

    private GameObject weaponCrate;
    private NavMeshAgent navMeshAgent;
    private WasAlerted wasAlerted;
    private PlayerVision vision;
    


    private void Start()
    {
        wasAlerted = this.GetComponent<WasAlerted>();
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        vision = this.GetComponent<PlayerVision>();
    }


    private void Update()
    {
        if (WeaponCrateIsNearby())
        {
            GoToWeaponCrateAndEquipWeapon();
        }
        else
        {
            RunAwayFromTarget();
        }
    }

    private bool WeaponCrateIsNearby()
    {
        return true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == weaponCrate) {
            

        }
    }

    private void GoToWeaponCrateAndEquipWeapon()
    {
        if(weaponCrate != null)
        {
            navMeshAgent.SetDestination(navMeshAgent.transform.position);
        }
    }

    private void RunAwayFromTarget()
    {
        Debug.Log("");
    }

}
