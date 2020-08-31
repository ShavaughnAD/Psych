using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpinner : MonoBehaviour
{
    public GameObject[] paths;
    int current = 0;
    public float speed;
    float wPradius = 1;


    private void Update()
    {
        if (Vector3.Distance(paths[current].transform.position, transform.position) < wPradius)
        {
            current++;
            if (current >= paths.Length)
            {
                current = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, paths[current].transform.position, Time.deltaTime * speed);
    }
}
