using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms.GameCenter;

public class BossAI : MonoBehaviour
{
    
    public Transform target = null;
    int cycle = 100;
    int counter = 0;
    float shootTimer = 0;
    public float rate = 10;
    public float throwSpeed = 1f;

    public GameObject bossBlast;
    public GameObject blastAttackSpawnPoint;


    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {

        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!target)
        {
            return;
        }
        counter++;
        if(counter > cycle && target)
        {
            counter = 0;
            cycle = Random.Range(700, 1000);
            MoveToTarget(target.position);
        }

        shootTimer += Time.deltaTime;
        if (shootTimer >= rate)
        {
            BlastAttack();
        }
        FieldOfView();
    }

    void FieldOfView()
    {
        if (!target)
        {
            return;
        }
        Vector3 directionToFace = target.position - transform.position;
        directionToFace.y = 0;
        if (directionToFace == Vector3.zero)
            directionToFace = -transform.forward;
        Quaternion targetPosition = Quaternion.LookRotation(directionToFace);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetPosition, Time.deltaTime * 5);
    }

    void MoveToTarget(Vector3 targetPosition)
    {
        if (!target)
        {
            return;
        }
        Vector3 directionToMove = target.position - transform.position;
        if(directionToMove.x >= 0)
        {
            if(directionToMove.z >= 0)
                agent.SetDestination(targetPosition + new Vector3(-10, 0, -5));
            else
                agent.SetDestination(targetPosition + new Vector3(-10, 0, 5));
        }
            
        else
        {
            if(directionToMove.z >= 0)
                agent.SetDestination(targetPosition + new Vector3(10, 0, -5));
            else
                agent.SetDestination(targetPosition + new Vector3(10, 0, 5));
        }
            
    }

    void BlastAttack()
    {
        if (!target)
        {
            return;
        }
        GameObject psychicBlast = Instantiate(bossBlast, blastAttackSpawnPoint.transform.position, Quaternion.identity);
        psychicBlast.GetComponent<Rigidbody>().AddForce(target.TransformDirection(Vector3.back)* throwSpeed *Time.deltaTime, ForceMode.Impulse);
        shootTimer = 0;
    }

}
