using UnityEngine;

public class Generator : MonoBehaviour
{
    #region Variables

    public int cells;
    public bool startFull = false;
    public GameObject[] cellInGenerator;

    public GameObject  doorToOpen;
    public GameObject lights;
    GameObject cell;
    PlayerAim playerAim;

    #endregion

    void Start()
    {
        playerAim = FindObjectOfType<PlayerAim>();
        if (startFull == true)
        {
            cells = 4;
            foreach (GameObject _cell in cellInGenerator)
            {
                _cell.SetActive(true);
            }
        }
        else
        {
            //cells = 0;
            foreach (GameObject _cell in cellInGenerator)
            {
                _cell.SetActive(false);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IsACell>() && cells < 4)
        {
            cell = other.gameObject;
            if(cells <= 4)
            {
                GeneratorState();
            }
        }
    }

    void GeneratorState()
    {
        cells++;
        switch (cells)
         {
            case 1:
                cellInGenerator[0].SetActive(true);
                break;

            case 2:
                cellInGenerator[1].SetActive(true);
                break;

            case 3:
                cellInGenerator[2].SetActive(true);
                break;

            case 4:
                if(lights != null)
                {
                    lights.SetActive(true);
                }
                if(doorToOpen != null)
                {
                    doorToOpen.GetComponent<DoorFunctionality>().Powered = true;
                }
                cellInGenerator[3].SetActive(true);
                break;
        }
        playerAim.EmptyHand();
        cell.SetActive(false);
    }
}
