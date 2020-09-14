using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    public int killScore;
    public bool isSlug = false;

    private IsDead isDeadCondition;

    AudioSource auSource;

    public AudioClip[] audioArray;
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
        HurtSound();
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

    void HurtSound()
    {
        AudioClip temp = audioArray[Random.Range(0, audioArray.Length)];
        auSource.PlayOneShot(temp);
    }
}
