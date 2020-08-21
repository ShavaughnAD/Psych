using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostWeapon : FsmCondition
{

    private AttackState attackState;

    public override bool IsSatisfied(FsmState curr, FsmState next)
    {
        return this.attackState.currentWeapon == null;
    }

    private void Start()
    {
        this.attackState = this.GetComponent<AttackState>();
    }

    private void Update()
    {
        //Debug.Log("Lost Weapon?: " + this.attackState.currentWeapon == null);
    }






}
