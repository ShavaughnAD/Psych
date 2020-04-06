using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float camSpeed = 120f;
    public float clampAngle = 0f;
    public float inputSens = 150f;

    public GameObject camFollowObj;

    public float mouseX;
    public float mouseY;
    public float finalInputX;
    public float finalnputZ;

    float rotX;
    float rotY;

    void Start()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotX = rot.x;
        rotY = rot.y;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float inputX = Input.GetAxis("RightStickHorizontal");
        float inputZ = Input.GetAxis("RightStickVertical");
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        finalInputX = inputX + mouseX;
        finalnputZ = inputZ + mouseY;

        rotY += finalInputX * inputSens * Time.deltaTime;
        rotX += finalnputZ * inputSens * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

        Quaternion localRot = Quaternion.Euler(rotX, rotY, 0);
        transform.rotation = localRot;
    }

    void LateUpdate()
    {
        CamUpdate();
    }

    void CamUpdate()
    {
        Transform target = camFollowObj.transform;

        float step = camSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }
}
