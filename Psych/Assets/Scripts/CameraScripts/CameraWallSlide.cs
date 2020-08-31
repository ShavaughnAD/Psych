using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWallSlide : MonoBehaviour
{
    [SerializeField]
    Camera myCamera = null;
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
    public float wallDetectionDistace = 10.0f;
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
                float distance = Vector3.Distance(newloc, playerMovementScript.transform.position);
                if (distance < wallDetectionDistace)
                {
                    transform.localPosition = Vector3.Lerp(transform.localPosition, newloc, lerpSpeed  * Time.fixedDeltaTime);
                   // Debug.Log("1");
                }

            }
            else if (Physics.SphereCast(myCamera.transform.position, sphereCheckRadius * 2, checkBehind, out hitBox))
            {
                //Keep this empty, don't want to move camera if its hugged against wall.

            }
            else if (Physics.OverlapSphere(transform.position, sphereCheckRadius).Length > 0)
            {
                Vector3 newloc = transform.localPosition + Vector3.forward * sphereCheckRadius;
                float distance = Vector3.Distance(newloc, playerMovementScript.transform.position);

                if (distance < wallDetectionDistace)
                {
                    transform.localPosition = Vector3.Lerp(transform.localPosition, newloc, lerpSpeed * Time.fixedDeltaTime);
                    //Debug.Log("3");
                }
            }
            else if (Physics.OverlapSphere(transform.position, sphereCheckRadius * 2).Length > 0)
            {
           //Keep this empty, don't want to move camera if its hugged against wall.
                    Debug.Log("4");
            }

           else if (Physics.SphereCast(playerMovementScript.transform.position, sphereCheckRadius, checkBehind, out hitBox))
            {
                transform.localEulerAngles = previousCameraRotation;

                Vector3 newloc = Vector3.zero;
                newloc = targetLocation + Vector3.forward * sphereCheckRadius;
                float distance = Vector3.Distance(newloc, playerMovementScript.transform.position);
                    Debug.Log("1");
                if (distance < wallDetectionDistace)
                {
                    transform.localPosition = Vector3.Lerp(transform.localPosition, newloc, lerpSpeed * Time.fixedDeltaTime);
                    Debug.Log("10");
                }

            }

            else if (Physics.OverlapSphere(playerMovementScript.transform.position, sphereCheckRadius).Length > 0)
            {
                Vector3 newloc = targetLocation + Vector3.forward * sphereCheckRadius;
                float distance = Vector3.Distance(newloc, playerMovementScript.transform.position);

                    Debug.Log("30");
                if (distance < wallDetectionDistace)
                {
                    transform.localPosition = Vector3.Lerp(transform.localPosition, newloc, lerpSpeed * Time.fixedDeltaTime);
                    Debug.Log("3");
                }
            }
            else if (Physics.OverlapSphere(playerMovementScript.transform.position, sphereCheckRadius * 2).Length > 0)
            {
                //Keep this empty, don't want to move camera if its hugged against wall.
                //  Debug.Log("4");
            }
            else
            {
                float distance = Vector3.Distance(transform.localPosition, targetLocation);
                transform.localPosition = Vector3.Lerp(transform.localPosition, targetLocation, lerpSpeed * Time.fixedDeltaTime);
            }




        }
    }



}
