using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeesPlayer : FsmCondition
{
    [SerializeField]
    private Transform targetObject = null;
    [SerializeField]
    private float visionRange = 5.0f;
    [SerializeField]
    private float maxVisionDistance = 15.0f;
    [SerializeField]
    private bool canSeeTarget = false;
    private PlayerVision playerVision;

    private void Start()
    {
        playerVision = this.GetComponent<PlayerVision>();
        targetObject = GameObject.FindWithTag("Player").transform;

        if(playerVision == null)
        {
            Debug.LogError("Error: Missing Player Vision script on " + this.gameObject.name + " object");
        }
    }


    private void Update() {
        AlertOtherMinionsOfTargetWithinVision();
        canSeeTarget = playerVision.GetTargetInSight();

        //if (canSeeTarget)
        //{
        //    Debug.Log("I can see the target");
        //}
        //else
        //{
        //    Debug.Log("Where is the target?");
        //}
        
    }

    //public Transform getTargetObjectTransform(){
    //    return targetObject;
    //}

    //public bool CheckIfTargetIsWithinVision(){
    //    Vector3 targetDirection = targetObject.position - transform.position;
    //    float angle = Vector3.Angle(targetDirection, transform.forward);

    //    //Debug.Log("Target Direction: " + targetDirection);
    //    //Debug.Log("angle: " + angle);
    //    //Debug.Log("Vector3 Distance between player and Enemy: " + targetDirection.magnitude);

    //    bool ableToSeeTarget = angle < visionRange && (targetDirection.magnitude < maxVisionDistance && targetDirection.magnitude >= 0);

    //    Debug.Log("Able to see target: " + ableToSeeTarget);



    //    return ableToSeeTarget;

    //}

    public override bool IsSatisfied(FsmState curr, FsmState next){

        //if (canSeeTarget)
        //{
        //    Debug.Log("I can see the target");
        //}
        //else
        //{
        //    Debug.Log("Where is the target?");
        //}
        return canSeeTarget;
    }

    private void AlertOtherMinionsOfTargetWithinVision(){
        
        if(canSeeTarget){
            
            foreach(Transform currentEnemyInRoom in this.transform.parent.transform){
                WasAlerted enemyAlerted = currentEnemyInRoom.gameObject.GetComponent<WasAlerted>();
                if(enemyAlerted != null && !currentEnemyInRoom.gameObject.Equals(this.gameObject))
                {
                    //Debug.Log(this.gameObject.name + ": Alerting " + currentEnemyInRoom.gameObject.name + " with target position " + targetObject.position );
                    enemyAlerted.AlertWithTargetLastKnownPosition(targetObject.position);
                }
            }
        }
        
    } 
}
