using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellCount : MonoBehaviour
{
    public int numberOfCells;

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
