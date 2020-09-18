using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthBarTrigger : MonoBehaviour
{
    public GameObject bossHealthBar;
    private void Start()
    {
        bossHealthBar.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            bossHealthBar.SetActive(true);
        else
            return;
    }
    private void Update()
    {
        if (BossAI.bossAI.isDead)
        {
            bossHealthBar.SetActive(false);
        }
    }
}

