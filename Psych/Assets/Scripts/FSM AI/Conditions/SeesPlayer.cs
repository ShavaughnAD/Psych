using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeesPlayer : FsmCondition
{

    [SerializeField]
    private bool canSeeTarget = false;
    private PlayerVision playerVision;

    private void Start()
    {
        playerVision = this.GetComponent<PlayerVision>();

        if(playerVision == null)
        {
            Debug.LogError("Error: Missing Player Vision script on " + this.gameObject.name + " object");
        }
    }

    private void Update()
    {
        canSeeTarget = playerVision.GetTargetInSight();
    }


    public override bool IsSatisfied(FsmState curr, FsmState next){

        return canSeeTarget;
    }

}
