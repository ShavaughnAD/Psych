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
    private Vector3 zoomLocation;
    [SerializeField]
    private float sphereCheckRadius = 2.0f;
    [SerializeField]
    public Vector3 checkBehind = Vector3.back;

    public LayerMask layerMaskDetect = (~(1 << 8) | ~(1 << 9) | ~(1 << 11));//layermasks apparrently work using bits, its a long story but basically this checks everything not on the player layer.
   // public int layerMaskDetect2 = ~((1 << 8) | (1<<9) | (1 << 11));
    private Vector3 previousCameraRotation;
    public float lerpSpeed = 2.0f;
    public float wallDetectionDistace = 10.0f;
    Collider[] detections = new Collider[1024];

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
        previousCameraRotation = transform.localEulerAngles;
    }



    // Update is called once per frame

    void FixedUpdate()
    {
        if (myCamera != null)
        {


            if (Physics.SphereCast(playerMovementScript.transform.position, sphereCheckRadius * 0.5f, checkBehind, out hitBox, sphereCheckRadius * 0.5f, layerMaskDetect))
            {


                transform.localPosition = Vector3.Lerp(transform.localPosition, zoomLocation, lerpSpeed * Time.fixedDeltaTime);

            }
            else if (Physics.SphereCast(playerMovementScript.transform.position, sphereCheckRadius, checkBehind, out hitBox, sphereCheckRadius, layerMaskDetect))
            {

                transform.localPosition = Vector3.Lerp(transform.localPosition, zoomLocation, lerpSpeed * 1.5f * Time.fixedDeltaTime);


              

            }
            else if (Physics.SphereCast(playerMovementScript.transform.position, sphereCheckRadius * 2, checkBehind, out hitBox, sphereCheckRadius * 2, layerMaskDetect))
            {


             

            }
            else if ((Physics.OverlapSphereNonAlloc(transform.position, sphereCheckRadius * 0.5f, detections, layerMaskDetect)) > 0)
            {

                transform.localPosition = Vector3.Lerp(transform.localPosition, zoomLocation, lerpSpeed * Time.fixedDeltaTime);

        
            }
            else if ((Physics.OverlapSphereNonAlloc(transform.position, sphereCheckRadius,detections, layerMaskDetect)) > 0)
            {


                transform.localPosition = Vector3.Lerp(transform.localPosition, zoomLocation, lerpSpeed * Time.fixedDeltaTime);

            }
            else if ((Physics.OverlapSphereNonAlloc(transform.position, sphereCheckRadius * 1.5f,detections, layerMaskDetect)) > 0)
            {

             

                transform.localPosition = Vector3.Lerp(transform.localPosition, zoomLocation, lerpSpeed * Time.fixedDeltaTime);

            }
            else if (Physics.OverlapSphereNonAlloc(transform.position, sphereCheckRadius * 2,detections, layerMaskDetect) > 0)
            {



          
            }

            else if (Physics.OverlapSphereNonAlloc(transform.position, sphereCheckRadius * 2.5f,detections, layerMaskDetect) > 0)
            {



            }
            else if ( Physics.OverlapSphereNonAlloc(transform.position, sphereCheckRadius * 3,detections, layerMaskDetect) > 0)
            {

       

            }
            else
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, targetLocation, lerpSpeed * Time.fixedDeltaTime);

            }



        }
    }



}
