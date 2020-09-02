using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager cameraManager;
    public Camera playerCam;
    public Camera weaponCam;

    public CameraController cameraController;
    public PlayerMovement playerMovement;
    public PlayerAim playerAim;

    void Awake()
    {
        cameraManager = this;
        playerMovement = FindObjectOfType<PlayerMovement>();
        //cameraController = weaponCam.GetComponent<CameraController>();
        playerAim = FindObjectOfType<PlayerAim>();
    }

    void Start()
    {
        ActivatePlayerCamera();
    }

    public void ActivatePlayerCamera()
    {
        playerCam.enabled = true;
        playerAim.cam = playerCam;
        //weaponCam.enabled = false;
        playerMovement.isBeingControlled = false;
        playerMovement.enabled = true;
        PowerManager.powerManager.drainPower = false;
    }

    public void ActivateWeaponCamera()
    {
        //cameraController.enabled = true;
        playerAim.cam = weaponCam;
        //weaponCam.enabled = true;
        playerMovement.isBeingControlled = true;
        playerMovement.enabled = false;
        PowerManager.powerManager.drainPower = true;
    }

    public void AssignNewWeaponCam(CameraController _camController, Camera _weaponCam)
    {
        weaponCam = _weaponCam;
        cameraController = _camController;
    }
}