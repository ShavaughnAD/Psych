using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSteal : MonoBehaviour
{

    private Ray ray;
    private const string ENEMY_TAG = "Enemy";
    public Transform rayOrigin;
    public float rayLength = 2f;
    public ParticleSystem rayParticleEffect = null;  //The particle effect for the psychic ray\
    public WeaponThrow playerCurrentWeapon = null;


    /*Objective: Create a feature that performs the following function(s)
     * When player fires a psychic RAY at an opponent with a stealable weapon...
         * disarm the enemy
            * If ray hits object:
                * Is other object an enemy?
                    Yes - Get enemy's arsenal (each enemy has an arsenal)
                    Does enemy aresenal contain stealable weapon?
                        Yes - exec: StealWeapon()
                * 
         * Assign the stealable weapon to player's ownership (Enemy's Weapon => Player's Weapon)
         * Float the stolen weapon back to the player
         * Weapon goes back into player's possession (player is holding the weapon)
    */

    private void Update(){
        if(Input.GetKeyDown(KeyCode.Z)){
            
            ShootPsychicRay();
        }
        
    }
    
    
    public void ReturnWeapon()
    {
        // time = 0;
        // prevPos = weaponRB.position;
        // isReturning = true;
        // weaponRB.velocity = Vector3.zero;
        // weaponRB.isKinematic = true;
        // Camera.main.GetComponent<CameraController>().target = player;
        
    }

    

    private void ShootPsychicRay(){
        Debug.Log("Shooting Ray");
        RaycastHit [] hits = null;

        hits = Physics.RaycastAll(rayOrigin.position, Vector3.forward, rayLength);

        if(hits.Length > 0){

            for(int i = 0; i < hits.Length; i++){
                if(hits[i].collider){
                   
                    GameObject objectThatWasHit = hits[i].transform.gameObject;
                    Debug.Log("Hit: " + objectThatWasHit.tag);

                    Debug.Log("Object that was hit has a child object: " + objectThatWasHit.transform.GetChild(0) != null);
                    
                    if (objectThatWasHit.tag == (ENEMY_TAG) &&  objectThatWasHit.transform.GetChild(0) != null){

                        
                        GameObject enemyWeapon = hits[i].transform.gameObject.transform.GetChild(0).gameObject;
                        if ((enemyWeapon.tag == "Weapon" )
                            &&  (enemyWeapon.transform.GetChild(0) != null)
                            &&  (enemyWeapon.transform.GetChild(0).gameObject.tag == "Stealable")
                            &&  (enemyWeapon.transform.GetChild(0).gameObject.activeSelf)){
                                
                            Debug.Log("Enemy has weapon: " + hits[i].transform.gameObject.transform.GetChild(0).gameObject.tag  );

                            Debug.Log("Stealable weapon found:" + enemyWeapon.transform.GetChild(0).gameObject.name );

                            
                            Destroy(playerCurrentWeapon.weapon);
                            playerCurrentWeapon.weapon = enemyWeapon.transform.GetChild(0).gameObject;
                            playerCurrentWeapon.weaponRB = enemyWeapon.transform.GetChild(0).gameObject.GetComponent<Rigidbody>();
                            playerCurrentWeapon.ReturnWeapon();
                            enemyWeapon.transform.GetChild(0).gameObject.GetComponent<WeaponShooting>().equipped = true;
                            
                        }else if(enemyWeapon.transform.GetChild(0).gameObject.activeSelf){
                            
                            Debug.Log("Enemy weapon slot is empty..."  );
                        }
                    }
                }
            }

        }
        Debug.DrawRay(rayOrigin.position, Vector3.forward * rayLength, Color.green, 3);

    }
}
