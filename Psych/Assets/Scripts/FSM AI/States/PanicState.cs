using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PanicState : FsmState
{
    [SerializeField]
    private GameObject weaponRack;
    [SerializeField]
    private float panicRunSpeed = 8f;

    private Vector3 lastKnownTargetPosition;
    private NavMeshAgent navMeshAgent;
    private WasAlerted wasAlerted;
    private PlayerVision vision;
    private AttackState attackState;
    private bool lookingForWeapon = true;


    public List<GunRack> GRack;



    private void Start()
    {
        wasAlerted = this.GetComponent<WasAlerted>();
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        vision = this.GetComponent<PlayerVision>();
        attackState = this.GetComponent<AttackState>();

        FindAllGunRacksInScene();

        
    }
    private void FindAllGunRacksInScene()
    {
        foreach (GunRack GAdd in FindObjectsOfType<GunRack>())
        {
            if (GRack.Contains(GAdd))
            {
                Debug.LogError("Cant Add");
                //Blank
            }
            else
            {
                GRack.Add(GAdd);
            }
        }
    }

    private void OnEnable()
    {
        //if (vision.GetTargetInSight())
        //{
        //   //lastKnownTargetPosition = vision.getTargetObjectTransform().position;
        //}
        lookingForWeapon = true;
    }

    private void Update()
    {

        RunToGunRack();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == weaponRack && lookingForWeapon) {

            Debug.Log("Attempting to equip weapon...");
            lookingForWeapon = false;
            wasAlerted.AlertWithTargetLastKnownPosition(lastKnownTargetPosition);
        }
    }


    public void RunToGunRack()
    {
        //Find Nearest Gun Rack and run 
        for (int i = 0; i < GRack.Count; i++)
        {
            foreach (Transform G_Rack in GRack[i].gameObject.transform)
            {
                float Dist = Mathf.Infinity;
                Vector3 GunRackPos = G_Rack.position;
                float GunRa = GunRackPos.sqrMagnitude;
                if (GunRa < Dist)
                {
                    navMeshAgent.SetDestination(GRack[i].gameObject.transform.position);
                    navMeshAgent.speed = panicRunSpeed;
                }
            }
        }
    }

    //private void GoToWeaponRackAndEquipWeapon()
    //{
    //    if(weaponRack != null && lookingForWeapon)
    //    {
    //        navMeshAgent.SetDestination(weaponRack.transform.position);
    //    }
    //    else
    //    {
    //        Debug.LogWarning("Weapon rack property is set to null");
    //    }

    //}
    

}
