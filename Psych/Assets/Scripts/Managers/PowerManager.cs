using UnityEngine;
using UnityEngine.UI;

public class PowerManager : MonoBehaviour
{
    public static PowerManager powerManager;
    public float power = 50f;
    public float maxPower = 50;
    public float regenRate = 5;
    public bool drainPower = false;
    public Image powerBar;
    public Text powerText;
    bool isStasis = false;
    float timer = 0f;
    float secondCount = 1f;

    Color normalMode = new Color(255f, 255, 255);
    Color stasisMode = Color.magenta;

    void Awake()
    {
        powerManager = this;
        power = maxPower;
    }

    void Start()
    {
        powerBar = GameObject.FindGameObjectWithTag("PowerBar").GetComponent<Image>();
        powerBar.color = normalMode;
        powerText = GameObject.FindGameObjectWithTag("PowerText").GetComponent<Text>();
    }

    void Update()
    {
        if (power <= 0)
        {
            isStasis = true;
            WeaponThrow.weaponThrow.ReturnWeapon();
            powerBar.color = stasisMode;
        }
        if (drainPower)
        {
            DecrementPower(5 * Time.deltaTime);
        }
        timer += Time.deltaTime;
        if (timer > secondCount && drainPower == false)
            PowerUp();

        powerText.text = power.ToString("F0") + " / " + maxPower.ToString("F0");
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
        if(power < maxPower)
        {
            if(isStasis)
                power += (regenRate/2);
            else
                power += regenRate;

            powerBar.fillAmount = power / maxPower;
        }
        else
        {
            powerBar.color = normalMode;
            isStasis = false;
        }
    }
}
