using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthTest : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;

    public GameObject bossHealthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetBossMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }

        if (currentHealth <= 0)
        {
            bossHealthBar.SetActive(false);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetBossHealth(currentHealth);
    }
}
