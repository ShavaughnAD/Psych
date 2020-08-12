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
    public List<GunRack> GRack;
    
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
            Debug.LogError("No Patrol Points Set");;;
        }          
    }
    private void Start()
    {
        foreach (GunRack GAdd in FindObjectsOfType<GunRack>())
        {
            if (GRack.Contains(GAdd))
            {
                Debug.LogError("Cant Add");
                //Blank
            }
            else
            {
                GRack.Add(GAdd);              
            }
        }
    }
    private void OnTriggerEnter(Collider other) {

        SetNextPatrolPoint(other);

    }

    public void SetNextPatrolPoint(Collider other)
    {
        if (other.gameObject == currentPatrolPoint)
        {
            currentPatrolPointIndex = (currentPatrolPointIndex + 1) % arrayOfPatrolPoints.Length;
            currentPatrolPoint = arrayOfPatrolPoints[currentPatrolPointIndex];
            // Debug.Log("Current Patrol Point Index: " + currentPatrolPointIndex);

            //If hits patrol point, it could be resetting back to the patrol state
        }
    }
        
    private void Update(){
        MoveToPatrolPoints();     
    }

    private void MoveToPatrolPoints(){
        // patrollerGameObject.transform.position = Vector3.MoveTowards(this.transform.position, currentPatrolPoint.transform.position, patrolSpeed * Time.deltaTime);
        //     this.transform.LookAt(currentPatrolPoint.transform);

        enemyAgent.SetDestination(currentPatrolPoint.transform.position);
        enemyAgent.speed = patrolSpeed;
    }

    public void RunToGunRack()
    {
        //Find Nearest Gun Rack and run 
        for (int i = 0; i < GRack.Count; i++)
        {
            foreach (Transform G_Rack in GRack[i].gameObject.transform)
            {
                float Dist = Mathf.Infinity;
                Vector3 GunRackPos = G_Rack.position;
                float GunRa = GunRackPos.sqrMagnitude;
                if(GunRa < Dist)
                {
                    currentPatrolPoint = GRack[i].gameObject;
                    //Ran Awayyyyyyyyyyy!!!!

                    enemyAgent.SetDestination(currentPatrolPoint.transform.position);
                    enemyAgent.speed = patrolSpeed;
                }
            }
        }       
    }
}
