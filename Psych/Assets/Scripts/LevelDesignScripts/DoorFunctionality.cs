using UnityEngine;

public class DoorFunctionality : MonoBehaviour
{
    public Animator doorAnimator;
    public bool shouldOpen = false;

    void Awake()
    {
        if(doorAnimator == null)
        {
            doorAnimator = GetComponent<Animator>();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            shouldOpen = !shouldOpen;
            doorAnimator.SetBool("open", shouldOpen);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            shouldOpen = !shouldOpen;
            doorAnimator.SetBool("open", shouldOpen);
        }
    }
}