using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 0;
    public float currentHealth = 0;
    public float damageTaken = 0;
    public float healingRecieved = 0;
    public float damageReduction = 1; //Will be used to divide the damageAmount. Example, if the player gets hit with 5 damage, they will instead take 2.5

    public GameObject popup;
    public GameObject healingpopup;
    public bool fullHealth;
    bool invulnerable = false;
    public bool immune { get => invulnerable; set => invulnerable = value; }

    public HealthDelegate onHurt = new HealthDelegate();
    public HealthDelegate onDeath = new HealthDelegate();
    public HealthDelegate onHeal = new HealthDelegate();

    public virtual void Awake()
    {
        ResetHealth();
    }

    public virtual void TakeDamage(float damageAmount)
    {
        if (immune) return;
        if (currentHealth <= 0) return;
        currentHealth = Mathf.Clamp(currentHealth - Mathf.Round(damageAmount / damageReduction), 0, maxHealth);
        if (currentHealth == 0)
        {
            onDeath.CallEvent(0);
        }
        else
        {
            damageTaken = damageAmount;
            onHurt.CallEvent(currentHealth / maxHealth);
            fullHealth = false;            
        }
    }

    public virtual void ResetHealth()
    {
        currentHealth = maxHealth;
        fullHealth = true;
    }

    public void Healing(float healingAmount)
    {
        healingRecieved = healingAmount;
        //onHeal.CallEvent(0);
        currentHealth += healingAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        if(currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
            fullHealth = true;
            //if (healingpopup!=null)
            //{
            //    HealingFloatingText();
            //}
        }
    }

    public virtual void DamageFloatingText()
    {
        var go = Instantiate(popup, transform.position, Quaternion.identity, transform);
        go.GetComponent<TextMesh>().text = damageTaken.ToString();
        go.transform.rotation = Camera.main.transform.rotation;

    }

    public virtual void HealingFloatingText()
    {
        var go = Instantiate(healingpopup, transform.position, Quaternion.identity, transform);
        go.GetComponent<TextMesh>().text = healingRecieved.ToString();
        go.transform.rotation = Camera.main.transform.rotation;
    }
}