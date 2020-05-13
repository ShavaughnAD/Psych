using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHandler : MonoBehaviour
{

    GameObject selectionManager;
    GameObject attractTarget;
    Vector3 targetPosition; 
    Vector3 objectRBPosition;
    Rigidbody objectRB = null;
    Rigidbody objectRBinHand = null;
    [SerializeField] float attractSpeed = 7f;
    [SerializeField] float throwSpeed = 30f;
    [SerializeField] bool isCarryingObject = false;
    [SerializeField] bool isAttracting = false;

    private void Start()
    {
        selectionManager = GameObject.Find("Selection Manager");
        attractTarget = GameObject.Find("Target Left");
    }

    private void Update()
    {
        if(isAttracting)
            MoveToTarget();
    }

    public void ObjectPickUP()
    {
        objectRB = selectionManager.GetComponent<SelectionManager>().selectedObjectRB;
        if (objectRB != null && isCarryingObject != true)
        {
            
            objectRBinHand = objectRB;
            objectRBinHand.isKinematic = true;
            isCarryingObject = true;
            isAttracting = true;
        }
    }

    public void ThrowObject()
    {
        Vector3 targetPoint = selectionManager.GetComponent<SelectionManager>().selectedObjectRB.transform.position;
        if (isCarryingObject == true)
        {
            isCarryingObject = false;
            objectRBinHand.transform.parent = null;
            objectRBinHand.isKinematic = false;
            objectRBinHand.tag = ("Selectable");

            Vector3 velocity = SetThrowVelocity(objectRBinHand, targetPoint, throwSpeed);
            if(velocity != Vector3.zero)
            {
                objectRBinHand.AddForce(velocity, ForceMode.VelocityChange);
            }
        }
    }

    void MoveToTarget()
    {
        objectRBPosition = objectRBinHand.position;
        targetPosition = attractTarget.transform.position;
        float speed = attractSpeed * Time.deltaTime;
        if(objectRBPosition == targetPosition)
        {
            objectRBinHand.transform.parent = GameObject.FindGameObjectWithTag("Player").transform;
            objectRBinHand.tag = "NotSelectable";
            isAttracting = false;
            return;
        }
        objectRBinHand.position = Vector3.MoveTowards(objectRB.position, targetPosition, speed);    
    }

    Vector3 SetThrowVelocity(Rigidbody rigidbody, Vector3 target, float force, float arch = 0.2f)
    {
        Mathf.Clamp(arch, 0, 1);
        var origin = rigidbody.position;
        float x = target.x - origin.x;
        float y = target.y - origin.y;
        float z = target.z - origin.z;
        float gravity = -Physics.gravity.y;
        float b = force * force - y * gravity;
        float discriminant = b * b - gravity * gravity * (x * x + y * y + z * z);
        if (discriminant < 0)
        {
            Debug.Log("Object not in range");
            return Vector3.zero;
        }
        float discriminantSquareRoot = Mathf.Sqrt(discriminant);
        float minTime = Mathf.Sqrt((b - discriminantSquareRoot) * 2) / Mathf.Abs(gravity);
        float maxTime = Mathf.Sqrt((b + discriminantSquareRoot) * 2) / Mathf.Abs(gravity);
        float time = (maxTime - minTime) * arch + minTime;
        float vx = x / time;
        float vy = y / time + time * gravity / 2;
        float vz = z / time;
        Vector3 velocity = new Vector3(vx, vy, vz);
        return velocity;
    }
}
