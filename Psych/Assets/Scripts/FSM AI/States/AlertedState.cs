using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
 
// This state controls the behaviour of the enemy when it is Alerted of the player's location by an event
// Once alerted (the player's location has been identified
    // (the player's location has been sent to this enemy)
    // Make sure the player's location isn't null)
    // Navigate to the player location
    //If, at any point, the enemy sees the player
        //Transition to Attack State
    //If the Enemy arrives at the given player's position, AND the Enemy does not see the player, AND a certain amount of time has passed
        //Transition back to Patrol or Stationed State
public class AlertedState : FsmState
{

    private WasAlerted wasAlerted;
    private Vector3 targetLastKnownPosition;

    private NavMeshAgent navMeshAgent;

    private void Start()
    {
        this.navMeshAgent = this.GetComponent<NavMeshAgent>();
        this.wasAlerted = this.GetComponent<WasAlerted>();
        if(this.wasAlerted == null)
        {
            throw new System.Exception(this.name + ": WasAlerted.cs Component missing from this object");
        }
    }

    private void OnEnable()
    {
        SetTargetLastKnownPosition();
        //Debug.Log("I've been alerted - (O_O)!");
    }

    private void SetTargetLastKnownPosition()
    {
        this.targetLastKnownPosition = wasAlerted.GetTargetLastKnownPosition();
    }

    private void Update()
    {
        MoveToTargetLastKnownPosition();
    }

    private void MoveToTargetLastKnownPosition()
    {
        if (targetLastKnownPosition != Vector3.zero)
        {
            this.navMeshAgent.SetDestination(targetLastKnownPosition);
        }
    }
}
