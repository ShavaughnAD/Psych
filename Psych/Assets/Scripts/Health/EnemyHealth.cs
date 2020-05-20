using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    public int killScore;
    public override void Awake()
    {
        base.Awake();
        popup = Resources.Load<GameObject>("Prefabs/UIAssets/DamageFloatingText");
        onHurt.BindToEvent(Hurt);
        onDeath.BindToEvent(Death);
    }

    void Hurt(float param)
    {
        DamageFloatingText();
    }

    void Death(float param)
    {
        //ScoreSystem.Instance.AddScore(killScore);
        Destroy(gameObject);
    }
}
