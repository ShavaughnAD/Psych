using UnityEngine;

public class Generator : MonoBehaviour
{
    public Collider powercell;
    public bool isHoldingBattery;
    public int cells;

    void Start()
    {
        cells = 0;
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
