using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackState : FsmState
{
    [SerializeField]
    private GameObject currentWeapon;
    [SerializeField]
    private float movementSpeed = 7f;

    private PlayerVision enemyVision;
    private Vector3 targetLastPosition;
    
    private NavMeshAgent enemyAgent;

    
    private void Start() {
        enemyVision = this.GetComponent<PlayerVision>();
        //Debug.Log("ENTERING ATTACK STATE.");
        enemyAgent = this.GetComponent<NavMeshAgent>();
    }

    private void Update() {
        //If the enemy can see the target
        if(enemyVision.GetTargetInSight()){
            
            this.transform.LookAt(enemyVision.getTargetObjectTransform());
            RememberTargetLastPosition();
            GetInAttackingRangeOfTarget();
            
        }else{
            MoveTowardsTargetLastKnownPosition();
        }
    }

    
    private void RememberTargetLastPosition(){
        
        //Debug.Log("I remember where I saw the target.");
        targetLastPosition = enemyVision.getTargetObjectTransform().position;
      
    }

    private void GetInAttackingRangeOfTarget(){

        AttackTarget();
    }

    private void AttackTarget(){
        AimWeaponAtTarget();
        UseWeaponAtTarget();
    }

    private bool AimWeaponAtTarget(){
        bool foundTheTarget = false;
        if(foundTheTarget){
            //Debug.Log("Found the target");
            currentWeapon.transform.LookAt(enemyVision.getTargetObjectTransform());
        
        }else{
            
            //Debug.Log("Aiming - ( -_･) ︻デ═一");
        }

        return foundTheTarget;
    }
    
    private void UseWeaponAtTarget(){
        Debug.Log("Using weapon!");
        currentWeapon.GetComponent<WeaponShooting>().EnemyShootProjectile();
    }

    private void MoveTowardsTargetLastKnownPosition(){
        //Debug.Log("I'm coming after you!");
        // transform.position = Vector3.MoveTowards(this.transform.position, targetPosition, movementSpeed * Time.deltaTime);
        enemyAgent.SetDestination(targetLastPosition);
    }



}
