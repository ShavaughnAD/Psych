using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class PlayerHealth : Health
{
    public Image healthBar;
    public Text healthText;
    public float maxPower = 50;
    public float currentPower = 0;
    public float healthRegenAmount = 1;
    public Vector3 respawnPoint;

    public override void Awake()
    {
        base.Awake();
        popup = Resources.Load<GameObject>("Prefabs/HealingFloatingText");
        //onHurt.BindToEvent(Hurt);
        onHeal.BindToEvent(Heal);
        onDeath.BindToEvent(Death);

#if DEBUG
        //DEBUG
        DebugManager dm = GameObject.Find("DebugManager").GetComponent<DebugManager>();
        if(dm)
        {
            dm.eve_playerInvul += DM_playerInvul;

        }
#endif
    }
    /// Called when Debug Manager's playerInvul event invoke
    private void DM_playerInvul(object sender, bool e)
    {
        immune = e;
    }

    void Update()
    {
        healthBar.fillAmount = currentHealth / maxHealth;
        healthText.text = currentHealth.ToString("0") + " / " + maxHealth.ToString("0");
        HealthRegen();
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
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void Respawn()
    {
        transform.position = respawnPoint;
        //if need rotation
        //transform.rotation = respawnPoint.rotation;
        currentHealth = maxHealth;
    }
}
