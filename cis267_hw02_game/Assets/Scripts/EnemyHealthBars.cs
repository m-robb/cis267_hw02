using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBars : MonoBehaviour
{
    [SerializeField] private Slider slider;

    public void updateHealthBar(float curHealth, float maxHealth)
    {
        slider.value = curHealth / maxHealth;
    }
}
