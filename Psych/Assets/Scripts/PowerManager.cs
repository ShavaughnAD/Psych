using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerManager : MonoBehaviour
{

    [SerializeField] float power = 50f;
    GameObject bar;
    bool isStasis = false;
    float timer = 0f;
    float secondCount = 1f;

    private void Start()
    {
        bar = GameObject.Find("Bar");
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if(timer > secondCount)
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
            isStasis = true;
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
            isStasis = false;
        }
    }

}
