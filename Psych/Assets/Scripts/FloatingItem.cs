using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingItem : MonoBehaviour
{
    Vector3 start, end;
    float time;
    public float speed = 2;
    public float height;
    public float rotateAmount;
    // Start is called before the first frame update
    void Start()
    {
        start = transform.position;
        end = transform.position;
        end.y += height;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        transform.position = Vector3.Lerp(start, end, Mathf.Abs(Mathf.Sin(time*speed)));
        transform.rotation = Quaternion.Euler(0, rotateAmount * speed * time, 0);
    }
}
