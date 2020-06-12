using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public Collider powercell;
    public bool isHoldingBattery;
    public int cells;

    // Start is called before the first frame update
    void Start()
    {
        cells = 0;
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
        if(other.tag == "Generator" && isHoldingBattery /*&& Input.GetKeyDown(KeyCode.Alpha5)*/)
        {          
            Debug.Log("Cell Added");
            cells++;
            
            
            /*ther.gameObject.SetActive(true);*/
            
        }
    }
}
