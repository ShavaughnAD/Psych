using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public Collider powercell;
    public bool isHoldingBattery;

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
        if(other.gameObject.name == "Cell" && Input.GetKey(KeyCode.Alpha1))
        {
            Debug.Log("Player is holding cell");
            isHoldingBattery = true;
        }
        if(other.tag == "Cell" && isHoldingBattery)
        {
            other.gameObject.SetActive(true);

        }
    }
}
