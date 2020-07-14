using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Variables

    public Transform target;
    public Transform pivot;
    public Vector3 offset;
    public bool useOffsetValues;
    public float rotSpeed;
    public float moveSpeed;

    WeaponThrow weaponThrow;

    public float rotationSpeed = 1f;
    float mouseX, mouseY;

    #endregion

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
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;

        if (CameraManager.cameraManager.playerMovement.isBeingControlled && weaponThrow.isReturning == false)
        {
            target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
            target.rotation = Quaternion.Euler(0, mouseX, 0);

            #region Weapon Movement

            if (Input.GetKey(KeyCode.W))
            {
                target.GetComponent<Rigidbody>().velocity = Vector3.zero;
                target.transform.Translate(Vector3.up * 1 * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.S))
            {
                target.transform.Translate(Vector3.down * 1 * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.A))
            {
                target.transform.Translate(Vector3.left * 1 * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.D))
            {
                target.transform.Translate(Vector3.right * 1 * Time.deltaTime);
            }
        };

        #endregion
    }
}