using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Script che gestisce la barra della salute
public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    //metodo per settare la vita massima
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        fill.color=gradient.Evaluate(1f);
    }

    //metodo per settare la vita
    public void SetHealth(int health)
    {
        slider.value = health;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    //metodo per ottenere la vita
    public float GetHealth()
    {
        return slider.value;
    }
}
