using UnityEngine;

public class ElevatorTrigger : MonoBehaviour
{
    public ElevatorFunctionality elevatorFunctionality;

    void Awake()
    {
        if(elevatorFunctionality == null)
        {
            elevatorFunctionality = GetComponentInParent<ElevatorFunctionality>();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.LogError(other.tag);
        if(other.tag == "Player")
        {
            Debug.LogError("Player stepped here dog");
            other.transform.parent = transform;
            elevatorFunctionality.enabled = true;
            elevatorFunctionality.startMoving = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            other.transform.parent = null;
        }
    }
  
}