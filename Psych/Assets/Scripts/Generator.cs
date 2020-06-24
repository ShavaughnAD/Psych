using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    
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
        GameObject Player = GameObject.Find("Player");
        PlayerAim playerAim = Player.GetComponent<PlayerAim>();
        if(playerAim.objectRBinHand != null)
        {
            GameObject Selected = GameObject.Find(playerAim.objectRBinHand.gameObject.name);
            IsACell isACell = Selected.GetComponent<IsACell>();
            if (isACell != null && isACell.isHoldingACell == true)
            {
                Debug.Log("Player is holding cell");
                isHoldingBattery = true;
            }
            /*if ()
             {
                 Debug.Log("Cell Added");
                 cells++;


                 ther.gameObject.SetActive(true);

             }*/
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        
    }
}
