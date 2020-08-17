using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVision : MonoBehaviour
{

    [SerializeField]
    private Transform targetObject;
    [SerializeField]
    private float visionRange = 5.0f;
    [SerializeField]
    private float maxVisionDistance = 15.0f;
    [SerializeField]
    private bool targetInSight = false;

    private void Start()
    {
        targetObject = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update()
    {
        targetInSight = CheckIfTargetIsWithinVision();

        if(Input.GetKeyDown(KeyCode.T))
        {
            GetComponent<AttackState>().currentWeapon = GetComponent<AttackState>().C_Stolen;
        }

        //Debug.Log("TRULY Able to see target: " + targetInSight);
    }

    public bool GetTargetInSight()
    {
        return targetInSight;
    }



    public bool CheckIfTargetIsWithinVision()
    {
        Vector3 targetDirection = targetObject.position - transform.position;
        float angle = Vector3.Angle(targetDirection, transform.forward);

        //Debug.Log("Target Direction: " + targetDirection);
        //Debug.Log("angle: " + angle);
        //Debug.Log("Vector3 Distance between player and Enemy: " + targetDirection.magnitude);

        //bool ableToSeeTarget = angle < visionRange && (targetDirection.magnitude < maxVisionDistance && targetDirection.magnitude >= 0);


        return angle < visionRange && (targetDirection.magnitude < maxVisionDistance && targetDirection.magnitude >= 0);

    }

    public Transform getTargetObjectTransform()
    {
        return targetObject;
    }

}
