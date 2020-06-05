using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public GameObject weapon;
    CharacterController characterController;
    public Light flashlight;

    public float speed = 6;
    public float gravity = 20;

    Vector3 moveDir = Vector3.zero;

    public Camera playerCam;
    public float camXSensitivity = 1.5f;
    public float camYSensitivity = 1;

    public bool isBeingControlled = false;
    bool isFlashlightOn = false;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        flashlight = GameObject.FindGameObjectWithTag("Flashlight").GetComponent<Light>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        CharacterControllerMovement();
        Flashlight();
        WeaponMovement();
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
