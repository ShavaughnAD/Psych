using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsDead : FsmCondition
{

    private bool healthReachedZero = false;


    public void SetHealthReachedZero(bool healthReachedZero)
    {
        this.healthReachedZero = healthReachedZero;
    }

    public override bool IsSatisfied(FsmState curr, FsmState next)
    {
        return this.healthReachedZero;
    }
}
