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

    [SerializeField]
     Animator Anim;
    private PlayerVision enemyVision;
    public Transform GunHand;
    public Vector3 storedWeaponEulerAngleRotation;
    public bool CanAttackAnimVar = false;
    
    private void Start() {
        enemyVision = this.GetComponent<PlayerVision>();
        enemyAgent = this.GetComponent<NavMeshAgent>();
        Anim = GetComponent<Animator>();
        storedWeaponEulerAngleRotation = currentWeapon.transform.localEulerAngles;
    }



    private void Update() {
        //If the enemy can see the target
        CanAttackAnimVar = Anim.GetBool("CanAttack");
        
        if(enemyVision.GetTargetInSight()){

            this.enemyAgent.isStopped = true;
            this.transform.LookAt(enemyVision.getTargetObjectTransform());
            RememberTargetLastPosition();
            GetInAttackingRangeOfTarget();
            
        }else{

            Anim.SetBool("CanAttack", false);
            MoveTowardsTarget();
        }
    }

    
    private void RememberTargetLastPosition(){
        
        //Debug.Log("I remember where I saw the target.");
        targetLastPosition = enemyVision.getTargetObjectTransform().position;
        
    }

    private void GetInAttackingRangeOfTarget(){
        if (enemyVision.GetTargetInSight())
        {
            Debug.LogWarning(this.gameObject.name + ": I'm attacking now!");
            AttackTarget();
        }
        else
        {

            MoveTowardsTarget();
        }
    }

    private void AttackTarget(){
        
        //AimWeaponAtTarget();
        UseWeaponAtTarget();
    }

    private bool AimWeaponAtTarget()
    {
        bool foundTheTarget = false;
        if (foundTheTarget)
        {
            //Debug.Log("Found the target");
            currentWeapon.transform.LookAt(enemyVision.getTargetObjectTransform());

        }
        else
        {

            //Debug.Log("Aiming - ( -_･) ︻デ═一");
        }

        return foundTheTarget;
    }

    private void UseWeaponAtTarget(){
        currentWeapon.GetComponent<WeaponShooting>().EnemyShootProjectile();

        Debug.Log(this.gameObject.name + "Attacking0");
        //Anim.SetBool("CanAttack", true);
    }

    private void MoveTowardsTarget(){
        //Debug.Log("I'm coming after you!");
        // transform.position = Vector3.MoveTowards(this.transform.position, targetPosition, movementSpeed * Time.deltaTime);
        enemyAgent.isStopped = false;
        enemyAgent.SetDestination(targetLastPosition);
    }

    public void WeapSteal()
    {
        C_Stolen = currentWeapon;
        //GetComponent<PanicState>().RunToGunRack();
    }

    public void CallFireTheProjectile()
    {
        currentWeapon.GetComponent<WeaponShooting>().FireTheProjectile();
    }

}
