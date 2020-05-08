using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public GameObject destination;
    public bool ready = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other + " : " + other.tag);

        if(other.tag == "Player" && ready)
        {
            if (destination.GetComponent<Teleporter>() != null)
            {
                destination.GetComponent<Teleporter>().ready = false;
            }
            other.gameObject.transform.position = new Vector3(destination.transform.position.x,other.gameObject.transform.position.y,destination.transform.position.z);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            ready = true;
        }
    }
}
