using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradientFill;
    public Gradient gradientIcon;
    public Image fill;
    public Image icon;

    public void SetBossMaxHealth (int bosshealth)
    {
        slider.maxValue = bosshealth;
        slider.value = bosshealth;

        fill.color = gradientFill.Evaluate(1f);
        icon.color = gradientIcon.Evaluate(1f);
    }

    public void SetBossHealth(int bosshealth)
    {
        slider.value = bosshealth;

        fill.color = gradientFill.Evaluate(slider.normalizedValue);
        icon.color = gradientIcon.Evaluate(slider.normalizedValue);
    }
}
