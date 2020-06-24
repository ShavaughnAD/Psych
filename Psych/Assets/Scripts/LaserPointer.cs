using UnityEngine;

public class LaserPointer : MonoBehaviour
{
    LineRenderer laserPointer;

    void Start()
    {
        laserPointer = GetComponent<LineRenderer>();
    }

    void LateUpdate()
    {
        laserPointer.SetPosition(0, transform.position);
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider)
            {
                laserPointer.SetPosition(1, hit.point);
            }
        }
        else
        {
            laserPointer.SetPosition(1, transform.position + (transform.forward * 5000));
        }
    }
}
