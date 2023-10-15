using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaseBoss : MonoBehaviour
{
    
    public float healthThreshold = 50f; // Limite de saúde para ativar esta fase
    public GameObject bossObject; // Referência ao objeto do chefe
    public GameObject nextPhase; // Referência à próxima fase do chefe

    private ControleVida bossHealth;

    void Start()
    {
        bossHealth = bossObject.GetComponent<ControleVida>();
    }

    void Update()
    {
        if (bossHealth.GetCurrentHealth() <= healthThreshold)
        {
            ActivateNextPhase();
        }
    }

    void ActivateNextPhase()
    {
        nextPhase.SetActive(true);
        gameObject.SetActive(false);
    }
}


