using UnityEngine;

//Reference: gamesplusjames. (20170. Make A 3D Platformer in Unity #6: Moving With Camera Rotation
public class CameraController : MonoBehaviour
{
    public Transform target;
    public Transform pivot;
    public Vector3 offset;
    public bool useOffsetValues;
    public float rotSpeed;
    public float inAirMoveSpeed;
    public float moveSpeed;

    WeaponThrow weaponThrow;

    void Start()
    {
        weaponThrow = FindObjectOfType<WeaponThrow>();
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
        vertical = Mathf.Clamp(vertical, -35, 60);
        float desiredYAngle = target.eulerAngles.y;
        float desiredXAngle = pivot.eulerAngles.x;
        Quaternion rot = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);
        transform.position = target.position - (rot * offset);
        var d = Input.GetAxis("Mouse ScrollWheel");
        if (CameraManager.cameraManager.playerMovement.isBeingControlled && weaponThrow.isReturning == false)
        {
            target.Rotate(0, horizontal, 0);
            //target.Rotate(-vertical, 0, 0);
            //pivot.Rotate(vertical, 0, 0);
            //pivot.Rotate(0, horizontal, 0);
            transform.LookAt(target);
            //pivot.Rotate(-vertical, 0, 0)
        };
    }
}