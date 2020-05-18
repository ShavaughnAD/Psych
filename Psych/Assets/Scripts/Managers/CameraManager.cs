using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager cameraManager;
    public Camera playerCam;
    public Camera weaponCam;

    public CameraController cameraController;
    public PlayerMovement playerMovement;

    void Awake()
    {
        cameraManager = this;
        playerMovement = FindObjectOfType<PlayerMovement>();
        cameraController = weaponCam.GetComponent<CameraController>();
    }

    void Start()
    {
        ActivatePlayerCamera();
    }

    public void ActivatePlayerCamera()
    {
        playerCam.enabled = true;
        weaponCam.enabled = false;
    }

    public void ActivateWeaponCamera()
    {
        cameraController.enabled = true;
        weaponCam.enabled = true;

    }
}
