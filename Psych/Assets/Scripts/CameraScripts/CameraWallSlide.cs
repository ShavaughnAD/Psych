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
    private Vector3 targetLocation;
    [SerializeField]
    private float sphereCheckRadius = 2.0f;
    [SerializeField]
    public Vector3 checkBehind = Vector3.back;

    public Vector3 previousCameraPosition;
    public Vector3 previousCameraRotation;
    public float lerpSpeed = 2.0f;
    public float maxDragDistance = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        if (myCamera == null)
        {
            myCamera = GetComponent<Camera>();
        }
        playerMovementScript = GameObject.FindObjectOfType<PlayerMovement>();

    }
  
    private void LateUpdate()
    {
        previousPlayerPosition = playerMovementScript.transform.position;
        previousCameraPosition = transform.localPosition;
        previousCameraRotation = transform.localEulerAngles;
    }



    // Update is called once per frame

    void FixedUpdate()
    {
        if (myCamera != null)
        {

            if (Physics.SphereCast(myCamera.transform.position, sphereCheckRadius, checkBehind, out hitBox))
            {
                transform.localEulerAngles = previousCameraRotation;
  
                Vector3 newloc = Vector3.zero;
                newloc = transform.localPosition + Vector3.forward * sphereCheckRadius;
                if (Vector3.Distance(newloc, playerMovementScript.transform.position) < maxDragDistance)
                {
                    transform.localPosition = Vector3.Slerp(transform.localPosition, newloc, lerpSpeed * playerMovementScript.speed * Time.fixedDeltaTime);

                }            
            }
            else if (Physics.SphereCast(myCamera.transform.position, sphereCheckRadius * 2, checkBehind, out hitBox))
            {
            }
            else if (Physics.OverlapSphere(transform.position, sphereCheckRadius).Length > 0)
            {
                Vector3 newloc = transform.localPosition + Vector3.forward * sphereCheckRadius;

                transform.localPosition = Vector3.Slerp(transform.localPosition, newloc, lerpSpeed * playerMovementScript.speed * Time.fixedDeltaTime);
            }
            else if (Physics.OverlapSphere(transform.position, sphereCheckRadius * 2).Length > 0)
            {
           //Keep this empty, don't want to move camera if its hugged against wall.
            }
            else
            {
                transform.localPosition = Vector3.Slerp(transform.localPosition, targetLocation, lerpSpeed * playerMovementScript.speed * Time.fixedDeltaTime);
            }

       


         

        }
    }



}
