using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public float lookRadius = 10f;
    Transform target;
    NavMeshAgent agent;
    Animator Anim;
    Transform CurrentLoc;
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        Anim = GetComponent<Animator>();
        CurrentLoc = gameObject.transform;
    }

    void FaceTarget()//rotate and face target
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void AttackTarget()//attack target
    {
        //Anim.SetBool("isAttacking", true);
       //Anim.SetBool("isWalking", false);
       // Anim.SetBool("isIdle", false);
    }
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);//distance between enemy and target
        if(distance <= lookRadius)//if target is within lookradius
        {
            //Anim.SetBool("isWalking", true);
            //Anim.SetBool("isIdle", false);
            agent.SetDestination(target.position);//move towards target

            if(distance <= agent.stoppingDistance)//if target is within attack distance
            {
                FaceTarget();
                AttackTarget();

            }
           
        }else if(distance >= lookRadius)
        {
            //Anim.SetBool("isIdle", true);
            //Anim.SetBool("isWalking", false);
            //Anim.SetBool("isAttacking", false);
        }
        
       
    }

     void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 2);

    }
   
}
