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

    private bool isAlerted = false;

    private void Update() {
        AlertOtherMinionsOfTargetWithinVision();
        
    }

    public Transform getTargetObjectTransform(){
        return targetObject;
    }

    public bool CheckIfTargetIsWithinVision(){
        Vector3 targetDirection = targetObject.position - transform.position;
        float angle = Vector3.Angle(targetDirection, transform.forward);

        //Debug.Log("Target Direction: " + targetDirection);
        //Debug.Log("angle: " + angle);
        //Debug.Log("Vector3 Distance between player and Enemy: " + targetDirection.magnitude);

        

        return angle < visionRange && ( targetDirection.magnitude < maxVisionDistance && targetDirection.magnitude >= 0);

    }

    public override bool IsSatisfied(FsmState curr, FsmState next){

        return CheckIfTargetIsWithinVision();
    }




    public void AlertOtherMinionsOfTargetWithinVision(){
        
        if(CheckIfTargetIsWithinVision()){
            
            foreach(Transform currentEnemyInRoom in this.transform.parent.transform){
                WasAlerted enemyAlerted = currentEnemyInRoom.gameObject.GetComponent<WasAlerted>();
                if(enemyAlerted != null && !currentEnemyInRoom.gameObject.Equals(this.gameObject))
                {
                    Debug.Log(this.gameObject.name + ": Alerting " + currentEnemyInRoom.gameObject.name + " with target position " + targetObject.position );
                    enemyAlerted.AlertWithTargetLastKnownPosition(targetObject.position);
                }
            }
        }
        
    } 
}
