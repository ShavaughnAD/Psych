﻿using UnityEngine;

public class WeaponThrow : MonoBehaviour
{
    #region Public Variables
    public static WeaponThrow weaponThrow;
    public GameObject weapon = null;
    public Rigidbody weaponRB = null;
    public float returnRotSpeed = 50;
    public float throwForce = 50;
    public Transform target = null;
    public Transform curvePoint = null;
    public bool isThrown = false;
    #endregion
    #region Private Variables
    Transform player;
    bool isReturning = false;
    float time = 0;
    Vector3 prevPos = Vector3.zero;

    void Awake()
    {
        weaponThrow = this;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    #endregion
    void Update()
    {
        #region Input
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if(isReturning || isThrown)
            {
                return;
            }
            else
            {
                ThrowWeapon();
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isThrown)
            {
                Camera.main.GetComponent<CameraController>().focusPlayer = true;
                ReturnWeapon();
            }
        }

        #endregion

        if (isReturning)
        {
            if(time < 1)
            {
                weaponRB.position = BezierQCP(time, prevPos, curvePoint.position, target.position);
                weaponRB.rotation = Quaternion.Slerp(weaponRB.transform.rotation, target.rotation, returnRotSpeed * Time.deltaTime);
                time += Time.deltaTime;
            }
            else
            {
                ResetWeapon();
            }
        }
    }

    void ThrowWeapon()
    {
        isReturning = false;
        isThrown = true;
        weapon.GetComponent<WeaponShooting>().thrown = true;
        weaponRB.transform.parent = null;
        weaponRB.isKinematic = false;
        weaponRB.AddForce(Camera.main.transform.TransformDirection(Vector3.forward) * throwForce, ForceMode.Impulse);
        weaponRB.AddTorque(weaponRB.transform.TransformDirection(Vector3.right) * 100, ForceMode.Impulse);
    }

    public void ReturnWeapon()
    {
        time = 0;
        prevPos = weaponRB.position;
        isReturning = true;
        weaponRB.velocity = Vector3.zero;
        weaponRB.isKinematic = true;
        Camera.main.GetComponent<CameraController>().target = player;
    }

    void ResetWeapon()
    {
        isReturning = false;
        isThrown = false;
        weapon.GetComponent<WeaponShooting>().controlling = false;
        weaponRB.transform.parent = transform;
        weaponRB.position = target.position;
        weaponRB.rotation = target.rotation;
        Debug.LogError("Weapon Returned");
    }

    Vector3 BezierQCP(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        Vector3 p = (uu * p0) + (2 * u * t * p1) + (tt * p2);
        return p;
    }
}