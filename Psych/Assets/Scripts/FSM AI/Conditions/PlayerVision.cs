using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVision : MonoBehaviour
{

    private Transform targetObject;
    [SerializeField]
    private float visionRange = 5.0f;
    [SerializeField]
    private float maxVisionDistance = 15.0f;
    [SerializeField]
    private bool targetInSight = false;

    public SkinnedMeshRenderer smRenderer;
    public float distanceBetweenTargetAndEnemy;
    public float angleOfVision;
    public float maxAlertRange = 20f;

    private void Start()
    {
        targetObject = GameObject.FindGameObjectWithTag("Player").transform;
    }    private void Update()
    {
        targetInSight = CheckIfTargetIsWithinVision();
        AlertOtherEnemiesOfTargetWithinVision();
        //Debug.Log(this.gameObject.name + " - Can see player: " + targetInSight);

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
        distanceBetweenTargetAndEnemy = targetDirection.magnitude;
        angleOfVision = angle;

        //bool ableToSeeTarget = angle < visionRange && (targetDirection.magnitude < maxVisionDistance && targetDirection.magnitude >= 0);


        return angle < visionRange && (targetDirection.magnitude < maxVisionDistance && targetDirection.magnitude >= 0);

    }

    public Transform getTargetObjectTransform()
    {
        return targetObject;
    }

	private void AlertOtherEnemiesOfTargetWithinVision()
    {

        if (targetInSight)
        {

            //foreach (Transform currentEnemyInRoom in this.transform.parent.transform)
            //{
            //    WasAlerted enemyAlerted = currentEnemyInRoom.gameObject.GetComponent<WasAlerted>();
            //    if (enemyAlerted != null && !currentEnemyInRoom.gameObject.Equals(this.gameObject))
            //    {
            //        //Debug.Log(this.gameObject.name + ": Alerting " + currentEnemyInRoom.gameObject.name + " with target position " + targetObject.position );
            //        enemyAlerted.AlertWithTargetLastKnownPosition(targetObject.position);
            //    }
            //}


            foreach (GameObject currentEnemyInScene in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                WasAlerted enemyAlerted = currentEnemyInScene.GetComponent<WasAlerted>();
                float distanceBetweenThisAndOtherEnemy = VectorMathUtil.CalculateDirectionBetweenTwoPositions(this.gameObject.transform.position, currentEnemyInScene.transform.position).magnitude;

                // If the retrieved enemy has the WasAlerted script AND is not this enemy AND is within the alert range...
                if (enemyAlerted != null && !currentEnemyInScene.Equals(this.gameObject) && distanceBetweenThisAndOtherEnemy <= maxAlertRange)
                {
                    // ... then, alert that enemy
                    //Debug.Log(this.gameObject.name + ": Alerting " + currentEnemyInRoom.gameObject.name + " with target position " + targetObject.position );
                    enemyAlerted.AlertWithTargetLastKnownPosition(targetObject.position);
                }
            }
        }

    }

    public SkinnedMeshRenderer GetMeshRenderer()
    {
        return smRenderer;
    }
}
