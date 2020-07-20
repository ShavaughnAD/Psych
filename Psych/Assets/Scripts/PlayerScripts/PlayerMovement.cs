﻿using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    CharacterController characterController;
    public Light flashlight;
    public Animator Anim;

    public float speed = 6;
    public float gravity = 20;

    Vector3 moveDir = Vector3.zero;

    public Camera playerCam;
    public float camXSensitivity = 1.5f;
    public float camYSensitivity = 1;
    public float sprintSpeed = 10f;
    float baseSpeed = 6;

    public bool isBeingControlled = false;
    bool isFlashlightOn = false;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        flashlight = GameObject.FindGameObjectWithTag("Flashlight").GetComponent<Light>();
        Cursor.lockState = CursorLockMode.Locked;
        //baseSpeed = speed;
       
    }

    void Update()
    {
        CharacterControllerMovement();
        Flashlight();
        ObjectHandlers();
    }

    void LateUpdate()
    {
        CamControls();
    }

    void CharacterControllerMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        moveDir = new Vector3(horizontal, 0, vertical);
        moveDir *= speed;
        moveDir = transform.TransformDirection(moveDir);

        moveDir.y = moveDir.y + Physics.gravity.y;
        characterController.Move(moveDir * Time.deltaTime);
        if(horizontal != 0 || vertical != 0)//if player moves play walk animation
        {
            Anim.SetBool("isWalking", true);
            Anim.SetBool("isIdle", false);

        }
        else //is player is not moving go back to idle animation
        {
            Anim.SetBool("isWalking", false);
            Anim.SetBool("isIdle", true);
            
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = sprintSpeed;
            Anim.SetBool("isWalking", false);
            Anim.SetBool("isRunning", true);
        }
        else
        {
            speed = baseSpeed;
            Anim.SetBool("isRunning", false);
           // Anim.SetBool("isWalking", true);
        }
    }

    void CamControls()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        Vector3 rotationY = transform.localEulerAngles;
        rotationY.y += mouseX * camYSensitivity;
        transform.localRotation = Quaternion.AngleAxis(rotationY.y, Vector3.up);

        Vector3 rotationX = playerCam.gameObject.transform.localEulerAngles;
        rotationX.x -= mouseY * camXSensitivity;
        //rotationX.x = Mathf.Clamp(rotationX.x, 0, 30);
        playerCam.gameObject.transform.localRotation = Quaternion.AngleAxis(rotationX.x, Vector3.right);
    }

    void Flashlight()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!isFlashlightOn)
            {
                flashlight.enabled = true;
            }
            else
            {
                flashlight.enabled = false;
            }
            isFlashlightOn = !isFlashlightOn;
        }
    }


    void ObjectHandlers()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (!PlayerAim.aim.isCarryingObject)
            {
                PlayerAim.aim.ObjectPickUP();
            }
            else
            {
                PlayerAim.aim.ThrowObject();
            }
        }
    }
}
