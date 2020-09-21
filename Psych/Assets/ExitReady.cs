using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitReady : MonoBehaviour
{
    public GameObject exitText;
    private void Start()
    {
        exitText.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && BossAI.bossAI.isDead)
        {
            DD();
        }
           
        
        else
            return;
    }
   public void DD()
    {
        exitText.SetActive(true);
        Invoke("Cancel", 6);
    }

    void Cancel()
    {
        exitText.SetActive(false);
        Destroy(this);
    }
}
