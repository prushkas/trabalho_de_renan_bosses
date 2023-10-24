using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControleVida : MonoBehaviour
{
  
    public float maxHealth = 100f; // Saúde máxima do chefe
    public float currentHealth;
    public GameObject Clone;
    public GameObject CloneI;
    public Image BarraDeVida;

    void Start()
    {
        currentHealth = maxHealth;
    }

    
    void Update()
    {

        if(currentHealth <= 50)
        {
            Clone.SetActive(true);
            CloneI.SetActive(true);
        }
        
        BarraDeVida.fillAmount = currentHealth / maxHealth;
    }
    
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }

        // barra de vida
        BarraDeVida.fillAmount = currentHealth / maxHealth;
    }

    void Die()
    {
        Destroy(gameObject);
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    
    // diminuir vida do boss
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("HitDoPlayer"))
        {
            TakeDamage(10);
        }
    }
}


