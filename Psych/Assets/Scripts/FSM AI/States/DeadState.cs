using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : FsmState
{

    public override void OnStateEnter()
    {
        Debug.Log(this.gameObject.name + ": AAH! I'm going down!");
        GetComponent<Animator>().SetBool("isDead", true);
        GetComponent<AttackState>().currentWeapon.transform.parent = null;
        GetComponent<AttackState>().currentWeapon.GetComponent<Rigidbody>().isKinematic = false;
        Destroy(gameObject, 2);
    }

}
