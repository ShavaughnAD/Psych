using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RB_Controller : MonoBehaviour
{
    public Rigidbody rb;

    public float sprint = 1.5f;
    public float speed = 5f;
    public float side, forward;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        forward = Input.GetAxis("Vertical") * speed;
        side = Input.GetAxis("Horizontal") * speed;

        

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            /*Debug.Log("Sprinting");*/
            rb.velocity = new Vector3(side, rb.velocity.y, forward * sprint);
        }
        else
        {
            rb.velocity = new Vector3(side, rb.velocity.y, forward);
        }
    }
}
