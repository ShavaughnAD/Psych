using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public GameObject displayWeapon;
    public Image displayWeaponImage;

    void Awake()
    {
        gameManager = this;
        displayWeapon = GameObject.FindGameObjectWithTag("DisplayWeapon");
        displayWeaponImage = displayWeapon.GetComponent<Image>();
    }

    void Start()
    {
        displayWeapon.SetActive(false);
    }
}