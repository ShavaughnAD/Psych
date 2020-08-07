using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    public int killScore;
    public bool isSlug = false;
    public override void Awake()
    {
        base.Awake();
        popup = Resources.Load<GameObject>("Prefabs/UIAssets/DamageFloatingText");
        onHurt.BindToEvent(Hurt);
        onDeath.BindToEvent(Death);
        gameObject.layer = 10;
    }

    void Hurt(float param)
    {
        DamageFloatingText();
    }

    void Death(float param)
    {
        //ScoreSystem.Instance.AddScore(killScore);
        if (isSlug)
        {
            GetComponent<Animator>().SetBool("isDead", true);
            GetComponent<AttackState>().currentWeapon.transform.parent = null;
            GetComponent<AttackState>().currentWeapon.GetComponent<Rigidbody>().isKinematic = false;
            Destroy(gameObject, 2);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
