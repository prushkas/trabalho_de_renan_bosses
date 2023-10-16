using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleVida : MonoBehaviour
{
  
    public float maxHealth = 100f; // Saúde máxima do chefe
    public float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }
}


