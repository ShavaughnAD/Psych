using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : Health
{
    public Image healthBar;
    //public Text healthText;
    public Renderer[] orbs;
    public float maxPower = 50;
    public float currentPower = 0;
    public float healthRegenAmount = 1;
    public Vector3 respawnPoint;

    AudioSource auSource;
    public AudioClip PlayerHurt;
    public override void Awake()
    {
        auSource = GetComponent<AudioSource>();
        base.Awake();
        popup = Resources.Load<GameObject>("Resources/Prefabs/UIAssets/HealingFloatingText");
        //onHurt.BindToEvent(Hurt);
        //onHeal.BindToEvent(Heal);
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
        //healthText.text = currentHealth.ToString("0") + " / " + maxHealth.ToString("0");
        HealthRegen();
        foreach(Renderer rend in orbs)
        {
            if (currentHealth == maxHealth || currentHealth >= maxHealth * 0.51f)
            {
                rend.material.SetColor("_EmissionColor", Color.blue);
                healthBar.color = new Color(255, 255, 255);
                damageReduction = 1;
            }
            else if (currentHealth <= maxHealth * 0.50f && currentHealth >= maxHealth * 0.31f)
            {
                rend.material.SetColor("_EmissionColor", Color.yellow);
                healthBar.color = new Color(255, 255, 255);
                //healthBar.color = Color.yellow;
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

    public void GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void SetRespawnPoint(Transform transform)
    {
        respawnPoint = transform.position;
        Debug.Log("PLAYER HP : NEW CHECK POINT");
    }

    void Hurt(float param)
    {
        auSource.PlayOneShot(PlayerHurt);
    }

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
        if (GetComponentInChildren<Animator>().GetBool("isDead") == false)
        {
            GetComponentInChildren<Animator>().SetBool("isDead", true);
        }
    }

    void Respawn()
    {
        transform.position = respawnPoint;
        //if need rotation
        //transform.rotation = respawnPoint.rotation;
        GetComponentInChildren<Animator>().SetBool("isDead", false);
        currentHealth = maxHealth;
    }

    public void DeathAnim()
    {
        GameRestart();
    }
}
