using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitlePage : MonoBehaviour
{

    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject titleMenu;

    // Start is called before the first frame update
    void Start()
    {
        mainMenu.SetActive(false);
        titleMenu.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(titleMenu.activeSelf)
        {
            if(Input.anyKey)
            {
                titleMenu.SetActive(false);
                mainMenu.SetActive(true);
            }

        }
    }
}
