using UnityEngine;
using UnityEngine.UI;

public class PowerManager : MonoBehaviour
{
    public static PowerManager powerManager;
    public float power = 50f;
    float maxPower = 50;
    public bool drainPower = false;
    public Image powerBar;
    public Text powerText;
    bool isStasis = false;
    float timer = 0f;
    float secondCount = 1f;

    Color normalMode = new Color(0f, 0.2878585f, 1f, 1f);
    Color stasisMode = new Color(0.7137255f, 0.2313726f, 1f, 1f);

    void Awake()
    {
        powerManager = this;
        power = maxPower;
    }

    private void Start()
    {
        powerBar = GameObject.FindGameObjectWithTag("PowerBar").GetComponent<Image>();
        powerBar.color = normalMode;
    }

    void Update()
    {
        if (power <= 0)
        {
            isStasis = true;
            WeaponThrow.weaponThrow.ReturnWeapon();
            powerBar.color = stasisMode;
        }

        timer += Time.deltaTime;
        if (timer > secondCount && drainPower == false)
            PowerUp();

        if (drainPower)
        {
            DecrementPower(5 * Time.deltaTime);
        }
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
                power += 0.5f;
            else
                power += 1;

            powerBar.fillAmount = power / maxPower;
        }
        else
        {
            powerBar.color = normalMode;
            isStasis = false;
        }
    }
}
