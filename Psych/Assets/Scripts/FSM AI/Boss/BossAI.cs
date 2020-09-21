using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BossAI : MonoBehaviour
{
    public static BossAI bossAI;
    public Image bossBar;
    public Transform target = null;
    int cycle = 100;
    int counter = 0;
    public int hp, point;
    public float shootTimer = 0;
    public float rate, throwSpeed, dist;
    public GameObject bossBlast, jointRef;
    public GameObject blastAttackSpawnPoint;
    public GameObject blastAttackSpawnPoint1;
    public GameObject blastAttackSpawnPoint2;
    public Animator anim;
    public GameObject warpPoint1,warpPoint2, warpPoint3;
    public bool isDead;
    public AudioSource auSource;
    public AudioClip auClip;
    public AudioClip auClip1;
    public AudioClip auClip2;


    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        bossAI = this;
        auSource = GetComponent<AudioSource>();
        agent = GetComponentInParent<NavMeshAgent>();
        jointRef.transform.rotation = Quaternion.identity;
        hp = 1500;
       
    }

    // Update is called once per frame
    void Update()
    {
        point = Random.Range(1, 10);
        bossBar.fillAmount = (hp)/1500f;
        transform.LookAt(target.position);
        dist = Vector3.Distance(target.position, transform.position);
        
        if (!target)
        {
            return;
        }
        counter++;
        if(counter > cycle)
        {
            counter = 0;
            cycle = Random.Range(700, 1000);
            MoveToTarget(transform.position);
        }

        shootTimer += Time.deltaTime;
        if (shootTimer >= rate && FF.force.shieldBreak)
        {
            BlastAttack();        
        }
        if(hp == 50)
        {
            anim.SetBool("isMad", true);
            FF.force.shieldBreak = false;
            auSource.PlayOneShot(auClip2);
            if (shootTimer >= .7)
            {
                GameObject psychicBlast1 = Instantiate(bossBlast, blastAttackSpawnPoint1.transform.position, Quaternion.identity);
                psychicBlast1.GetComponent<Rigidbody>().AddForce(transform.forward * throwSpeed, ForceMode.VelocityChange);
                GameObject psychicBlast2 = Instantiate(bossBlast, blastAttackSpawnPoint2.transform.position, Quaternion.identity);
                psychicBlast2.GetComponent<Rigidbody>().AddForce(transform.forward * throwSpeed, ForceMode.VelocityChange);
                shootTimer = 0;
               
            }
           
        }
        if(hp <= 0)
        {
            auSource.PlayOneShot(auClip);
            FF.force.shieldBreak = false;
            isDead = true;
            anim.SetBool("isDead", true);
            Invoke("Dead", 2);
        }
        //FieldOfView();
    }

    //void FieldOfView()
    //{
    //    if (!target)
    //    {
    //        return;
    //    }
    //    Vector3 directionToFace = target.position - transform.position;
    //    directionToFace.y = 0;
    //    if (directionToFace == Vector3.zero)
    //        directionToFace = -transform.forward;
    //    Quaternion targetPosition = Quaternion.LookRotation(directionToFace);
    //    transform.rotation = Quaternion.Slerp(transform.rotation, targetPosition, Time.deltaTime * 5);
    //}
    void Dead()
    {
        Destroy(gameObject);
    }
    void MoveToTarget(Vector3 targetPosition)
    {
        //anim.SetBool("isWalking", true);

        //if (dist >= 6&& FF.force.shieldBreak)
        //{
        //    anim.SetBool("isWalking", true);
           
        //    agent.SetDestination(targetPosition);
        //}
        //if (!target)
        //{
        //    return;
        //}
        //Vector3 directionToMove = target.position - transform.position;
        //if (directionToMove.x >= 0)
        //{
        //    if (directionToMove.z >= 0)
        //agent.SetDestination(transform.position + new Vector3(-10, 0, -5));
        //    else
        //        agent.SetDestination(targetPosition + new Vector3(-10, 0, 5));
        //}

        //else
        //{
        //    if (directionToMove.z >= 0)
        //        agent.SetDestination(targetPosition + new Vector3(10, 0, -5));
        //    else
        //        agent.SetDestination(targetPosition + new Vector3(10, 0, 5));
        //}

    }

    void BlastAttack()
    {
        anim.SetBool("isAttack", true);
        auSource.PlayOneShot(auClip1);
        GameObject psychicBlast = Instantiate(bossBlast, blastAttackSpawnPoint.transform.position, Quaternion.identity);
        psychicBlast.GetComponent<Rigidbody>().AddForce(transform.forward * throwSpeed, ForceMode.VelocityChange);
        shootTimer = 0;
        if(point<= 3)
        {
            agent.Warp(warpPoint1.transform.position);
        }
        else if(point==4||point==5||point==6)
        {
            agent.Warp(warpPoint2.transform.position);          
        }
        else if (point >= 7)
        {
            agent.Warp(warpPoint3.transform.position);
        }

    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Projectile")
        {
            hp -= 50;
            
        }   
    }
}