using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public GameObject weapon;
    CharacterController characterController;
    WeaponThrow weaponThrow;

    public float speed = 6;
    public float jumpHeight = 8;
    public float gravity = 20;

    Vector3 moveDir = Vector3.zero;

    public Camera playerCam;
    public float camXSensitivity = 1.5f;
    public float camYSensitivity = 1;

    public bool isBeingControlled = false;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        weaponThrow = GetComponent<WeaponThrow>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Movement();
        CamControls();
        WeaponMovement();
        GetCursor();
        ObjectHandlers();
    }

    void Movement()
    {
        if (characterController.isGrounded)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            moveDir = new Vector3(horizontal, 0, vertical);
            moveDir *= speed;
        }
        moveDir.y -= gravity * Time.deltaTime;
        moveDir = transform.TransformDirection(moveDir);
        characterController.Move(moveDir * Time.deltaTime);
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

    void WeaponMovement()
    {
        if (isBeingControlled == false)
        {
            float x = Screen.width / 2;
            float y = Screen.height / 2;
            var ray = playerCam.ScreenPointToRay(new Vector3(x, y, 0));
            weapon.transform.LookAt(playerCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 100)));
        }
    }

    void GetCursor()
    {
        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void ObjectHandlers()
    {
        if (Input.GetKeyDown(KeyCode.Alpha9))
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
