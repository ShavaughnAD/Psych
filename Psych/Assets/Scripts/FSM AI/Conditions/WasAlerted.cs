using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WasAlerted : FsmCondition

{
    [SerializeField]
    private float alertedDuration = 1000f;

    private Vector3 targetLastKnownPosition;
    private bool isAlerted = false;
    private LostPlayer targetSearchTimer;
    private float alertedDurationTimer;

    private void Start()
    {
        targetSearchTimer = this.GetComponent<LostPlayer>();
        if(targetSearchTimer == null)
        {
            throw new Exception(this.name + ": LostPlayer.cs Component not attached to this game object");
        }
        ResetAlertedDurationTimer();
    }

    private void Update()
    {
        CheckIfAlertedTimerIsDone();
        CountDownAlertedDurationTimer();
    }


    public override bool IsSatisfied(FsmState curr, FsmState next)
    {
        return isAlerted;
    }

    public Vector3 GetTargetLastKnownPosition()
    {
        return targetLastKnownPosition;
    }

    public void AlertWithTargetLastKnownPosition(Vector3 targetLastKnownPosition)
    {
        this.targetLastKnownPosition = targetLastKnownPosition;
        if(targetLastKnownPosition != Vector3.zero)
        {
            ResetAlertedDurationTimer();
            isAlerted = true;
        }
        else
        {
            Debug.LogWarning(this.name + " received 'null' for targetLastKnownPosition");
        }
    }

    private void CheckIfAlertedTimerIsDone()
    {
        if (alertedDurationTimer <= 0)
        {
            isAlerted = false;
        }
    }

    private void ResetAlertedDurationTimer()
    {

        alertedDurationTimer = alertedDuration;
    }

    private void CountDownAlertedDurationTimer()
    {
        alertedDurationTimer--;
    }
}
