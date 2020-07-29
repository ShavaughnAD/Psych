using UnityEngine;
using UnityEngine.UI;

public class TutorialPopUp : MonoBehaviour
{
    PlayerMovement playerMovement;
    public Text promptText;
    public string textPrompt;
    public string nextPrompt;

    public bool hasNextPrompt = false;
    public bool startCountdown = false;
    public float countdownTime = 3;

    #region MovementChecks

    bool move1 = false;
    bool move2 = false;
    bool move3 = false;
    bool move4 = false;

    #endregion


    public enum TutorialPhase 
    {   
        Movement, 
        PickUpWeapon, 
        ShootingWeapon, 
        ThrowingWeapon, 
        RecallingWeapon, 
        StealingWeapon,
        Play
    }
    public TutorialPhase tutorialPhase;

    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        if (startCountdown == true)
        {
            countdownTime -= Time.deltaTime;
            playerMovement.enabled = false;
            WeaponThrow.weaponThrow.enabled = false;
        }
        if (countdownTime <= 0)
        {
            if (hasNextPrompt)
            {
                promptText.text = nextPrompt;
            }
            else
            {
                promptText.text = textPrompt;
            }
            startCountdown = false;
            playerMovement.enabled = true;
            WeaponThrow.weaponThrow.enabled = true;
            TutorialPhases();
        }
    }

    public void TutorialPhases()
    {
        switch (tutorialPhase)
        {
            case TutorialPhase.Movement:

                if(move1 == true && move2 == true && move3 == true && move4 == true)
                {
                    tutorialPhase = TutorialPhase.PickUpWeapon;
                }

                if (Input.GetKeyDown(KeyCode.W))
                {
                    move1 = true;
                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                    move2 = true;
                }
                if (Input.GetKeyDown(KeyCode.A))
                {
                    move3 = true;
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    move4 = true;
                }

                break;

            case TutorialPhase.PickUpWeapon:

                promptText.text = "Nice, now move over to the weapon nearby and Press E to pick it up";

                if(WeaponThrow.weaponThrow.weapon != null)
                {
                    tutorialPhase = TutorialPhase.ShootingWeapon;
                }
                break;

            case TutorialPhase.ShootingWeapon:
                promptText.text = "Awesome, to shoot your equipped weapon, press the Left Mouse Button, go on Shoot some rounds";
                if (PlayerAim.aim.ammo <= 0)
                {
                    tutorialPhase = TutorialPhase.StealingWeapon;
                }

                break;

            case TutorialPhase.StealingWeapon:
               
                promptText.text = "Fear not when out of ammo, you can steal enemy weapons, aim at the nearby enemy's weapon and press Q to steal it!";
              
                if(WeaponThrow.weaponThrow.weapon.GetComponent<WeaponPickUp>().wasStolen == true)
                {
                    tutorialPhase = TutorialPhase.ThrowingWeapon;
                }

                break;

            case TutorialPhase.RecallingWeapon:
                promptText.text = "To Recall Weapon, Press Right Mouse Button Again";
                if (WeaponThrow.weaponThrow.isReturning)
                {
                    tutorialPhase = TutorialPhase.Play;
                }
                break;

            case TutorialPhase.ThrowingWeapon:
               
                promptText.text = "If you haven't realized yet, you have Psychic powers. You can throw weapons and then control them. Press the Right Mouse Button to throw weapon and then press 3 to control it";
                if (WeaponThrow.weaponThrow.isThrown)
                {
                    tutorialPhase = TutorialPhase.RecallingWeapon;
                }

                break;

            case TutorialPhase.Play:
                promptText.text = "Aight G, good stuff, you can play now";
                break;
        }
    }
}