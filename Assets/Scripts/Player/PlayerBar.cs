using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBar : MonoBehaviour
{
    public Slider slider;

    public void SetPlayerHealth(int health)
    {
        // Sets the slider's value as the health value passed in when the function is called
        slider.value = health;
    }

    public void SetMaxPlayerHealth(int health)
    {
        // Sets the slider's value and maxValue as the health value passed in when the function is called
        slider.maxValue = health;
        slider.value = health;
    }
}
