using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Script che gestisce la barra della stamina
public class EnergyBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    //metodo per settare la stamina massima
    public void SetMaxEnergy(float energy)
    {
        slider.maxValue = energy;
        slider.value = energy;
        fill.color = gradient.Evaluate(1f);
    }

    //metodo per settare la stamina
    public void SetEnergy(float energy)
    {
        slider.value = energy;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    //metodo per ottenere la stamina
    public float GetEnergy()
    {
        return slider.value;
    }

}
