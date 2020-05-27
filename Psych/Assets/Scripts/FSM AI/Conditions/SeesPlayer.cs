using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeesPlayer : MonoBehaviour
{
    [SerializeField]
    private Transform targetObject = null;
    [SerializeField]
    private float visionRange = 5.0f;
    [SerializeField]
    private float maxVisionDistance = 15.0f;

    private void Update() {
        if(CheckIfTargetIsWithinVision()){
            
        Debug.Log("Enemy can see the player" );
        }else{
        Debug.Log("Player Out of sight" );
        }
    }

    private bool CheckIfTargetIsWithinVision(){
        Vector3 targetDirection = targetObject.position - transform.position;
        float angle = Vector3.Angle(targetDirection, transform.forward);

        //Debug.Log("Target Direction: " + targetDirection);
        //Debug.Log("angle: " + angle);
        //Debug.Log("Vector3 Distance between player and Enemy: " + targetDirection.magnitude);


        return angle < visionRange && ( targetDirection.magnitude < maxVisionDistance && targetDirection.magnitude >= 0)  ;
    }

}
