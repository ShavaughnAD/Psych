using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWallSlide : MonoBehaviour
{
    [SerializeField]
    Camera myCamera = null;
    private Vector3 previousPlayerPosition;
    private Rigidbody myRigidbody;
    private RaycastHit hitBox;
    [SerializeField]
    private PlayerMovement playerMovementScript;
    [SerializeField]
    private Vector3 offset;
    private Vector3 targetLocation;
    //[SerializeField]
    //private float sphereCheckRadius = 2.0f;
    //public Vector3 checkBehind = Vector3.back;
    //public Vector3 checkRight = Vector3.right;
    //public Vector3 checkLeft = Vector3.left;
    //public Vector3 checkFront = Vector3.forward;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        if (myCamera == null)
        {
            myCamera = GetComponent<Camera>();
        }
        playerMovementScript = GameObject.FindObjectOfType<PlayerMovement>();
        if(playerMovementScript != null && myRigidbody != null)
        {
            offset = playerMovementScript.transform.position - myRigidbody.position;
        }
    }
    private void Update()
    {
        if (playerMovementScript != null)
        {
            previousPlayerPosition = playerMovementScript.transform.position;
            targetLocation = previousPlayerPosition;
        }
    }
    private void FixedUpdate()
    {
        playerMovementScript.transform.position = targetLocation;
    }
    private void OnCollisionEnter(Collision collision)
    {
    
        if(playerMovementScript != null )
        {
            Debug.Log("pre:" + playerMovementScript.gameObject.transform.position);
            targetLocation = myRigidbody.position + offset;
            Debug.Log("post:" + playerMovementScript.gameObject.transform.position);
        }
    }
    // Update is called once per frame
    /*
    void FixedUpdate()
    {
        if (myCamera != null)
        {
            if (Physics.SphereCast(myCamera.transform.position, sphereCheckRadius, checkBehind, out hitBox))
            {
                Debug.Log("hit " + hitBox.collider.gameObject.name);
                myCamera.transform.position = previousCameraPosition;
            }


            if (Physics.SphereCast(myCamera.transform.position, sphereCheckRadius, checkRight, out hitBox))
            {
                Debug.Log("hit " + hitBox.collider.gameObject.name);
                myCamera.transform.position = previousCameraPosition;
            }


            if (Physics.SphereCast(myCamera.transform.position, sphereCheckRadius, checkLeft, out hitBox))
            {
                Debug.Log("hit " + hitBox.collider.gameObject.name);
                myCamera.transform.position = previousCameraPosition;
            }


            if (Physics.SphereCast(myCamera.transform.position, sphereCheckRadius, checkFront, out hitBox))
            {
                Debug.Log("hit " + hitBox.collider.gameObject.name);
                myCamera.transform.position = previousCameraPosition;
            }

        }
    }
     */


}
