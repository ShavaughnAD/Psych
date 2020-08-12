using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public GameObject[] bulbs;

    public bool isFlickering = false;
    public float timeDelay;

    void Update()
    {
        if (isFlickering == false)
        {
            StartCoroutine(FlickerControl());
        }
    }

    IEnumerator FlickerControl()
    {
        isFlickering = true;
        this.gameObject.GetComponent<Light>().enabled = false;
        foreach (GameObject light in bulbs )
        {
            light.GetComponent<MeshRenderer>().enabled = false;
        }
        timeDelay = Random.Range(0.01f, 0.2f);
        yield return new WaitForSeconds(timeDelay);
        this.gameObject.GetComponent<Light>().enabled = true;
        foreach (GameObject light in bulbs)
        {
            light.GetComponent<MeshRenderer>().enabled = true;
        }
        timeDelay = Random.Range(0.01f, 0.2f);
        yield return new WaitForSeconds(timeDelay);
        isFlickering = false;
    }
}
