using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Healthbar : MonoBehaviour
{

    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public TMP_Text text;

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        text.SetText(health.ToString());

        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        slider.value = health;

        text.SetText(health.ToString());

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
