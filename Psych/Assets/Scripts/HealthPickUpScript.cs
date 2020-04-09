using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUpScript : MonoBehaviour
{
    float bottomHeight;
    public float healAmount;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if (other.GetComponent<PlayerHealth>() != null)
        {
            if(!other.GetComponent<PlayerHealth>().fullHealth)
            {
                other.GetComponent<PlayerHealth>().Healing(healAmount);
                Destroy(this.gameObject);
            }
        }
    }
}
