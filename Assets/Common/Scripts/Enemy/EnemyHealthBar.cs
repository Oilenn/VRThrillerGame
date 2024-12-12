using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class EnemyHealthBar
{
    private Slider healthBar; // —сылка на полоску здоровь€
    private float maxHealth;

    public EnemyHealthBar(float maxHealth, Slider healthBar)
    {
        this.maxHealth = maxHealth;
        this.healthBar = healthBar;
    }

    public void UpdateHealthBar(float currentHealth)
    {
        if (healthBar != null)
        {
            healthBar.value = currentHealth / maxHealth;
        }
    }
}

