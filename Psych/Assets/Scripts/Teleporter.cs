using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public GameObject destination;
    public bool ready = true;
    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other + " : " + other.tag);

        if(other.tag == "Player" && ready)
        {
            if (destination.GetComponent<Teleporter>() != null)
            {
                destination.GetComponent<Teleporter>().ready = false;
            }
            other.GetComponent<PlayerMovement>().enabled = false;
            other.gameObject.transform.position = new Vector3(destination.transform.position.x,other.gameObject.transform.position.y,destination.transform.position.z);
            Invoke("EnablePlayerMovement", .02f);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            ready = true;
        }
    }

    void EnablePlayerMovement()
    {
        player.GetComponent<PlayerMovement>().enabled = true;
    }
}
