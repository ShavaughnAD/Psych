using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    public int killScore;
    public bool isSlug = false;

    private IsDead isDeadCondition;

    AudioSource auSource;
    public AudioClip EnemyHurt;
    public override void Awake()
    {
        auSource = GetComponent<AudioSource>();
        base.Awake();
        popup = Resources.Load<GameObject>("Prefabs/UIAssets/DamageFloatingText");
        onHurt.BindToEvent(Hurt);
        onDeath.BindToEvent(Death);
        gameObject.layer = 10;
        isDeadCondition = this.GetComponent<IsDead>();
    }

    void Hurt(float param)
    {
        auSource.PlayOneShot(EnemyHurt);
        //DamageFloatingText();
    }

    void Death(float param)
    {
        //ScoreSystem.Instance.AddScore(killScore);
        if (isSlug)
        {
            isDeadCondition.SetHealthReachedZero(true);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
