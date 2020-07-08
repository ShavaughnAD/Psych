﻿using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health
{
    public Image healthBar;
    public Text healthText;
    public Renderer[] orbs;
    public float maxPower = 50;
    public float currentPower = 0;
    public float healthRegenAmount = 1;
    public Vector3 respawnPoint;

    public override void Awake()
    {
        base.Awake();
        popup = Resources.Load<GameObject>("Resources/Prefabs/UIAssets/HealingFloatingText");
        //onHurt.BindToEvent(Hurt);
        onHeal.BindToEvent(Heal);
        onDeath.BindToEvent(Death);

//#if DEBUG
//        //DEBUG
//        DebugManager dm = GameObject.Find("DebugManager").GetComponent<DebugManager>();
//        if(dm)
//        {
//            dm.eve_playerInvul += DM_playerInvul;

//        }
//#endif
    }
    /// Called when Debug Manager's playerInvul event invoke
    /// 
    private void DM_playerInvul(object sender, bool e)
    {
        immune = e;
    }

    void Update()
    {
        healthBar.fillAmount = currentHealth / maxHealth;
        healthText.text = currentHealth.ToString("0") + " / " + maxHealth.ToString("0");
        HealthRegen();
        foreach(Renderer rend in orbs)
        {
            if (currentHealth == maxHealth || currentHealth >= maxHealth * 0.51f)
            {
                rend.material.SetColor("_EmissionColor", Color.blue);
                healthBar.color = Color.blue;
                damageReduction = 1;
            }
            else if (currentHealth <= maxHealth * 0.50f && currentHealth >= maxHealth * 0.31f)
            {
                rend.material.SetColor("_EmissionColor", Color.yellow);
                healthBar.color = Color.yellow;
                damageReduction = 1;
            }
            else if (currentHealth <= maxHealth * 0.30f)
            {
                rend.material.SetColor("_EmissionColor", Color.red);
                healthBar.color = Color.red;
                //We can reduce incoming damage here to give the player a fighting chance if need be
                damageReduction = 2;
            }
        }
    }

    void Start()
    {
        GameRestart();
    }

    void GameRestart()
    {
        respawnPoint = transform.position;
    }
    public void SetRespawnPoint(Transform transform)
    {
        respawnPoint = transform.position;
        Debug.Log("PLAYER HP : NEW CHECK POINT");
    }

    //void Hurt(float param)
    //{

    //}

    void Heal(float param)
    {
        HealingFloatingText();
    }

    void HealthRegen()
    {
        currentHealth += healthRegenAmount * Time.deltaTime;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    void Death(float param)
    {
        Respawn();
    }

    void Respawn()
    {
        transform.position = respawnPoint;
        //if need rotation
        //transform.rotation = respawnPoint.rotation;
        currentHealth = maxHealth;
    }
}
