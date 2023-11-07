using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    
    public GameObject laserPrefab; // Prefab do laser
    public Transform firePoint; // Ponto de origem do laser
    public float fireRate = 3.0f; // Taxa de disparo em segundos
    public float attackRange = 10.0f; // Alcance de ataque do chefe
    private float nextFireTime;
    private Transform player; // Referência ao jogador

    private AudioSource audio;
    public AudioClip clip1;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Encontre o jogador
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Time.time > nextFireTime)
    {
        if (IsPlayerInAttackRange())
        {
            // Determina a posição relativa do jogador em relação ao chefe
            Vector3 direction = player.position - transform.position;

            // Verifica se o jogador está à esquerda ou à direita do chefe
            if (direction.x < 0)
            {
                // Vira o chefe para a direita
                transform.localScale = new Vector3(7, 7, 7);
            }
            else
            {
                // Vira o chefe para a esquerda
                transform.localScale = new Vector3(-7, 7, 7);
            }

            FireLaser();
            
            audio.PlayOneShot(clip1);
            
            nextFireTime = Time.time + 1.0f / fireRate;
        }
    }
 }

    bool IsPlayerInAttackRange()
    {
        // Verifica se o jogador está dentro do alcance de ataque
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            return distanceToPlayer <= attackRange;
        }
        return false;
    }

    void FireLaser()
{
    if (player != null)
    {
        Vector3 direction = player.position - firePoint.position;
        direction.Normalize();

        // Cria o laser com um Rigidbody2D
        GameObject laser = Instantiate(laserPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = laser.GetComponent<Rigidbody2D>();

        // Aplica uma força para mover o tiro na direção do jogador
        float laserSpeed = 10.0f; // Ajuste a velocidade conforme necessário
        rb.velocity = direction * laserSpeed;

        // Destroi o laser após um certo tempo (se não atingir o jogador)
        Destroy(laser, 0.5f);
    }
}

   
}




