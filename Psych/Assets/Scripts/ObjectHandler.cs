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
    [SerializeField] float throwSpeed = 7f;
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

            Vector3 toTarget = targetPoint - objectRBinHand.position;

            float gSquared = Physics.gravity.sqrMagnitude;

            /*float b = throwSpeed * throwSpeed + Vector3.Dot(toTarget, Physics.gravity);    
            float discriminant = b * b - gSquared * toTarget.sqrMagnitude;

            
            if(discriminant < 0) {
                Debug.Log("Object not in range");
                return;
            }

            float discRoot = Mathf.Sqrt(discriminant);*/

            float T_lowEnergy = Mathf.Sqrt(Mathf.Sqrt(toTarget.sqrMagnitude * 4f/gSquared));
            float T = T_lowEnergy;
            Vector3 velocity = toTarget / T - Physics.gravity * T / 2f;

            objectRBinHand.AddForce(velocity, ForceMode.VelocityChange);
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
            isAttracting = false;
            return;
        }
        objectRBinHand.position = Vector3.MoveTowards(objectRB.position, targetPosition, speed);    
    }
}
