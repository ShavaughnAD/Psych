using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDestroy : MonoBehaviour
{
    void Update()
    {
        if (BossAI.bossAI.isDead)
        {
            Destroy(gameObject);
        }
    }
}