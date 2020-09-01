using UnityEngine;

public class DoorFunctionality : MonoBehaviour
{
    public Animator doorAnimator;
    public bool shouldOpen = false;
    public bool Powered;
    public bool BossDoor;

    AudioSource auSource;
    public AudioClip MechanicalDoor;
    void Awake()
    {
        if(doorAnimator == null)
        {
            doorAnimator = GetComponent<Animator>();
        }
    }
    private void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && Powered || other.tag == "Enemy" && Powered)
        {           
            auSource.PlayOneShot(MechanicalDoor);
            shouldOpen = !shouldOpen;
            doorAnimator.SetBool("open", shouldOpen);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (BossDoor == true)
        {
            shouldOpen = false;
            doorAnimator.SetBool("open", shouldOpen);
            Powered = false;
        }
    }
}