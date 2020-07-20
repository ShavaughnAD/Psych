using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorFunctionality : MonoBehaviour
{
    public GameObject doorObj;
    Transform startingPoint;
    public Transform endPoint;
    public float speed;

    float startTime;
    float journeyLength;

    void Start()
    {
        startingPoint = doorObj.transform;
        startTime = Time.time;
        journeyLength = Vector3.Distance(startingPoint.position, endPoint.position);
        enabled = false;
    }

    void Update()
    {
        float distCovered = (Time.time - startTime) * speed;
        float fractionOfJourney = distCovered / journeyLength;
        doorObj.transform.position = Vector3.Lerp(startingPoint.position, endPoint.position, fractionOfJourney);
    }
}