using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySci : MonoBehaviour
{
    public NavMeshAgent ai;
    public GameObject player;
    public float fov = 94.0f;
    public Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();

    }
    void Update()
    {
       if(PlayerVisible())
       {
            anim.Play("SlugScientistRunawayAnim");
         
       } 
    }
    bool PlayerVisible()
    {
        bool visib = false;
        Vector3 direction = player.transform.position - transform.position;
        RaycastHit hit;
        if(Physics.Raycast(transform.position,direction, out hit, 10f))
        {
            if (hit.transform.tag.Equals(player.transform.tag))
            {
                float angle = Vector3.Angle(transform.forward, direction);
                if(angle <= fov)
                {
                    visib = true;
                }
            }
        }
        return visib;
    }
}
