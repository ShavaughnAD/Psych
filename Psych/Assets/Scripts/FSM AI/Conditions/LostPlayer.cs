using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This condition checks if this object loses sight of the target for a certain amount of time
    //If SeesPlayer.cannot see player
        //If loseSightTimer <= 0
            //return true
        //else
            //Timer--
    //else
        //return false
    
public class LostPlayer : FsmCondition
{
    [SerializeField]
    private float maxWaitTimeToLookForTarget = 5f;
    private float waitTimeToLookForTarget;
    
    private PlayerVision lineOfSight;
     Animator Anim;
    private bool lostSightOfTarget = false;
    private WasAlerted wasAlerted;


    private void Awake() {
        lineOfSight = this.GetComponent<PlayerVision>();
        float waitTimeToLookForTarget = maxWaitTimeToLookForTarget;
    }
     void Start()
    {
        Anim = GetComponent<Animator>();
        wasAlerted = this.GetComponent<WasAlerted>();
    }

    private void Update() {
        this.lostSightOfTarget = LostSightOfTarget();
        if (wasAlerted.IsAlerted()) //If being alerted, continue to wait
        {
            ResetWaitTime();
        }
    }

    public bool LostSightOfTarget(){

        //If I can see the target, then I haven't lost sight of it.  
        if(lineOfSight.GetTargetInSight()){
            ResetWaitTime();
            return false;
        
        }else{//If I can't see the player...
            //...and I ran out of time to look for the target, 
            //  then I lost sight of the target

            if(waitTimeToLookForTarget <= 0){
                Debug.Log("Gave up on the target - v( ･_･)v ???");
                Anim.SetBool("CanAttack", false);
                return true;

            }else{
                //...and I still have time to look for the player
                //  then just subtract from the wait time
                waitTimeToLookForTarget--;
                //Debug.Log("Current Wait Time: " + waitTimeToLookForTarget * Time.deltaTime);

                return false;
            }
        }
       
            
    }

    private void ResetWaitTime(){
        waitTimeToLookForTarget = maxWaitTimeToLookForTarget;
    }
    
    public override bool IsSatisfied(FsmState curr, FsmState next){

        return lostSightOfTarget;
    }
}
