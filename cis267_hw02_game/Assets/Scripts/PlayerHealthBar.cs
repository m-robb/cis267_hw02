using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;

    public void updateHealthBar(float health, float maxHealth)
    {
        slider.value = health / maxHealth;
    }
}
