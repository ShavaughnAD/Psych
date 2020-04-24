using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorFunctionality : MonoBehaviour
{
    public GameObject elevatorObj;
    public Transform startingPoint;
    public Transform endPoint;
    public bool goingUp = false;
    public bool startMoving = false;

    public float speed = 1;

    //Time when the movement started
    float startTime;
    //Total distance between starting point & end point
    float journeyLength;

    void Start()
    {
        startTime = Time.time;
        journeyLength = Vector3.Distance(startingPoint.position, endPoint.position);
    }

    void Update()
    {
        if (startMoving)
        {
            float distCovered = (Time.time - startTime) * speed;
            float fractionOfJourney = distCovered / journeyLength;
            elevatorObj.transform.position = Vector3.Lerp(startingPoint.position, endPoint.position, fractionOfJourney);
        }
    }
}
