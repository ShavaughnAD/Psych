using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : FsmState
{

    public GameObject[] arrayOfPatrolPoints;
    public float patrolSpeed = 5f;

    private GameObject currentPatrolPoint;
    private int currentPatrolPointIndex = 0;

    private NavMeshAgent enemyAgent;
    
    //On entry, immediately move to last patrol point (this is set to the first patrol point in the array on wake)
    //when a patrol point is reached, switch current patrol point to the next pp in line
    //When reaching the last patrol point, cycle to the beginning

    private void Awake() {

        enemyAgent = this.GetComponent<NavMeshAgent>();
        if(arrayOfPatrolPoints.Length > 0){

            currentPatrolPoint = arrayOfPatrolPoints[0];
        }
        else{
            //Alternatively, we can set a patrol to the position of the object
            arrayOfPatrolPoints = new GameObject[1];
            //Debug.Log(this.gameObject.name + ": Number of patrol points: " + arrayOfPatrolPoints.Length);

            GameObject defaultPatrolPointObject = new GameObject();
            defaultPatrolPointObject.transform.position = this.gameObject.transform.position;
            defaultPatrolPointObject.gameObject.name = "Default Patrol Point";
            arrayOfPatrolPoints[0] = defaultPatrolPointObject;
            currentPatrolPoint = arrayOfPatrolPoints[0];

            Debug.LogWarning(this.gameObject.name + ": No Patrol Points Set");
        }
    }
    private void OnTriggerEnter(Collider other) {

        SetNextPatrolPoint(other);
        Debug.Log(this.gameObject.name + ": Hit Patrol Point");

    }

    public void SetNextPatrolPoint(Collider other)
    {
        Debug.Log(this.gameObject.name + ": Current Patrol Point Name: " + currentPatrolPoint);
        if (other.gameObject == currentPatrolPoint)
        {
            currentPatrolPointIndex = (currentPatrolPointIndex + 1) % arrayOfPatrolPoints.Length;
            currentPatrolPoint = arrayOfPatrolPoints[currentPatrolPointIndex];

            
            Debug.Log(this.gameObject.name + ": Current Patrol Point Name: " + currentPatrolPoint);

            //If hits patrol point, it could be resetting back to the patrol state
        }
    }
        
    private void Update(){
        MoveToPatrolPoints();     
    }

    private void MoveToPatrolPoints(){
        // patrollerGameObject.transform.position = Vector3.MoveTowards(this.transform.position, currentPatrolPoint.transform.position, patrolSpeed * Time.deltaTime);
        //     this.transform.LookAt(currentPatrolPoint.transform);
        if(currentPatrolPoint != null)
        {
            enemyAgent.SetDestination(currentPatrolPoint.transform.position);
            enemyAgent.speed = patrolSpeed;
        }
        else
        {
            // Debug.LogWarning(this.gameObject.name + ": PatrolPoint at index " + System.Array.IndexOf(arrayOfPatrolPoints, currentPatrolPoint) + " is null");
            Debug.LogWarning(this.gameObject.name + "Current patrolPoint = " + currentPatrolPoint);
        }
    }

    
}
