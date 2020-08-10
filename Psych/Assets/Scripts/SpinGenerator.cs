using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinGenerator : MonoBehaviour
{
    public float X = 0f;
    public float Y = 0f;
    public float Z = 0f;
    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.Rotate(X, Y, Z, Space.World);
    }
}
