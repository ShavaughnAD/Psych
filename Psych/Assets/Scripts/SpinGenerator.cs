using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinGenerator : MonoBehaviour
{
    public float SpinSpeed = 1f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.Rotate(SpinSpeed, 0, 0, Space.World);
    }
}
