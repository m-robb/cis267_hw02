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

    public void flipHealthBar()
    {
        //Flips health bar
        Vector3 currentScale = gameObject.GetComponentInParent<Transform>().localScale;
        currentScale.x *= -1;
        gameObject.GetComponentInParent<Transform>().localScale = currentScale;
    }
}
