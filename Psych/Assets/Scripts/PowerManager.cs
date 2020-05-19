using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerManager : MonoBehaviour
{

    [SerializeField] float power = 50f;
    GameObject bar;
    GameObject powerRadar;
    bool isStasis = false;
    float timer = 0f;
    float secondCount = 1f;

    Color normalMode = new Color(0f, 0.2878585f, 1f, 1f);
    Color stasisMode = new Color(0.7137255f, 0.2313726f, 1f, 1f);

    private void Start()
    {
        bar = GameObject.Find("Bar");
        powerRadar = GameObject.Find("PowerRadar");
        powerRadar.GetComponent<Image>().color = normalMode;
    }

    private void Update()
    {
        if (power <= 0)
        {
            isStasis = true;
            powerRadar.GetComponent<Image>().color = stasisMode;
        }

        timer += Time.deltaTime;
        if (timer > secondCount)
            PowerUp();
    }

    public bool DecrementPower(float valueDeducted)
    {
        power = (power - valueDeducted);
        if (power >= 0)
        {
            bar.transform.localScale = new Vector3(power * 0.02f, 1f);
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
        if(power < 50)
        {
            if(isStasis)
                power += 0.5f;
            else
                power += 1;

            bar.transform.localScale = new Vector3(power * 0.02f, 1f);
        }
        else
        {
            powerRadar.GetComponent<Image>().color = normalMode;
            isStasis = false;
        }
    }

}
