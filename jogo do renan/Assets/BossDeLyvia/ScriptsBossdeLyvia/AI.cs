using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
   

    public float detectionRadius = 5.0f; // Raio de detecção do jogador
    public float moveSpeed = 3.0f; // Velocidade de movimento do chefe
    private Transform player; // Referência ao jogador
    private bool isChasingPlayer = false; // Indica se o chefe está perseguindo o jogador

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Encontre o jogador
    }

    void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                // O jogador está dentro do raio de detecção, o chefe pode começar a persegui-lo
                isChasingPlayer = true;
            }
        }

        if (isChasingPlayer)
        {
            if (player != null)
            {
                // Calcule a direção para o jogador
                Vector3 direction = (player.position - transform.position).normalized;
                
                // Movimente o chefe na direção do jogador com a velocidade definida
                transform.Translate(direction * moveSpeed * Time.deltaTime);
            }
        }
    }
}


