using UnityEngine;

//Reference: gamesplusjames. (20170. Make A 3D Platformer in Unity #6: Moving With Camera Rotation
public class CameraController : MonoBehaviour
{
    public Transform target;
    public Transform pivot;
    public Vector3 offset;
    public bool useOffsetValues;
    public float rotSpeed;
    public bool focusPlayer = true;

    void Start()
    {
        if (!useOffsetValues)
        {
            offset = target.position - transform.position;
        }

        pivot.transform.position = target.transform.position;
        pivot.transform.parent = target.transform;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        float vertical = Input.GetAxis("Mouse Y") * rotSpeed;
        float horizontal = Input.GetAxis("Mouse X") * rotSpeed;
        float desiredYAngle = target.eulerAngles.y;
        float desiredXAngle = pivot.eulerAngles.x;
        Quaternion rot = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);
        transform.position = target.position - (rot * offset);
        if (focusPlayer)
        {
            target.Rotate(0, horizontal, 0);
            pivot.Rotate(-vertical, 0, 0);
            transform.LookAt(target);
        }
        else
        {
            target.Rotate(0, horizontal, 0);
            target.Rotate(-vertical, 0, 0);
            pivot.Rotate(vertical, 0, 0);
            pivot.Rotate(0, horizontal, 0);
            transform.LookAt(target);
            //pivot.Rotate(-vertical, 0, 0);
        }
    }
}