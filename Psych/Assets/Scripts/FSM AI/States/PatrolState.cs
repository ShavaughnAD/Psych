using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : MonoBehaviour
{

    public GameObject[] arrayOfPatrolPoints;
    public GameObject patrollerGameObject;
    public float patrolSpeed = 5f;

    private GameObject currentPatrolPoint;
    private int currentPatrolPointIndex = 0;

    
    //On entry, immediately move to last patrol point (this is set to the first patrol point in the array on wake)
    //when a patrol point is reached, switch current patrol point to the next pp in line
    //When reaching the last patrol point, cycle to the beginning

    private void Awake() {

        if(arrayOfPatrolPoints.Length > 0){

        currentPatrolPoint = arrayOfPatrolPoints[0];
        }
        else{
            //Alternatively, we can set a patrol to the position of the object
            Debug.LogError("No Patrol Points Set");;;
        }
        
    }

    private void OnTriggerEnter(Collider other) {

        if(other.gameObject == currentPatrolPoint){
            
            currentPatrolPointIndex = (currentPatrolPointIndex+ 1) % arrayOfPatrolPoints.Length;
            currentPatrolPoint = arrayOfPatrolPoints[currentPatrolPointIndex];
           // Debug.Log("Current Patrol Point Index: " + currentPatrolPointIndex);
        }

    }
        
    private void Update(){
        patrollerGameObject.transform.position = Vector3.Lerp(patrollerGameObject.transform.position, currentPatrolPoint.transform.position, patrolSpeed * Time.deltaTime);


    }
}
