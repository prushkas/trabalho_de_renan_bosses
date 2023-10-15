using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementsBoss : MonoBehaviour

{
    public float speed = 3.0f; // Velocidade de movimento do chefe
    private Transform player; // Referência ao jogador

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Encontre o jogador
    }

    void Update()
    {
        // Verifique se o jogador está à vista
        if (player != null)
        {
            // Calcule a direção para o jogador
            Vector3 direction = (player.position - transform.position).normalized;
            
            // Movimente o chefe na direção do jogador
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }
}


