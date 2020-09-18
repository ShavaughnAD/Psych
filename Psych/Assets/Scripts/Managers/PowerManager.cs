using UnityEngine;
using UnityEngine.UI;

public class PowerManager : MonoBehaviour
{
    public static PowerManager powerManager;
    public float power = 50f;
    public float maxPower = 100;
    public float regenRate = 5;
    public bool drainPower = false;
    public Image powerBar;
    //public Text powerText;
    public bool isStasis = false;
    float timer = 0f;
    float secondCount = 1f;

    Color normalMode = new Color(255f, 255, 255);
    Color stasisMode = Color.magenta;

    void Awake()
    {
        if (powerManager == null)
        {
            powerManager = this;
            power = maxPower;
        }
    }
    private void OnEnable()
    {
        if (powerManager == null)
        {
            powerManager = this;
            power = maxPower;
        }
    }

    void Update()
    {
        if (powerBar == null && GameObject.FindGameObjectWithTag("PowerBar") != null)
        {
            powerBar = GameObject.FindGameObjectWithTag("PowerBar").GetComponent<Image>();
            power = maxPower;
            powerBar.color = normalMode;
            timer = 0.0f;
            //powerText = GameObject.FindGameObjectWithTag("PowerText").GetComponent<Text>();

        }

        if (power <= 0)
        {
            isStasis = true;
            //FindObjectOfType<WeaponThrow>().ReturnWeapon();
            powerBar.color = stasisMode;
        }
        if (drainPower)
        {
            DecrementPower(5 * Time.deltaTime);
        }
     
        if (power < maxPower && PauseMenu.pauseMenuRef.isPaused == false)
        {
            Debug.Log("Power up");
            timer += Time.deltaTime;
            if (timer > secondCount && drainPower == false)
                PowerUp();
        }
        else
        {
            //needed to remove stasis
            PowerUp();
        }
    
        //dpowerText.text = power.ToString("F0") + " / " + maxPower.ToString("F0");
    }

    public bool DecrementPower(float valueDeducted)
    {
        power = Mathf.Clamp(power - valueDeducted, 0, maxPower);
        if (power >= 0)
        {
            powerBar.fillAmount = power / maxPower;
            return true;
        }
        else
        {
            Debug.LogError("Not enough Power");
            return false;
        }
    }

    void PowerUp()
    {
        timer -= secondCount;
        if (power < maxPower)
        {
            if (isStasis)
                power += (regenRate * 2);
            else
                power += regenRate;

            if (power > maxPower)
            {
                power = maxPower;
            }
            powerBar.fillAmount = power / maxPower;
        }
        else
        {
            powerBar.color = normalMode;
            isStasis = false;
        }
    }
}
