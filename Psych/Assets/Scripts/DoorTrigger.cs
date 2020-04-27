using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public DoorFunctionality leftDoor;
    public DoorFunctionality rightDoor;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(leftDoor != null)
            {
                leftDoor.enabled = true;
            }
            if(rightDoor != null)
            {
                rightDoor.enabled = true;
            }
        }
    }
}
