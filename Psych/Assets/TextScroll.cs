using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextScroll : MonoBehaviour
{
    public float speed;
    public Vector3 Dir;
    //public bool IVisible;
    void FixedUpdate()
    {
        transform.Translate(Dir * Time.deltaTime * speed);
        //if(GetComponent<Renderer>().isVisible)
        //{
        //    IVisible = true;
        //}
        //else
        //{
        //    IVisible = false;
        //}
    }
}
