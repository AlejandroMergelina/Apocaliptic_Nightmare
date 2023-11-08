using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    Slider slider;

    [SerializeField]
    PlayerHealth pH;

    private void Start()
    {

        slider.maxValue = pH.GetInfoMaxHealth();

    }


    void Update()
    {
        slider.value = pH.GetInfoCurrentHealth();
    }
}
