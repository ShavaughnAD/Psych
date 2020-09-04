using UnityEngine;

public class DoorFunctionality : MonoBehaviour
{
    public Animator doorAnimator;
    public bool shouldOpen = false;
    public bool Powered;
    public bool BossDoor;
    public GameObject Boss;

    AudioSource auSource;
    public AudioClip MechanicalDoor;
    void Awake()
    {
        auSource = GetComponent<AudioSource>();
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
            shouldOpen = !shouldOpen;
            doorAnimator.SetBool("open", shouldOpen);
            auSource.PlayOneShot(MechanicalDoor);
            GetComponent<BoxCollider>().enabled = false;
            GetComponentInChildren<BoxCollider>().enabled = false;
            if (BossDoor)
            {
                Boss.GetComponent<BossAI>().target = other.transform;
            }
        }
        
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && Powered || other.tag == "Enemy" && Powered)
        {
            GetComponent<BoxCollider>().enabled = true;
            GetComponentInChildren<BoxCollider>().enabled = true;
        }
        if (BossDoor == true)
        {
            shouldOpen = false;
            doorAnimator.SetBool("open", shouldOpen);
            Powered = false;
        }       
    }
}