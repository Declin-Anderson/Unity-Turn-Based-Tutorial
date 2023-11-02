using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//* Keeps track of the health on the current unit
public class HealthSystem : MonoBehaviour
{
    public event EventHandler onDead;
    public event EventHandler OnDamaged;

    [SerializeField] private int health = 100;

    private int healthMax;

    private void Awake()
    {
        healthMax = health;
    }

    // Called when the unit takes damage
    public void Damage(int damageAmount)
    {
        health -= damageAmount;

        // sets health to 0 if goes negative
        if (health < 0)
        {
            health = 0;
        }

        OnDamaged?.Invoke(this, EventArgs.Empty);

        if (health == 0)
        {
            Die();
        }
    }

    //* Called when unit is dead
    private void Die()
    {
        onDead?.Invoke(this, EventArgs.Empty);
    }

    public float GetHealthNormalized()
    {
        return (float)health / healthMax;
    }
}
