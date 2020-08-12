using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackState : FsmState
{
    public GameObject currentWeapon;
    public GameObject C_Stolen;
    [SerializeField]
    private float movementSpeed = 7f;

    private Vector3 targetLastPosition;
    
    private NavMeshAgent enemyAgent;
     Animator Anim;
    private PlayerVision enemyVision;
    public Transform GunHand;
    
    private void Start() {
        enemyVision = this.GetComponent<PlayerVision>();
        enemyAgent = this.GetComponent<NavMeshAgent>();
        Anim = GetComponent<Animator>();
    }

    private void Update() {
        //If the enemy can see the target
        if(enemyVision.GetTargetInSight()){
            
            this.transform.LookAt(enemyVision.getTargetObjectTransform());
            RememberTargetLastPosition();
            GetInAttackingRangeOfTarget();
            
        }else{
            MoveTowardsTarget();
        }
    }

    
    private void RememberTargetLastPosition(){
        
        //Debug.Log("I remember where I saw the target.");
        targetLastPosition = enemyVision.getTargetObjectTransform().position;
        
    }

    private void GetInAttackingRangeOfTarget(){
        if(enemyVision.GetTargetInSight() ){
            Anim.SetBool("CanAttack", true);
            AttackTarget();
        }else{

            MoveTowardsTarget();
        }
    }

    private void AttackTarget(){
        
        //AimWeaponAtTarget();
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
        currentWeapon.GetComponent<WeaponShooting>().EnemyShootProjectile();
    }

    private void MoveTowardsTarget(){
        //Debug.Log("I'm coming after you!");
        // transform.position = Vector3.MoveTowards(this.transform.position, targetPosition, movementSpeed * Time.deltaTime);
        enemyAgent.SetDestination(targetLastPosition);
    }

    public void WeapSteal()
    {
        C_Stolen = currentWeapon;
        GetComponent<PatrolState>().RunToGunRack();
    }

}
