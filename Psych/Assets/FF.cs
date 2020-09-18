using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FF : MonoBehaviour
{
    public static FF force;
    public Vector3 speed;
    public int hp;
    public bool shieldBreak;
     void Awake()
    {
        force = this;
        hp = 400;
    }
    void Update()
    {
        if (hp <= 0)
        {
            shieldBreak = true;
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        CharacterController chtr = other.gameObject.GetComponent(typeof(CharacterController)) as CharacterController;
        if (chtr)
        {
            chtr.SimpleMove(speed);
        }
        if(other.gameObject.tag == "Projectile")
        {
            hp -= 50;
        }
    }
}
