using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    public bool triggered = false;
    public ushort cp_id;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!triggered)
        {
            if(other.GetComponent<PlayerHealth>()!=null)
            {
                other.GetComponent<PlayerHealth>().SetRespawnPoint(this.transform);
                triggered = true;
                transform.Translate(0, 1.0f, 0);
            }
        }     
    }
}
