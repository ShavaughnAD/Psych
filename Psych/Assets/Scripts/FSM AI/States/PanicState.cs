using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PanicState : FsmState
{
    [SerializeField]
    private GameObject weaponRack;
    private Vector3 lastKnownTargetPosition;
    private NavMeshAgent navMeshAgent;
    private WasAlerted wasAlerted;
    private PlayerVision vision;
    private AttackState attackState;
    private bool lookingForWeapon = true;
    


    private void Start()
    {
        wasAlerted = this.GetComponent<WasAlerted>();
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        vision = this.GetComponent<PlayerVision>();
        attackState = this.GetComponent<AttackState>();
    }

    private void OnEnable()
    {
        if (vision.GetTargetInSight())
        {
           lastKnownTargetPosition = vision.getTargetObjectTransform().position;
        }
    }

    private void Update()
    {
        if (WeaponCrateIsNearby() )
        {
            GoToWeaponRackAndEquipWeapon();
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
        if(other.gameObject == weaponRack && lookingForWeapon) {

            Debug.Log("Attempting to equip weapon...");
            lookingForWeapon = false;
            wasAlerted.AlertWithTargetLastKnownPosition(lastKnownTargetPosition);
        }
    }

    private void GoToWeaponRackAndEquipWeapon()
    {
        if(weaponRack != null && lookingForWeapon)
        {
            navMeshAgent.SetDestination(navMeshAgent.transform.position);
        }
        else
        {
            Debug.LogWarning("Weapon rack property is set to null");
        }

    }
    
    private void RunAwayFromTarget()
    {
         
    }

}
