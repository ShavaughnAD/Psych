using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellCount : MonoBehaviour
{
    public int numberOfCells;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject Player = GameObject.Find("Player");
        Generator generator = Player.GetComponent<Generator>();
        numberOfCells = generator.cells;
        if(numberOfCells == 4)
        {
            Debug.Log("Generator Active");
        }
    }
}
