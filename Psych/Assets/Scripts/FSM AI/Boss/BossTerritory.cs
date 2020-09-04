using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTerritory : MonoBehaviour
{

    public GameObject Boss;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Boss.GetComponent<BossAI>().target = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Boss.GetComponent<BossAI>().target = null;
    }
}
