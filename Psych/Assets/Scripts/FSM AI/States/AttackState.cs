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

    private SeesPlayer enemyVision;
    private Vector3 targetLastPosition;
    
    private NavMeshAgent enemyAgent;
    public Animator Anim;

    
    private void Start() {
        enemyVision = this.GetComponent<SeesPlayer>();
        Debug.Log("ENTERING ATTACK STATE.");
        enemyAgent = this.GetComponent<NavMeshAgent>();
        Anim = GetComponent<Animator>();
    }

    private void Update() {
        //If the enemy can see the target
        if(enemyVision.CheckIfTargetIsWithinVision()){
            
            this.transform.LookAt(enemyVision.getTargetObjectTransform());
            RememberTargetLastPosition(true);
            GetInAttackingRangeOfTarget();
            
        }else{
            MoveTowardsTarget();
        }
    }

    
    private void RememberTargetLastPosition(bool rememberPosition){
        if(rememberPosition){
            //Debug.Log("I remember where I saw the target.");
            targetLastPosition = enemyVision.getTargetObjectTransform().position;
        }else{
            targetLastPosition = Vector3.zero;
        }
    }

    private void GetInAttackingRangeOfTarget(){
        if(enemyVision.CheckIfTargetIsWithinVision() ){
            Anim.SetBool("CanAttack", true);
            AttackTarget();
        }else{

            MoveTowardsTarget();
        }
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

    private void MoveTowardsTarget(){
        //Debug.Log("I'm coming after you!");
        // transform.position = Vector3.MoveTowards(this.transform.position, targetPosition, movementSpeed * Time.deltaTime);
        enemyAgent.SetDestination(targetLastPosition);
    }



}
