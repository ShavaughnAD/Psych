using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorCollider : MonoBehaviour
{
    private Animator anim;
    public GameObject doorAnimation;

    // Start is called before the first frame update
    void Start()
    {
        anim = doorAnimation.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            anim.SetTrigger("playerOpenDoor");
        }
    }
}
