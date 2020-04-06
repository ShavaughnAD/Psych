using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : Health
{
    public Image healthBar;
    public Text healthText;
    public float maxPower = 50;
    public float currentPower = 0;
    public float healthRegenAmount = 1;

    public override void Awake()
    {
        base.Awake();
        popup = Resources.Load<GameObject>("Prefabs/HealingFloatingText");
        //onHurt.BindToEvent(Hurt);
        onHeal.BindToEvent(Heal);
        onDeath.BindToEvent(Death);
    }

    void Update()
    {
        healthBar.fillAmount = currentHealth / maxHealth;
        healthText.text = currentHealth.ToString("0") + " / " + maxHealth.ToString("0");
        HealthRegen();
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
